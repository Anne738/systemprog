namespace lab7_8
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCreateFolder;
        private System.Windows.Forms.Button btnCreateFile;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtPath = new TextBox();
            listBox1 = new ListBox();
            btnRefresh = new Button();
            btnBack = new Button();
            btnDelete = new Button();
            btnCreateFolder = new Button();
            btnCreateFile = new Button();
            SuspendLayout();
            // 
            // txtPath
            // 
            txtPath.Location = new Point(72, 13);
            txtPath.Name = "txtPath";
            txtPath.ReadOnly = true;
            txtPath.Size = new Size(488, 27);
            txtPath.TabIndex = 0;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(72, 49);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(571, 324);
            listBox1.TabIndex = 1;
            listBox1.DoubleClick += listBox1_DoubleClick;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(36, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(30, 30);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "↻";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(0, 10);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(30, 30);
            btnBack.TabIndex = 3;
            btnBack.Text = "⬅︎";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(649, 13);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(30, 30);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "✖";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnCreateFolder
            // 
            btnCreateFolder.Location = new Point(567, 13);
            btnCreateFolder.Name = "btnCreateFolder";
            btnCreateFolder.Size = new Size(40, 30);
            btnCreateFolder.TabIndex = 5;
            btnCreateFolder.Text = "📁";
            btnCreateFolder.UseVisualStyleBackColor = true;
            btnCreateFolder.Click += btnCreateFolder_Click;
            // 
            // btnCreateFile
            // 
            btnCreateFile.Location = new Point(613, 13);
            btnCreateFile.Name = "btnCreateFile";
            btnCreateFile.Size = new Size(30, 30);
            btnCreateFile.TabIndex = 6;
            btnCreateFile.Text = "⎙";
            btnCreateFile.UseVisualStyleBackColor = true;
            btnCreateFile.Click += btnCreateFile_Click;
            // 
            // Form1
            // 
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(683, 395);
            Controls.Add(btnCreateFile);
            Controls.Add(btnCreateFolder);
            Controls.Add(btnDelete);
            Controls.Add(btnBack);
            Controls.Add(btnRefresh);
            Controls.Add(listBox1);
            Controls.Add(txtPath);
            Name = "Form1";
            Text = "Файловий Менеджер";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
