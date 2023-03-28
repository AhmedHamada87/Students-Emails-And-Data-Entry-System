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
    public partial class inputtype : Form
    {
        public inputtype()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            indinput inp = new indinput();
            inp.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bulkadd ada = new bulkadd();
            ada.ShowDialog();
        }
    }
}
