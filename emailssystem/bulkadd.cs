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
    public partial class bulkadd : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public bulkadd()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= emails.db");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try {
                if (e.ColumnIndex == 6)
                {
                    foreach (DataGridViewCell oneCell in dataGridView1.SelectedCells)
                    {
                        if (oneCell.Selected)
                            dataGridView1.Rows.RemoveAt(oneCell.RowIndex);
                    }


                }
            }
            catch (Exception ex) { MessageBox.Show("لا يمكن مسح صف فارغ"); }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = Clipboard.GetText();

            string[] lines = s.Replace("\n", "").Split('\r');

            dataGridView1.Rows.Add(lines.Length - 1);
            string[] fields;
            int row = 0;
            int col = 0;

            foreach (string item in lines)
            {
                fields = item.Split('\t');
                foreach (string f in fields)
                {
                    Console.WriteLine(f);
                    dataGridView1[col, row].Value = f;
                    col++;
                }
                row++;
                col = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //dataGridView1.CurrentRow.Cells[0].Value

                //dataGridView1.Rows[1].Cells[0].Value
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    qu = "INSERT INTO studb (Name,deb,user,pass,level,natid) VALUES ($Name,$deb,$user,$pass,$level,$natid)";
                    cmd = new SqliteCommand(qu, con);
                    cmd.Parameters.AddWithValue("$Name", dataGridView1.Rows[i].Cells[0].Value);
                    cmd.Parameters.AddWithValue("$deb", dataGridView1.Rows[i].Cells[1].Value);
                    cmd.Parameters.AddWithValue("$user", dataGridView1.Rows[i].Cells[2].Value);
                    cmd.Parameters.AddWithValue("$pass", dataGridView1.Rows[i].Cells[3].Value);
                    cmd.Parameters.AddWithValue("$level", dataGridView1.Rows[i].Cells[4].Value);
                    cmd.Parameters.AddWithValue("$natid", dataGridView1.Rows[i].Cells[5].Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                


                dataGridView1.Rows.Clear();
                MessageBox.Show("تم تحميل البيانات بنجاح");
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
