using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emailssystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewstd stud = new viewstd();
            stud.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inputtype inp=new inputtype();
            inp.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectexp exp = new selectexp();
            exp.Show();
        }
    }
}
