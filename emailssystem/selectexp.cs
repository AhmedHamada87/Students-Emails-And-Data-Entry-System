using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Excel = Microsoft.Office.Interop.Excel;


namespace emailssystem
{
    public partial class selectexp : Form
    {
        SqliteConnection con;
        SqliteCommand cmd;
        SqliteDataReader dr;
        string qu;
        public selectexp()
        {
            InitializeComponent();
            con = new SqliteConnection("Data Source= emails.db");

        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            exportall();
        }

        private void exportall()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


            string data = String.Empty;

            int i = 0;
            int j = 0;

            using (con)
            {
                con.Open();

                string stm = "SELECT * FROM studb";

                using (SqliteCommand cmd = new SqliteCommand(stm, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read()) // Reading Rows
                        {
                            for (j = 0; j <= rdr.FieldCount - 1; j++) // Looping throw colums
                            {
                                data = rdr.GetValue(j).ToString();
                                xlWorkSheet.Cells[i + 1, j + 1] = data;
                            }
                            i++;
                        }
                    }
                }
                con.Close();
            }

            xlWorkBook.SaveAs("emails.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            MessageBox.Show("تم بنجاح");
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.Diagnostics.Process.Start("explorer", myDocumentsPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fileToCopy = "emails.db";
            string destinationDirectory = "f:\\";
            try {
                File.Copy(fileToCopy, destinationDirectory + Path.GetFileName(fileToCopy));
                MessageBox.Show("تم بنجاح");
                Process.Start(@"f:\");
            }
            catch (Exception ex) { File.Delete("f:\\emails.db"); MessageBox.Show("جارى الاعداد لعمل نقطة استعادة بيانات من فضلك حاول مرة اخرى"); }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            info inf = new info();
            inf.ShowDialog();
        }
    }
    }

