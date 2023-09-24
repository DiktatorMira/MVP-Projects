using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Authors_and_books
{
    public partial class EditAuthor : Form
    {
        public string oldname { get; set; }
        public string newname { get; set; }
        public EditAuthor()
        {
            InitializeComponent();
            textBox1.Text = oldname;
            textBox1.Select();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            newname = textBox1.Text;
            Close();
        }
        private void button2_Click(object sender, EventArgs e) => Close();
    }
}
