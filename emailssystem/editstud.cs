using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace emailssystem
{
    public partial class editstud : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public editstud()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= emails.db");
            var frm = Application.OpenForms["viewstd"] as viewstd;
            label7.Text = frm.label4.Text;
            loaddeps();
            loaddata();
            
        }

        private void loaddeps()
        {
            try
            {

                con.Open();
                cmd = new SqliteCommand("SELECT DISTINCT deb FROM studb", con);
                string get = "";
                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        get = read.GetString(0);
                        comboBox2.Items.Add(get);
                    }
                    con.Close();
                }
                if (comboBox2.Items.Count < 5) {
                    comboBox2.Items.Add("اللغة العربية");
                    comboBox2.Items.Add("اللغة الانجليزية");
                    comboBox2.Items.Add("اللغة الفرنسية");
                    comboBox2.Items.Add("علم النفس");
                    comboBox2.Items.Add("جغرافيا");
                    comboBox2.Items.Add("تاريخ");
                    comboBox2.Items.Add("العلوم البيولوجية");
                    comboBox2.Items.Add("الفيزياء");
                    comboBox2.Items.Add("الكيمياء");
                    comboBox2.Items.Add("الرياضيات");
                    comboBox2.Items.Add("حاسب الى");
                    comboBox2.Items.Add("اساسي عربي");
                    comboBox2.Items.Add("اساسي انجليزي");
                    comboBox2.Items.Add("اساسي مواد اجتماعية");
                    comboBox2.Items.Add("اساسي علوم");
                    comboBox2.Items.Add("انجليزي خاص");
                    comboBox2.Items.Add("علوم خاص");
                    comboBox2.Items.Add("رياضيات خاص");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void loaddata()
        {
            con.Open();
            qu = "SELECT * FROM studb WHERE natid=$id";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$id", label7.Text);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    textBox1.Text = read.GetString(0);
                    comboBox2.Text = read.GetString(1);
                    textBox3.Text = read.GetString(2);
                    textBox4.Text = read.GetString(3);
                    comboBox1.Text = read.GetString(4);
                    textBox2.Text = read.GetString(5);
                }

            }
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void editstud_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            qu = "UPDATE studb SET Name=$nam,deb=$deb,user=$user,pass=$pass,level=$lev,natid=$natid WHERE natid=$id";
            cmd = new SqliteCommand(qu, con);
            cmd.Parameters.AddWithValue("$id", label7.Text);
            cmd.Parameters.AddWithValue("$nam", textBox1.Text);
            cmd.Parameters.AddWithValue("$deb", comboBox2.Text);
            cmd.Parameters.AddWithValue("$user", textBox3.Text);
            cmd.Parameters.AddWithValue("$pass", textBox4.Text);
            cmd.Parameters.AddWithValue("$lev", comboBox1.Text);
            cmd.Parameters.AddWithValue("$natid", textBox2.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            var appo = Application.OpenForms["viewstd"] as viewstd;
            appo.loaddata();

            Close();
        }
    }
}
