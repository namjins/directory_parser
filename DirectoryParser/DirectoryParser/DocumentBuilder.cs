using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Word = Microsoft.Office.Interop.Word;


namespace DirectoryParser
{
    class DocumentBuilder
    {
        private bool success = false;
        private Word.Document document;
        private Word.Application winword;



        private String ParseCsv(String directory)
        {
            using (TextFieldParser parser = new TextFieldParser(directory))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        //TODO: Process field
                    }
                }
            }

            return "bob";
        }

        //https://www.c-sharpcorner.com/UploadFile/muralidharan.d/how-to-create-word-document-using-C-Sharp/
        public void CreateDocument(String directory)
        {
            string parsed_file = this.ParseCsv(directory);

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
