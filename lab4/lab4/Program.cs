using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace lab4
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); 
        }
    }
    public partial class Form1 : Form
    {
        private CancellationTokenSource cts;

        //public Form1()
        //{
        //    InitializeComponent();
        //}

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;

            cts = new CancellationTokenSource();

            IProgress<int> onChangeProgress = new Progress<int>((i) =>
            {
                label1.Text = $"Прогрес: {i}%";
                progressBar1.Value = -i;
            });

            try
            {
                int result = await Process(100, onChangeProgress, cts.Token);

                if (result == -1)
                    label1.Text = "Операцію скасовано";
                else
                    label1.Text = $"Готово: {result}%";

                
            }
            finally
            {
                button1.Enabled = true;
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }

        Task<int> Process(int count, IProgress<int> ChangeProgressBar, CancellationToken cancellTocken)
        {
            return Task.Run(() =>
            {
                try
                {
                    int i;
                    for (i = 1; i <= count; i++)
                    {
                        cancellTocken.ThrowIfCancellationRequested();

                        ChangeProgressBar.Report(-i);
                        Thread.Sleep(100);
                    }
                    return i;
                }
                catch (OperationCanceledException)
                {
                    return 1;
                }
            }, cancellTocken);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                MessageBox.Show("botton 1", "Info");
            else if (radioButton2.Checked)
                MessageBox.Show("botton 2", "Info");
            else if (radioButton3.Checked)
                MessageBox.Show("botton 3", "Info");
        }
    }
}
