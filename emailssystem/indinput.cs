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
using Tulpep.NotificationWindow;

namespace emailssystem
{
    public partial class indinput : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public indinput()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= emails.db");
            loaddeps();
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
                if (comboBox2.Items.Count < 1)
                {
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


        private void indinput_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                qu = "INSERT INTO studb (Name,deb,user,pass,level,natid) VALUES ($Name,$deb,$user,$pass,$level,$natid)";
                cmd = new SqliteCommand(qu, con);
                cmd.Parameters.AddWithValue("$Name", textBox1.Text);
                cmd.Parameters.AddWithValue("$deb", comboBox2.Text);
                cmd.Parameters.AddWithValue("$user", textBox3.Text);
                cmd.Parameters.AddWithValue("$pass", textBox4.Text);
                cmd.Parameters.AddWithValue("$level", comboBox1.Text);
                cmd.Parameters.AddWithValue("$natid", textBox2.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                PopupNotifier pop = new PopupNotifier();
                pop.TitleText = "إعلام";
                pop.ContentText = "تمت إضافة الطالب بنجاح";
                pop.Popup();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
