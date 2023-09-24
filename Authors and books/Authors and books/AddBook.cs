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
    public partial class AddBook : Form
    {
        public string authorname { get; set; }
        public string bookname { get; set; }
        public AddBook()
        {
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            authorname = textBox1.Text;
            bookname = textBox2.Text;
            Close();
        }
        private void button2_Click(object sender, EventArgs e) => Close();
    }
}
