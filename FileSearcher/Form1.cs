using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSearcher
{
    public partial class Form1 : Form
    {
        public static List<string> strarr = new List<string>();
        private static void findFile(DirectoryInfo dirinfo, string reg,List<string> strarr)
        {
            Regex rgx = new Regex(reg);
            try
            {
                foreach (FileInfo file in dirinfo.GetFiles())
                {
                    if (rgx.IsMatch(file.Name))
                    {
                        strarr.Add(file.Name);
                    }
                }
                foreach (DirectoryInfo subDir in dirinfo.GetDirectories())
                {
                    findFile(subDir, reg,strarr);
                }
            }
            catch
            {
                return;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            strarr.Clear();
            string str = maskedTextBox1.Text;
            DirectoryInfo dir = new DirectoryInfo(@"C: \Users\a_kurmasheva\Desktop");
            findFile(dir, str,strarr);
            MessageBox.Show("finished search");
            listBox1.DataSource = null;
            listBox1.DataSource = strarr;

        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Form2 oFrm2 = new Form2();
            oFrm2.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
