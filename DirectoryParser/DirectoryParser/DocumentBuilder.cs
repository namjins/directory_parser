using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                        if (string.IsNullOrEmpty(this.CleanField(fields[i])))
                        {
                            continue;
                        }

                        if (this.IsDesiredField(i))
                        {
                            this.ProcessField(family, this.CleanField(fields[i]), this.CleanField(this.headers[i]));
                        }

                    }

                    if (!string.IsNullOrEmpty(family.GetHead()))
                    {
                        this.familyList.Add(family);
                    }
                }
            }

        }

        private String CleanField(String field)
        {
            field = Regex.Replace(field, @"\t|\n|\r", "");
            return field.Trim();
        }

        private void ProcessField(Family family, string field_value, string header)
        {
            switch (header)
            {
                case "Couple Name":
                    //Upper case the surname
                    string[] coupleNameTokensStepOne = field_value.Split(new[] { "," }, StringSplitOptions.None);
                    coupleNameTokensStepOne[0] = coupleNameTokensStepOne[0].ToUpper();

                    //split on ambersand
                    string[] coupleNameTokensStepTwo = coupleNameTokensStepOne[1].Split(new[] { " & " }, StringSplitOptions.None);

                    // Check if entry is a couple or individual.  Remove middle names from record
                    if (coupleNameTokensStepTwo.Length > 1)
                    {
                        string[] headTokens = coupleNameTokensStepTwo[0].Trim().Split(new[] { " " }, StringSplitOptions.None);
                        string[] spouseTokens = coupleNameTokensStepTwo[1].Trim().Split(new[] { " " }, StringSplitOptions.None);
                        coupleNameTokensStepOne[1] = headTokens[0] + " & " + spouseTokens[0];
                    }
                    else
                    {
                        string[] headTokens = coupleNameTokensStepTwo[0].Trim().Split(new[] { " " }, StringSplitOptions.None);
                        coupleNameTokensStepOne[1] = headTokens[0];
                    }

                    field_value = coupleNameTokensStepOne[0] + ", " + coupleNameTokensStepOne[1];
                    family.SetHead(field_value);
                    break;
                case "Family Phone":
                case "Head Of House Phone":
                    family.AddPhoneNumber(field_value);
                    break;
                case "Family Address":
                    //Some special logic to split the address and just grab street addresses
                    string[] familyAddressTokens = field_value.Split(new[] { " Spanish Fork, Utah 84660" }, StringSplitOptions.None);
                    field_value = this.CleanField(familyAddressTokens[0].ToUpper());

                    foreach (var pair in this.directions)
                    {
                        field_value = field_value.Replace(pair.Key, pair.Value);
                    }

                    family.SetAddress(field_value);
                    break;
                case "Child Name":
                    //Some special logic to split a child's name so we only receive the first name.
                    string[] childNameTokens = field_value.Split(new[] { "," }, StringSplitOptions.None);
                    childNameTokens = childNameTokens[1].Split(new[] { " " }, StringSplitOptions.None);
                    field_value = childNameTokens[1];
                    family.AddChild(this.CleanField(field_value));
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
            document.Range().Font.Name = "Cambria";
            document.Range().Font.Size = 8;
            document.PageSetup.TopMargin = 25f;
            document.PageSetup.BottomMargin = 25f;
            document.PageSetup.LeftMargin = 25f;
            document.PageSetup.RightMargin = 25f;
            int start = 0;
            int end = 0;
            string header_text = "Mount Loafer Neighborhood Directory " + DateTime.Now.ToString("MMMM d, yyyy") + Environment.NewLine;
            this.InsertMultiFormatDirectoryParagraph(missing, header_text, ref start, ref end);

            //iterate over each entry
            foreach (Family family  in this.familyList)
            {
                this.InsertMultiFormatParagraph(family, missing, ref start, ref end);
            }

            Word.Range twoColumns = this.document.Range(35, end);
//            twoColumns.PageSetup.TextColumns.SetCount(2);
            this.success = true;
        }

        //https://stackoverflow.com/questions/11564073/how-do-i-write-bold-text-to-a-word-document-programmatically-without-bolding-the
        private void InsertMultiFormatParagraph(Family family, object missing, ref int start, ref int end, int piSize = 8, int piSpaceAfter = 1)
        {

            Word.Paragraph para = this.document.Content.Paragraphs.Add(ref missing);

            string head = this.CleanField(family.GetHead());
            string children = this.CleanField(family.GetChildren());
            string phone = this.CleanField(family.GetPhoneNumbers());
            string address = this.CleanField(family.GetAddress());

            // The modifier of +1 or +2 is to compensate for additional spacing needs for the phone and address.
            int phone_length = phone.Length + 1; 
            int address_length = address.Length + 2;


            para.Range.Text = head + children + " " + phone + " " + address;
            para.Range.Font.Size = 8;
            para.Format.LineSpacing = 9;

            //head formatting
            end = start + head.Length;
            object objHeadStart = start;
            object objHeadEnd = end;
            Word.Range rngHeadBold = this.document.Range(ref objHeadStart, ref objHeadEnd);
            rngHeadBold.Bold = 1;

            //phone formatting
            start = end + children.Length;
            end = start + phone_length;

            object objPhoneStart = start;
            object objPhoneEnd = end;
            Word.Range rngPhoneBold = this.document.Range(ref objPhoneStart, ref objPhoneEnd);
            rngPhoneBold.Bold = 1;

            para.Range.InsertParagraphAfter();

            start = end + address_length;
        }

        private void InsertMultiFormatDirectoryParagraph(object missing, string text, ref int start, ref int end)
        {
            start = 0;
            end = 35;
            Word.Paragraph para = this.document.Content.Paragraphs.Add(ref missing);

            para.Range.Text = text;
            // Explicitly set this to "not bold"
            para.Range.Font.Bold = 0;
            para.Range.Font.Size = 9;
            para.Format.SpaceAfter = 1;

            object objStart = start;
            object objEnd = end;

            Word.Range rngBold = this.document.Range(ref objStart, ref objEnd);
            rngBold.Bold = 1;

            para.Range.InsertParagraphAfter();

            start = text.Length;
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
