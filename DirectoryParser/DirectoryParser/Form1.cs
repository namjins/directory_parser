using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryParser
{
    public partial class Form1 : Form
    {
        private string file;

        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void select_file_btn_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.  
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Cursor Files|*.csv";
            openFileDialog1.Title = "Select a CSV File";

        // Show the Dialog.  
        // If the user clicked OK in the dialog and  
        // a .csv file was selected, open it.  
       // https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-open-files-using-the-openfiledialog-component
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.file = openFileDialog1.FileName;
                //TODO validate file is csv
                //TODO update field text to show file path.
                //TODO show save button
            }
        }

        private void save_file_Click(object sender, EventArgs e)
        {
            DocumentBuilder db = new DocumentBuilder();

            try
            {
                db.CreateDocument(this.file);
            }
            catch (Exception ex)
            {
                //TODO set error label = ex.Message
            }

            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            if (db.IsSuccessful()) { 
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Word Document|*.docx";
                saveFileDialog1.Title = "Save the Word Document";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.  
                if (saveFileDialog1.FileName != "")
                {
                    db.SaveDocument(saveFileDialog1.FileName);
                }
            }
        }
    }
}
