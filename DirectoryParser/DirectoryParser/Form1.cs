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
            error_msg.Visible = false;
            feedback_label.Visible = false;
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
                textBox1.Text = openFileDialog1.FileName;
                this.file = textBox1.Text;
                save_file.Visible = true;
                feedback_label.Visible = false;

                //TODO validate file is csv
                //TODO show save button
            }
        }

        private void save_file_Click(object sender, EventArgs e)
        {
            save_file.Enabled = false;
            feedback_label.Text = "Processing...";
            feedback_label.Visible = true;
            DocumentBuilder db = new DocumentBuilder();

            try
            {
                db.CreateDocument(this.file);
            }
            catch (Exception ex)
            {
                save_file.Visible = false;
                error_msg.Text = ex.Message;
                error_msg.Visible = true;
                //TODO set error label = ex.Message
            }

            feedback_label.Visible = false;

            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            if (db.IsSuccessful()) {
                feedback_label.Text = "Done";
                feedback_label.Visible = true;
                try { 
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Word Document|*.docx";
                    saveFileDialog1.Title = "Save the Word Document";
                    saveFileDialog1.ShowDialog();

                    // If the file name is not an empty string open it for saving.  
                    if (saveFileDialog1.FileName != "")
                    {
                        db.SaveDocument(saveFileDialog1.FileName);
                    }

                    feedback_label.Text = "File Saved Successfully";
                    feedback_label.Visible = true;
                }
                catch (Exception ex)
                {
                    feedback_label.Visible = false;
                    error_msg.Text = "Unable to save file";
                    error_msg.Visible = true;
                }
            }

            save_file.Enabled = true;
            save_file.Visible = false;
            textBox1.Text = "Click SELECT FILE to start";

        }
    }
}
