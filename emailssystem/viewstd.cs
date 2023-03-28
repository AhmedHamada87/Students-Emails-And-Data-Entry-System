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
    public partial class viewstd : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public viewstd()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= emails.db");
            loaddata();
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
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
           

        }

        public void loaddata()
        {
            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqliteCommand("Select * From studb", con);
            dataGridView1.RowTemplate.Height = 30;
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    dataGridView1.Rows.Add(new object[] {
                    read.GetValue(0),
                    read.GetValue(1),
                    read.GetValue(2),
                    read.GetValue(3),
                    read.GetValue(4),
                    read.GetValue(5),

                    });
                }
            }
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            comboBox2.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text!="") { loaddebament(); }
        }

        private void loaddebament()
        {
            dataGridView1.Rows.Clear();
            con.Open();
            cmd = new SqliteCommand("Select * From studb Where deb=$dep", con);
            cmd.Parameters.AddWithValue("$dep", comboBox2.Text);
            dataGridView1.RowTemplate.Height = 30;
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    dataGridView1.Rows.Add(new object[] {
                    read.GetValue(0),
                    read.GetValue(1),
                    read.GetValue(2),
                    read.GetValue(3),
                    read.GetValue(4),
                    read.GetValue(5),

                    });
                }
            }
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            button1.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            double id;
            if (e.ColumnIndex == 7)
            {
                if (MessageBox.Show("هل أنت متأكد من حذف هذا الطالب؟", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string ida = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
                    dataGridView1.Rows.Clear();
                    qu = "DELETE FROM studb WHERE natid=$ida";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$ida", ida);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loaddata();


                    /*id = Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value);
                    dataGridView1.Rows.Clear();
                    qu = "DELETE FROM studb WHERE natid=$ida";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$ida", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loaddata();*/
                }


            }
            if (e.ColumnIndex == 6)
            {
                string ida = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
                label4.Text = ida;
                editstud edsi = new editstud();
                edsi.ShowDialog();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            try
            {
                if (textBox1.Text == "") { loaddata(); }
                else
                {
                    dataGridView1.Rows.Clear();
                    con.Open();
                    cmd = new SqliteCommand("Select * From studb Where natid Like $se", con);
                    dataGridView1.RowTemplate.Height = 30;
                    cmd.Parameters.AddWithValue("$se", textBox1.Text);
                    using (SqliteDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            dataGridView1.Rows.Add(new object[] {
                    read.GetValue(0),
                    read.GetValue(1),
                    read.GetValue(2),
                    read.GetValue(3),
                    read.GetValue(4),
                    read.GetValue(5),

                    });
                        }
                    }
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                    dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            searchname();
        }

        private void searchname()
        {
            try
            {
                if (textBox2.Text == "") { loaddata(); }
                else
                {
                    dataGridView1.Rows.Clear();
                    con.Open();
                    cmd = new SqliteCommand("Select * From studb Where Name Like $se", con);
                    dataGridView1.RowTemplate.Height = 30;
                    cmd.Parameters.AddWithValue("$se", textBox2.Text);
                    using (SqliteDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            dataGridView1.Rows.Add(new object[] {
                    read.GetValue(0),
                    read.GetValue(1),
                    read.GetValue(2),
                    read.GetValue(3),
                    read.GetValue(4),
                    read.GetValue(5),


                    });
                        }
                    }
                    dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(175, 220, 220);
                    dataGridView1.EnableHeadersVisualStyles = false;
                    dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                    dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void viewstd_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loaddata();
            comboBox2.Text = "";
            button1.Enabled = false;
        }
    }
}
