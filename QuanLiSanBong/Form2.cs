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
namespace QuanLiSanBong
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            ketnoi();
            textBox1.Text = ClassMain.nvStatic.tennv;
            textBox2.Text = ClassMain.nvStatic.phone;
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
            String query = String.Format("Update NhanVienSanBong Set TenNV ='{0}', phone = '{1}' where idNV='{2}'", textBox1.Text, textBox2.Text,ClassMain.nvStatic.id);
            conn.Open();
            SqlCommand com = new SqlCommand(query, conn);
            com.CommandType = CommandType.Text;
            com.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Update Thành Công");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox4.Text!=ClassMain.nvStatic.matkhau)
            {
                if(textBox5.Text !="" & textBox6.Text!="")
                {
                    if(textBox5.Text == textBox6.Text)
                    {
                        String query = String.Format("Update NhanVienSanBong Set Matkhau ='{0}' where idNV='{1}'", textBox5.Text, ClassMain.nvStatic.id);
                        conn.Open();
                        SqlCommand com = new SqlCommand(query, conn);
                        com.CommandType = CommandType.Text;
                        com.ExecuteNonQuery();
                        conn.Close();
                        MessageBox.Show("Update Thành Công");
                    }
                }    
            }
            else
            {
                MessageBox.Show("Mật khẩu không đúng");
            }    
        }
    }
}
