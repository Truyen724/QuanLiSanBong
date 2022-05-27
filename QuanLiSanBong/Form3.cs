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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            ketnoi();
           
            conn.Open();
            String query2 = String.Format("Select SoSan from SanBanhNho where idSan = '{0}' ", ClassMain.nvStatic.idSanbong);
            SqlCommand com2 = new SqlCommand();
            com2.Connection = conn;
            com2.CommandText = query2;
            using (DbDataReader reader = com2.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int sannho = unchecked((int)Convert.ToInt64(reader.GetValue(0)));
                        comboBox1.Items.Add(sannho);
                        
                    }
                    reader.Dispose();
                }
            }
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            String query = String.Format("Select idSan, TenSan, idNV, phone, adress, GPS from SanBong where idSan = '{0}' ", ClassMain.nvStatic.idSanbong);
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            com.CommandText = query;

            using (DbDataReader reader = com.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = unchecked((int)Convert.ToInt64(reader.GetValue(0)));
                        textBox1.Text = id.ToString();
                        textBox2.Text = reader.GetString(1).ToString();
                        textBox3.Text = reader.GetValue(3).ToString();
                        textBox7.Text = reader.GetValue(2).ToString();
                        textBox6.Text = reader.GetValue(4).ToString();
                        textBox5.Text = reader.GetValue(5).ToString();


                    }
                    reader.Dispose();
                }

            }
            conn.Close();
        }
        
        string ConnetionString = "server=DESKTOP-O41267U;database=119001358_21_01;integrated security=true";
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
            String query = String.Format("Update SanBong Set TenSan =N'{0}', phone = '{1}',idNV ='{2}',adress = '{3}',GPS = '{4}'  where idSan='{5}'", textBox2.Text, textBox3.Text, textBox7.Text, textBox6.Text, textBox5.Text, ClassMain.nvStatic.idSanbong);
            String query2 = String.Format("Update NhanVienSanBong set TenSan = N'{0}'", textBox2.Text);
            conn.Open();
            SqlCommand com2 = new SqlCommand(query2, conn);
            com2.CommandType = CommandType.Text;
            com2.ExecuteNonQuery();
            SqlCommand com = new SqlCommand(query, conn);
            com.CommandType = CommandType.Text;
            com.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Update Thành Công");
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            String query2 = String.Format("Select UuDai, GiaSan from SanBanhNho where idSan = '{0}' and SoSan = '{1}' ", ClassMain.nvStatic.idSanbong, comboBox1.Text);
            SqlCommand com2 = new SqlCommand();
            com2.Connection = conn;
            com2.CommandText = query2;

            using (DbDataReader reader = com2.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        textBox4.Text = reader.GetString(0).ToString();
                        int giasan = unchecked((int)Convert.ToInt64(reader.GetValue(1)));
                        textBox8.Text = giasan.ToString();



                    }
                    reader.Dispose();
                }

            }
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String query2 = String.Format("Update SanBanhNho set UuDai = '{0}', GiaSan ='{1}' where idSan ='{2}' and SoSan ='{3}' ", textBox4.Text, textBox8.Text, ClassMain.nvStatic.idSanbong, comboBox1.Text);
            conn.Open();
            SqlCommand com2 = new SqlCommand(query2, conn);
            com2.CommandType = CommandType.Text;
            com2.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Cập Nhật Thành Công");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int max = 0;
            
            foreach (int i in comboBox1.Items)
            {
                try
                {
                    if (max < i)
                    {
                        max = i;
                    }    
                }
                catch
                    {

                    }
            }
 

                string query2 = String.Format("Insert into SanBanhNho VALUES ((select max(idSannho) from SanBanhNho)+1,'{0}','{1}','{2}','{3}')", ClassMain.nvStatic.idSanbong, max + 1, 0, 0);
                conn.Open();
                SqlCommand com2 = new SqlCommand(query2, conn);
                com2.CommandType = CommandType.Text;
                com2.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công, Hãy chỉnh sửa");
                this.Close();
                conn.Close();

            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
