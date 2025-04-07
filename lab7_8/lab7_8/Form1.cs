using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace lab7_8
{
    public partial class Form1 : Form
    {
        private string currentPath = "";

        public Form1()
        {
            InitializeComponent();
            currentPath = Environment.CurrentDirectory; // поточна папка запуску
            LoadDirectory(currentPath);
        }

        private void LoadDirectory(string path)
        {
            try
            {
                listBox1.Items.Clear();
                txtPath.Text = path;
                currentPath = path;

                DirectoryInfo dir = new DirectoryInfo(path);

                foreach (var d in dir.GetDirectories())
                    listBox1.Items.Add("[DIR] " + d.Name);

                foreach (var f in dir.GetFiles())
                    listBox1.Items.Add(f.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string selected = listBox1.SelectedItem.ToString();
            string fullPath;

            if (selected.StartsWith("[DIR] "))
            {
                selected = selected.Substring(6);
                fullPath = Path.Combine(currentPath, selected);
                LoadDirectory(fullPath);
            }
            else
            {
                fullPath = Path.Combine(currentPath, selected);
                try
                {
                    Process.Start(new ProcessStartInfo(fullPath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не вдалося відкрити файл: " + ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDirectory(currentPath); //оновити вміст поточної папки
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (Directory.GetParent(currentPath) != null)
            {
                LoadDirectory(Directory.GetParent(currentPath).FullName);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            string selected = listBox1.SelectedItem.ToString();
            string fullPath = selected.StartsWith("[DIR] ")
                ? Path.Combine(currentPath, selected.Substring(6))
                : Path.Combine(currentPath, selected);

            try
            {
                if (selected.StartsWith("[DIR] "))
                    Directory.Delete(fullPath, true);
                else
                    File.Delete(fullPath);

                LoadDirectory(currentPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка видалення: " + ex.Message);
            }
        }

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            string folderName = Prompt.ShowDialog("Введіть назву папки:", "Створити папку");
            if (!string.IsNullOrWhiteSpace(folderName))
            {
                string fullPath = Path.Combine(currentPath, folderName);
                try
                {
                    Directory.CreateDirectory(fullPath);
                    LoadDirectory(currentPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка створення папки: " + ex.Message);
                }
            }
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            string fileName = Prompt.ShowDialog("Введіть назву файлу:", "Створити файл");
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                string fullPath = Path.Combine(currentPath, fileName);
                try
                {
                    File.Create(fullPath).Close();
                    LoadDirectory(currentPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка створення файлу: " + ex.Message);
                }
            }
        }
    }

    //клас для діалогового вікна вводу
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                Text = caption,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 340 };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340 };
            Button confirmation = new Button() { Text = "ОК", Left = 270, Width = 90, Top = 80, DialogResult = DialogResult.OK };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
        }
    }
}

