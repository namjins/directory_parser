using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualBasic.FileIO;
using Word = Microsoft.Office.Interop.Word;


namespace DirectoryParser
{
    class DocumentBuilder
    {
        private string[] headers;
        private bool success = false;
        private Word.Document document;
        private Word.Application winword;
        private List<Family> familyList = new List<Family>();
        private string[] desired_fields = new string[]
        {
            "Couple Name",
            "Family Phone",
            "Family Address",
            "Head Of House Phone",
            "Child Name"
        };
        private Dictionary<string, string> directions = new Dictionary<string, string>
        {
            {"NORTH", "N"},
            {"SOUTH", "S"},
            {"EAST", "E"},
            {"WEST", "W"},
        };

        private void ParseCsv(String directory)
        {
            
            using (TextFieldParser parser = new TextFieldParser(directory))
            {
                int pass = 0;
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    ++pass;
                    if (pass == 1)
                    {
                        //Assign CSV headers to separate array for easier processing.
                        this.headers = parser.ReadFields();
                        continue;
                    }

                    //Process row
                    Family family = new Family();
                    string[] fields = parser.ReadFields();

                    for (int i = 0; i < fields.Length; ++i) {
                        if (string.IsNullOrEmpty(fields[i].Trim()))
                        {
                            continue;
                        }

                        if (this.IsDesiredField(i))
                        {
                            this.ProcessField(family, fields[i].Trim(), this.headers[i].Trim());
                        }

                    }

                    if (!string.IsNullOrEmpty(family.GetHead()))
                    {
                        this.familyList.Add(family);
                    }
                }
            }

        }

        private void ProcessField(Family family, string field_value, string header)
        {
            switch (header)
            {
                case "Couple Name":
                    family.SetHead(field_value);
                    break;
                case "Family Phone":
                case "Head Of House Phone":
                    family.AddPhoneNumber(field_value);
                    break;
                case "Family Address":
                    //Some special logice to split the address and just grab street addresses
                    string[] tokens = field_value.Split(new[] { " Spanish Fork, Utah 84660" }, StringSplitOptions.None);
                    field_value = tokens[0].ToUpper().Trim();

                    foreach (var pair in this.directions)
                    {
                        field_value = field_value.Replace(pair.Key, pair.Value);
                    }

                    family.SetAddress(field_value);
                    break;
                case "Child Name":
                    family.AddChild(field_value);
                    break;
                default:
                    throw new Exception("Invalid column name: " + header);
            }
        }

        private Boolean IsDesiredField(int index)
        {
            return this.desired_fields.Contains(this.headers[index].Trim());
        }

        //https://www.c-sharpcorner.com/UploadFile/muralidharan.d/how-to-create-word-document-using-C-Sharp/
        public void CreateDocument(String directory)
        {
            this.ParseCsv(directory);

            //Create an instance for word app
            this.winword = new Word.Application();

            //Set animation status for word application
            winword.ShowAnimation = false;

            //Set status for word application is to be visible or not.
            winword.Visible = false;

            //Create a missing variable for missing value
            object missing = System.Reflection.Missing.Value;

            //Create a new document
            this.document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            //adding text to document
            document.Content.SetRange(0, 0);
            document.Content.Text = "This is test document " + Environment.NewLine;

            //Add paragraph with Heading 1 style
            Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
            object styleHeading1 = "Heading 1";
            para1.Range.set_Style(ref styleHeading1);
            para1.Range.Text = "Para 1 text";
            para1.Range.InsertParagraphAfter();

            //Add paragraph with Heading 2 style
            Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
            object styleHeading2 = "Heading 2";
            para2.Range.set_Style(ref styleHeading2);
            para2.Range.Text = "Para 2 text";
            para2.Range.InsertParagraphAfter();

            
        }

        public void SaveDocument(string _filename)
        {
            //Create a missing variable for missing value
            object missing = System.Reflection.Missing.Value;

            //Save the document
            object filename = _filename;
            document.SaveAs2(ref filename);
            document.Close(ref missing, ref missing, ref missing);
            document = null;
            winword.Quit(ref missing, ref missing, ref missing);
            winword = null;
        }

        public bool IsSuccessful()
        {
            return this.success;
        }
    }
}
