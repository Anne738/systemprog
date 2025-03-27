namespace lab5
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TreeView treeView1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();

            // treeView1
            this.treeView1.Location = new System.Drawing.Point(10, 10);
            this.treeView1.Size = new System.Drawing.Size(400, 500);
            this.treeView1.Name = "treeView1";

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(450, 530);
            this.Controls.Add(this.treeView1);
            this.Name = "Form1";
            this.Text = "Lab 5 - Цифровий фотоапарат";
            this.ResumeLayout(false);
        }
    }
}
