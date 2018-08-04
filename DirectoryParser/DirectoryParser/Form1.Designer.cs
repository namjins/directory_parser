namespace DirectoryParser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.select_file_btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.save_file = new System.Windows.Forms.Button();
            this.error_msg = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.feedback_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // select_file_btn
            // 
            this.select_file_btn.Location = new System.Drawing.Point(302, 33);
            this.select_file_btn.Name = "select_file_btn";
            this.select_file_btn.Size = new System.Drawing.Size(75, 23);
            this.select_file_btn.TabIndex = 0;
            this.select_file_btn.Text = "Select File";
            this.select_file_btn.UseVisualStyleBackColor = true;
            this.select_file_btn.Click += new System.EventHandler(this.select_file_btn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // save_file
            // 
            this.save_file.Location = new System.Drawing.Point(148, 64);
            this.save_file.Name = "save_file";
            this.save_file.Size = new System.Drawing.Size(149, 23);
            this.save_file.TabIndex = 3;
            this.save_file.Text = "Save Directory";
            this.save_file.UseVisualStyleBackColor = true;
            this.save_file.Visible = false;
            this.save_file.Click += new System.EventHandler(this.save_file_Click);
            // 
            // error_msg
            // 
            this.error_msg.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.error_msg.AutoSize = true;
            this.error_msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error_msg.ForeColor = System.Drawing.Color.Red;
            this.error_msg.Location = new System.Drawing.Point(55, 90);
            this.error_msg.MaximumSize = new System.Drawing.Size(327, 20);
            this.error_msg.MinimumSize = new System.Drawing.Size(327, 20);
            this.error_msg.Name = "error_msg";
            this.error_msg.Size = new System.Drawing.Size(327, 20);
            this.error_msg.TabIndex = 4;
            this.error_msg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.error_msg.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(59, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(244, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Click SELECT FILE to start";
            // 
            // feedback_label
            // 
            this.feedback_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.feedback_label.AutoSize = true;
            this.feedback_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedback_label.ForeColor = System.Drawing.Color.Green;
            this.feedback_label.Location = new System.Drawing.Point(55, 9);
            this.feedback_label.MaximumSize = new System.Drawing.Size(327, 20);
            this.feedback_label.MinimumSize = new System.Drawing.Size(327, 20);
            this.feedback_label.Name = "feedback_label";
            this.feedback_label.Size = new System.Drawing.Size(327, 20);
            this.feedback_label.TabIndex = 5;
            this.feedback_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.feedback_label.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 150);
            this.Controls.Add(this.feedback_label);
            this.Controls.Add(this.error_msg);
            this.Controls.Add(this.save_file);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.select_file_btn);
            this.Name = "Form1";
            this.Text = "Directory Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button select_file_btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button save_file;
        private System.Windows.Forms.Label error_msg;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label feedback_label;
    }
}

