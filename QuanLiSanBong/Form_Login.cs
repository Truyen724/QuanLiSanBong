using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;
using QuanLiSanBong.Object;
namespace QuanLiSanBong
{
    public partial class Form_Login : Form
    {
        
        public Form_Login()
        {
            InitializeComponent();
            ketnoi();
            textBox1.Text = "0394511955";
            textBox2.Text = "ntt2432001";
            MessageBox.Show(ClassMain.ConnetionString);
        }
        string ConnetionString = ClassMain.ConnetionString;
        SqlConnection conn = new SqlConnection();
        public void ketnoi()
        {
            try
            {
                conn.ConnectionString = ConnetionString;

            }
            catch
            {
                MessageBox.Show("Kết nối thất bại");
            }
        }
            private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" & textBox2.Text != "")
            {
                conn.Open();
                String query = String.Format("Select * from NhanVienSanBong where phone = '{0}' and Matkhau = '{1}'", textBox1.Text, textBox2.Text);
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandText = query;

                using (DbDataReader reader = com.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int idNV = unchecked((int)Convert.ToInt64(reader.GetValue(0)));
                            String TenNV = reader.GetString(1);
                            
                            int idSan = unchecked((int)Convert.ToInt64(reader.GetValue(2)));
                            String TenSan = reader.GetString(3);
                            String phone = reader.GetString(4);
                            String Matkhau = reader.GetString(5);

                            ClassMain.nvStatic = new NhanvienSB(idNV, TenNV, idSan, TenSan, phone, Matkhau);
                        }    
                        ClassMain.Quyen = 1;
                        this.Hide();
                        Form1 f = new Form1();
                        f.ShowDialog();
                        reader.Dispose();
                        this.Close();   
                   

                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu sai");
                    }    
                }
                String query2 = String.Format("Select * from DoiBong where phone = '{0}' and Matkhau = '{1}'", textBox1.Text, textBox2.Text);
                com.CommandText = query2;
                using (DbDataReader reader = com.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Dispose();
                        ClassMain.Quyen = 2;
                        this.Hide();
                        Form1 f = new Form1();
                        f.Show();
                        
                        this.Close();
                        
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu sai");
                    }
                }

                conn.Close();

            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
