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
    public partial class Form_Report : Form
    {
        public Form_Report()
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
        private void button1_Click(object sender, EventArgs e)
        {
            
            DateTime iDate1 = dateTimePicker1.Value;
            DateTime iDate2 = dateTimePicker2.Value;

                int a = Int32.Parse(iDate1.ToString("yyyyMMdd"));
                int b = Int32.Parse(iDate2.ToString("yyyyMMdd"));
                if(b<a)
                {
                    MessageBox.Show("Hãy chọn khoảng thời gian khác");
                }    
                else
                {
                    String strdate1 = iDate1.ToString("yyyy-MM-dd");
                    String strdate2 = iDate2.ToString("yyyy-MM-dd");
                    //String strdate1 = iDate1.ToString("ddMMyyyy");
                    //String strdate2 = iDate2.ToString("ddMMyyyy");
                    string query ="";
                    string query2 = "";
                    DataTable dt = new DataTable();
                   if(comboBox1.SelectedIndex == -1)
                    {
                        query = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, Name  , phone, IdDoiBong2  from (select [DatSan].idDatsan ,[DatSan].idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, IdDoiBong1,  IdDoiBong2  from [DatSan] inner join [SanBanhNho] on DatSan.idSannho = SanBanhNho.idSannho  where [SanBanhNho].idSan = '{0}' ) as tb_show inner join DoiBong on [tb_show].IdDoiBong1 = [DoiBong].idDoibong", ClassMain.nvStatic.idSanbong);
                        query2 = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, tb.Name  as [Tên đội 1], tb.phone as[Liên hệ D1] , IdDoiBong2, [DoiBong].Name as  [Tên đội 2], [DoiBong].phone as[Liên hệ] from ({0}) as tb inner join [DoiBong]  on tb.IdDoiBong2 = [DoiBong].idDoibong  where CAST(Ngay as date) >= '{1}' and CAST(Ngay as date) <= '{2}' ", query, strdate1, strdate2);

                    }
                   else
                    {
                        query = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, Name  , phone, IdDoiBong2  from (select [DatSan].idDatsan ,[DatSan].idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, IdDoiBong1,  IdDoiBong2  from [DatSan] inner join [SanBanhNho] on DatSan.idSannho = SanBanhNho.idSannho  where [SanBanhNho].idSan = '{0}' ) as tb_show inner join DoiBong on [tb_show].IdDoiBong1 = [DoiBong].idDoibong", ClassMain.nvStatic.idSanbong);
                        query2 = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, tb.Name  as [Tên đội 1], tb.phone  as[Liên hệ D1] , IdDoiBong2, [DoiBong].Name as  [Tên đội 2], [DoiBong].phone as [Liên hệ] from ({0}) as tb inner join [DoiBong]  on tb.IdDoiBong2 = [DoiBong].idDoibong  where CAST(Ngay as date) >= '{1}' and CAST(Ngay as date) <= '{2}' and ( IdDoiBong1 = '{3}' or IdDoiBong2 = '{3}')", query, strdate1, strdate2,comboBox1.SelectedItem.ToString());

                    }
                    
                    conn.Open();

                    SqlCommand com = new SqlCommand(query2, conn);
                    com.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    int sum = 0; 
                        string query3 = String.Format("select sum(Gia) from ({0}) as tbs",query2);
                    SqlCommand com2 = new SqlCommand(query3, conn);
                
                using (DbDataReader reader = com2.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                textBox1.Text = reader.GetValue(0).ToString();
                            }
                            reader.Dispose();
                        }
                    }

                   
                    conn.Close();

                }    
            


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = comboBox2.SelectedIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            
        }
    }
}
