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
    public partial class Form2 : Form
    {
        public static List<string> strarr = new List<string>();
        private static void findFile(DirectoryInfo dirinfo, string reg, List<string> strarr, DateTime created1, DateTime created2)
        {
            Regex rgx = new Regex(reg);
            try
            {
                foreach (FileInfo file in dirinfo.GetFiles())
                {
                    DateTime creationTime = File.GetCreationTime(file.FullName);
                    if (rgx.IsMatch(file.Name) && created1<creationTime && creationTime<created2)
                    {
                        strarr.Add(file.Name);
                    }
                }
                foreach (DirectoryInfo subDir in dirinfo.GetDirectories())
                {
                    findFile(subDir, reg, strarr, created1, created2);
                }
            }
            catch
            {
                return;
            }
        }
        public Form2()
        {
            InitializeComponent();
            label1.Font = new Font("TimesNewRoman", 10, FontStyle.Bold);
            label5.Font = new Font("TimesNewRoman", 10, FontStyle.Bold);
            label4.Font = new Font("TimesNewRoman", 10, FontStyle.Bold);
            label6.Font = new Font("TimesNewRoman", 10, FontStyle.Bold);


        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath wantedshape = new GraphicsPath();
            Point[] points = new Point[] { new Point { X = 0, Y = 0 }, new Point { X = 420, Y = 20 }, new Point { X = 20, Y = 420 } };
            wantedshape.AddEllipse(0, 0, this.Width, this.Height);
            this.Region = new Region(wantedshape);
            this.BackColor = System.Drawing.Color.CornflowerBlue;
        }
        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 oFrm1 = new Form1();
            oFrm1.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string foldername = this.folderBrowserDialog1.SelectedPath;
                textBox1.Text = foldername;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            strarr.Clear();
            string name = textBox4.Text;
            string path = textBox1.Text;
            DateTime created1 = Convert.ToDateTime(textBox2.Text);
            DateTime created2 = Convert.ToDateTime(textBox3.Text);
            DirectoryInfo dir = new DirectoryInfo(path);
            findFile(dir, name, strarr,created1,created2);
            MessageBox.Show("finished search");
            listBox1.DataSource = null;
            listBox1.DataSource = strarr;
        }
    }
}
