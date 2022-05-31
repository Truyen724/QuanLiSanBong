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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            ketnoi();
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

        private void Form4_Load(object sender, EventArgs e)
        {
            LayData();
        }
        public void LayData()
        {
            String query = String.Format("Select idDoibong, Name from DoiBong");
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            conn.Open();
            com.CommandText = query;
            using (DbDataReader reader = com.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int iddoibong = unchecked((int)Convert.ToInt64(reader.GetValue(0)));
                        comboBox1.Items.Add(iddoibong);
                        String ten = reader.GetValue(1).ToString();
                        comboBox2.Items.Add(ten.Trim());
                    }
                    reader.Dispose();
                }
            }
            conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            String query = String.Format("Select idDoibong,phone, Adress,Name,Matkhau from DoiBong where idDoibong = '{0}'", comboBox1.Text);
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            conn.Open();
            com.CommandText = query;
            using (DbDataReader reader = com.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        String ten = reader.GetValue(3).ToString();
                        comboBox2.SelectedIndex= comboBox2.FindStringExact(ten.Trim());
                        textBox2.Text = reader.GetValue(1).ToString();
                        textBox3.Text = reader.GetValue(2).ToString();
                    }
                    reader.Dispose();
                }
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String query = String.Format("UPDATE DoiBong " +
                "set phone ='{1}',Adress =N'{2}', Name = N'{3}'" +
                " where idDoibong = '{0}'", comboBox1.Text, textBox2.Text, textBox3.Text, comboBox2.Text);
            SqlCommand com = new SqlCommand(query, conn);
            conn.Open();
            com.ExecuteNonQuery();
            MessageBox.Show("Thanh Cong");
            conn.Close();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            LayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            String query = String.Format("Delete from DoiBong where idDoibong = '{0}'", comboBox1.Text);
            SqlCommand com = new SqlCommand(query, conn);
            conn.Open();
            com.ExecuteNonQuery();
            MessageBox.Show("Thanh Cong");
            conn.Close();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            LayData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String query = String.Format("INSERT INTO DoiBong(idDoibong, phone, Adress, Name, Matkhau) values ((Select max(idDoibong)+1 from DoiBong), '{0}',N'{1}',N'{2}',1)",  textBox8.Text, textBox7.Text,textBox9.Text);
            SqlCommand com = new SqlCommand(query, conn);
            conn.Open();
            com.ExecuteNonQuery();
            MessageBox.Show("Thanh Cong");
            conn.Close();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            LayData();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox2.SelectedIndex;
            comboBox1.SelectedIndex = selectedIndex;
        }
    }
}
