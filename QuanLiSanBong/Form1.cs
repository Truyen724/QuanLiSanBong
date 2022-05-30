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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ketnoi();
            comboBox1.Text = comboBox2.Text = comboBox3.Text = comboBox4.Text =   "00";
            for (int i = 0; i < 24; i++)
            {
                if(i<10)
                {
                    string k = "0" + i.ToString();
                    comboBox1.Items.Add(k);
                    comboBox2.Items.Add(k);

                }    
                else
                {
                    comboBox1.Items.Add(i);
                    comboBox2.Items.Add(i);

                }    
                
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                {
                    string k = "0" + i.ToString();
                    comboBox3.Items.Add(k);
                    comboBox4.Items.Add(k);

                }
                else
                {
                    comboBox3.Items.Add(i);
                    comboBox4.Items.Add(i);

                }    
                
            }
            textBox7.Text = ClassMain.nvStatic.idSanbong.ToString();
            textBox8.Text = ClassMain.nvStatic.tenSan;
            conn.Open();
            String query = String.Format("Select SoSan from SanBanhNho where idSan = '{0}' ", ClassMain.nvStatic.idSanbong);
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            com.CommandText = query;
            using (DbDataReader reader = com.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int sannho = unchecked((int)Convert.ToInt64(reader.GetValue(0)));
                        comboBox9.Items.Add(sannho);
                    }
                    reader.Dispose();
                }    
            }
            comboBox9.DropDownStyle = ComboBoxStyle.DropDownList;
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


            show();
        }
        public void show()
        {
            DateTime iDate = DateTime.Today;
            string strDate = iDate.ToString("yyyyMMdd");
            string query = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, Name  , phone, IdDoiBong2  from (select [DatSan].idDatsan ,[DatSan].idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, IdDoiBong1,  IdDoiBong2  from [DatSan] inner join [SanBanhNho] on DatSan.idSannho = SanBanhNho.idSannho  where [SanBanhNho].idSan = '{0}' ) as tb_show inner join DoiBong on [tb_show].IdDoiBong1 = [DoiBong].idDoibong", ClassMain.nvStatic.idSanbong);
            string query2 = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, tb.Name  as [Tên đội 1], tb.phone as[Liên hệ] , IdDoiBong2, [DoiBong].Name as  [Tên đội 2], [DoiBong].phone as[Liên hệ] from ({0}) as tb inner join [DoiBong]  on tb.IdDoiBong2 = [DoiBong].idDoibong Order by Ngay DESC, TimeStart DESC", query);
            conn.Open();
            SqlCommand com = new SqlCommand(query2, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime iDate;
            iDate = dateTimePicker1.Value;
            string strDate = iDate.ToString("yyyyMMdd");
            //string query = String.Format("select idSan, DatSan.TenSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from DatSan inner join NhanVienSanBong on DatSan.idSan = NhanVienSanBong.idSan where Ngay='{0}' and idSan = 2 ", strDate);
            //string query = "select idSan, DatSan.TenSan from DatSan inner join NhanVienSanBong on DatSan.idSan = NhanVienSanBong.idSan";
            string query = String.Format("select [DatSan].idSan, TenSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from [DatSan] inner join [NhanVienSanBong] on DatSan.IdSan = NhanVienSanBong.IdSan  where [DatSan].Ngay = '{0}' Order by Ngay, TimeStart", strDate);
 
            SqlCommand com = new SqlCommand(query, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime iDate = dateTimePicker2.Value;
            string strDate = iDate.ToString("yyyyMMdd");
            string start = comboBox1.Text + ":" + comboBox3.Text;
            string end = comboBox2.Text + ":" + comboBox4.Text;
            //string query = String.Format("select idSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from DatSan where Ngay = '{3}'  idSan = 1 and (TimeStart<'{0}' and TimeEnd>'{0}') or (TimeStart<'{1}' and TimeEnd>'{1}')  ", "21:00", "22:00", strDate);
            //string query = String.Format("select [DatSan].idSan, TenSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from [DatSan] inner join [NhanVienSanBong] on DatSan.IdSan = NhanVienSanBong.IdSan  where SoSan = '{4}' and [DatSan].IdSan = '{0}' and [DatSan].Ngay = '{1}' and ((TimeStart<='{2}' and TimeEnd>'{2}') or (TimeStart<'{3}' and TimeEnd>='{3}') or  (TimeStart>'{2}' and TimeEnd<'{3}')) Order by Ngay, TimeStart  ", ClassMain.nvStatic.idSanbong, strDate, start, end, comboBox9.Text);
            //string query = String.Format("select [DatSan].idSan, TenSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from [DatSan] inner join [NhanVienSanBong] on DatSan.IdSan = NhanVienSanBong.IdSan  where Ngay='{0}'  ", strDate);
            string query0 = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, Name  , phone, IdDoiBong2  from (select [DatSan].idDatsan ,[DatSan].idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, IdDoiBong1,  IdDoiBong2  from [DatSan] inner join [SanBanhNho] on DatSan.idSannho = SanBanhNho.idSannho  where [SanBanhNho].idSan = '{0}' ) as tb_show inner join DoiBong on [tb_show].IdDoiBong1 = [DoiBong].idDoibong", ClassMain.nvStatic.idSanbong);
            string query = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, tb.Name  as [Tên đội 1], tb.phone as[Liên hệ] , IdDoiBong2, [DoiBong].Name as  [Tên đội 2], [DoiBong].phone as[Liên hệ] from ({0}) as tb inner join [DoiBong]  on tb.IdDoiBong2 = [DoiBong].idDoibong where idSannho ='{1}' and Ngay = '{2}' and ((TimeStart<='{3}' and TimeEnd>'{3}') or (TimeStart<'{4}' and TimeEnd>='{4}') or  (TimeStart>'{3}' and TimeEnd<'{4}')) Order by Ngay, TimeStart ", query0, ClassMain.idSannho, strDate, start, end);

            conn.Open();

            SqlCommand com = new SqlCommand(query, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
            if(dataGridView1.Rows.Count ==0)
            {

                    string query2 = String.Format("Insert into DatSan (idDatsan, idSannho, Ngay, TimeStart, TimeEnd, Gia, IdDoiBong1,IdDoiBong2, SDT_Datsan)" +
                    " VALUES  ((Select max(idDatsan)+1 from DatSan),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", ClassMain.idSannho, strDate, start, end, textBox9.Text, textBox4.Text, textBox5.Text, textBox6.Text);
                    SqlCommand com2 = new SqlCommand(query2, conn);
                    com2.CommandType = CommandType.Text;
                    com2.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công");

                    button1.Enabled = false;

            }    
            else
            {
                MessageBox.Show("Giờ đã bị trùng, các giờ bị trùng hiển thị dưới đây");
            }
            
            conn.Close();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 )
            {

                //Lưu lại dòng dữ liệu vừa kích chọn
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                if(row.Cells[0].Value.ToString()!="")
                {
                    
                    textBox4.Text = row.Cells[8].Value.ToString();
                    textBox1.Text = row.Cells[9].Value.ToString();
                    textBox2.Text = row.Cells[12].Value.ToString();
                    textBox5.Text = row.Cells[11].Value.ToString();
                    textBox6.Text = row.Cells[7].Value.ToString();
                    //textBox9.Text = row.Cells[6].Value.ToString();
                    string[] x = row.Cells[3].Value.ToString().Substring(0, 10).Split('/');
                    int day = 0, month = 0, year = 0;
                    try
                    {
                        day = Int32.Parse(x[0]);
                        month = Int32.Parse(x[1]);
                        year = Int32.Parse(x[2]);
                    }
                    catch (FormatException)
                    {

                    }
                    dateTimePicker2.Value = new DateTime(year, month, day);
                    string[] y = row.Cells[4].Value.ToString().Split(':');

                    comboBox1.SelectedIndex = comboBox1.FindStringExact(y[0]);
                    

                        comboBox3.SelectedIndex = comboBox3.FindStringExact(y[1]);
                    string[] z = row.Cells[5].Value.ToString().Split(':');



                        comboBox2.SelectedIndex = comboBox2.FindStringExact(z[0]);
                    

                        comboBox4.SelectedIndex = comboBox4.FindStringExact("00");


                        comboBox4.SelectedIndex = comboBox4.FindStringExact(z[1]);
                    string strDate = dateTimePicker2.Value.ToString("dd/MM/yyyy");
                    string start = comboBox1.Text + ":" + comboBox3.Text;
                    string end = comboBox2.Text + ":" + comboBox4.Text;
                    //        public DatSan(String idDatSan, String idSanNho, String Ngay, String TimeStart, String TimeEnd, String Gia, String IdDoiBong1, String IdDoiBong2, String SDT_Datsan)

                    ClassMain.datSanstc = new Object.DatSan(row.Cells[0].Value.ToString(), comboBox9.Text, strDate, start, end, textBox9.Text,  textBox4.Text, textBox5.Text, textBox6.Text);
                    //MessageBox.Show(ClassMain.datSanstc.idDatSan +"  "+ ClassMain.datSanstc.idSanNho + "  " + ClassMain.datSanstc.ngay + "  " + ClassMain.datSanstc.timeStart + "  " + ClassMain.datSanstc.timeEnd + "  " + ClassMain.datSanstc.gia + "  " + ClassMain.datSanstc.IdDoiBong1 + "  " + ClassMain.datSanstc.IdDoiBong2 + "  " + ClassMain.datSanstc.sDT_Datsan);              
                    button2.Enabled = true;
                    comboBox9.Text = row.Cells[1].Value.ToString();
                }
                //Đưa dữ liệu vào textbox




            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

                /*DateTime iDate = dateTimePicker1.Value;
                string strDate = iDate.ToString("yyyyMMdd");
                string start = comboBox7.Text + ":" + comboBox5.Text;
                string end = comboBox8.Text + ":" + comboBox6.Text;
            //string query = String.Format("select idSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from DatSan where Ngay = '{3}'  idSan = 1 and (TimeStart<'{0}' and TimeEnd>'{0}') or (TimeStart<'{1}' and TimeEnd>'{1}')  ", "21:00", "22:00", strDate);
            //string query = String.Format("select [DatSan].idSan, TenSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from [DatSan] inner join [NhanVienSanBong] on DatSan.IdSan = NhanVienSanBong.IdSan  where [DatSan].IdSan = '{0}' and Ngay = '{1}' and ((TimeStart<='{2}' and TimeEnd>'{2}') or (TimeStart<'{3}' and TimeEnd>='{3}') or  (TimeStart>'{2}' and TimeEnd<'{3}'))   ", ClassMain.nvStatic.idSanbong, strDate, start, end);
            //string query = String.Format("select [DatSan].idSan, TenSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from [DatSan] inner join [NhanVienSanBong] on DatSan.IdSan = NhanVienSanBong.IdSan  where Ngay='{0}'  ", strDate);
            string query = String.Format("select [DatSan].idSan, TenSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from [DatSan] inner join [NhanVienSanBong] on DatSan.IdSan = NhanVienSanBong.IdSan  where [DatSan].IdSan = '{0}' and [DatSan].Ngay = '{1}' and ((TimeStart<='{2}' and TimeEnd>'{2}') or (TimeStart<'{3}' and TimeEnd>='{3}') or  (TimeStart>'{2}' and TimeEnd<'{3}') and Ngay ='{4}')  ", ClassMain.nvStatic.idSanbong, strDate, start, end, strDate);

            conn.Open();
                //MessageBox.Show(start);
                SqlCommand com = new SqlCommand(query, conn);
                com.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();
                dataGridView1.DataSource = dt;
              */
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

            
            DateTime iDate = dateTimePicker2.Value;
            string strDate = iDate.ToString("yyyyMMdd");
            string strDate2 = iDate.ToString("dd/MM/yyyy");
            string start = comboBox1.Text + ":" + comboBox3.Text;
            string end = comboBox2.Text + ":" + comboBox4.Text;

            if (strDate2 == ClassMain.datSanstc.ngay & start == ClassMain.datSanstc.timeStart & end == ClassMain.datSanstc.timeEnd & ClassMain.idSannho == ClassMain.datSanstc.idSanNho)
            {
                conn.Open();
                String query2 = String.Format("UPDATE DatSan Set idSannho = '{0}',Ngay='{1}',TimeStart='{2}',TimeEnd='{3}',Gia='{4}',IdDoiBong1='{5}',IdDoiBong2='{6}', SDT_Datsan='{7}' where idDatsan ='{8}'", comboBox9.Text, strDate, start, end, textBox9.Text, textBox4.Text, textBox5.Text, textBox6.Text, ClassMain.datSanstc.idDatSan);
                SqlCommand com2 = new SqlCommand(query2, conn);
                com2.CommandType = CommandType.Text;
                com2.ExecuteNonQuery();
                MessageBox.Show("Update Thành Công");
                button2.Enabled = false;
                conn.Close();
            }
            else
            {
               
                //string query = String.Format("select idSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from DatSan where Ngay = '{3}'  idSan = 1 and (TimeStart<'{0}' and TimeEnd>'{0}') or (TimeStart<'{1}' and TimeEnd>'{1}')  ", "21:00", "22:00", strDate);
                //string query = String.Format("select [DatSan].idSan, TenSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from [DatSan] inner join [NhanVienSanBong] on DatSan.IdSan = NhanVienSanBong.IdSan  where SoSan= '{4}' and [DatSan].IdSan = '{0}' and [DatSan].Ngay = '{1}' and ((TimeStart<='{2}' and TimeEnd>'{2}') or (TimeStart<'{3}' and TimeEnd>='{3}') or  (TimeStart>'{2}' and TimeEnd<'{3}')) Order by Ngay, TimeStart  ", ClassMain.nvStatic.idSanbong, strDate, start, end, comboBox9.Text);
                //string query = String.Format("select [DatSan].idSan, TenSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, TenDoi1, IdDoiBong1, TenDoi2, IdDoiBong2  from [DatSan] inner join [NhanVienSanBong] on DatSan.IdSan = NhanVienSanBong.IdSan  where Ngay='{0}'  ", strDate);
                //string query0 = String.Format("select idDatsan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, Name  , phone, IdDoiBong2  from (select [DatSan].idDatsan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, IdDoiBong1,  IdDoiBong2  from [DatSan] inner join [SanBanhNho] on DatSan.idSannho = SanBanhNho.idSannho  where [SanBanhNho].idSan = '{0}' ) as tb_show inner join DoiBong on [tb_show].IdDoiBong1 = [DoiBong].idDoibong", ClassMain.nvStatic.idSanbong);
                //string query = String.Format("select idDatsan, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, tb.Name  as [Tên đội 1], tb.phone as[Liên hệ] , IdDoiBong2, [DoiBong].Name as  [Tên đội 2], [DoiBong].phone as[Liên hệ] from ({0}) as tb inner join [DoiBong]  on tb.IdDoiBong2 = [DoiBong].idDoibong where idSannho ='{1}'and [DatSan].Ngay = '{1}' and ((TimeStart<='{3}' and TimeEnd>'{3}') or (TimeStart<'{4}' and TimeEnd>='{4}') or  (TimeStart>'{3}' and TimeEnd<'{4}')) Order by Ngay, TimeStart ", query0, ClassMain.idSannho, strDate, start, end);
                string query0 = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, Name  , phone, IdDoiBong2  from (select [DatSan].idDatsan ,[DatSan].idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, IdDoiBong1,  IdDoiBong2  from [DatSan] inner join [SanBanhNho] on DatSan.idSannho = SanBanhNho.idSannho  where [SanBanhNho].idSan = '{0}' ) as tb_show inner join DoiBong on [tb_show].IdDoiBong1 = [DoiBong].idDoibong", ClassMain.nvStatic.idSanbong);
                string query = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, tb.Name  as [Tên đội 1], tb.phone as[Liên hệ] , IdDoiBong2, [DoiBong].Name as  [Tên đội 2], [DoiBong].phone as[Liên hệ] from ({0}) as tb inner join [DoiBong]  on tb.IdDoiBong2 = [DoiBong].idDoibong where idSannho ='{1}' and Ngay = '{2}' and ((TimeStart<='{3}' and TimeEnd>'{3}') or (TimeStart<'{4}' and TimeEnd>='{4}') or  (TimeStart>'{3}' and TimeEnd<'{4}')) Order by Ngay, TimeStart ", query0, ClassMain.idSannho, strDate, start, end);

                conn.Open();

                SqlCommand com = new SqlCommand(query, conn);
                com.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                
                if(dataGridView1.Rows.Count == 0)
                {
                    string[] x = ClassMain.datSanstc.ngay.Split('/');
                    string d = x[2] + x[1] + x[0];
                    String query2 = String.Format("UPDATE DatSan Set idSannho = '{0}',Ngay='{1}',TimeStart='{2}',TimeEnd='{3}',Gia='{4}',IdDoiBong1='{5}',IdDoiBong2='{6}', SDT_Datsan='{7}' where idDatsan ='{8}'", comboBox9.Text, strDate, start, end, textBox9.Text, textBox4.Text, textBox5.Text, textBox6.Text, ClassMain.datSanstc.idDatSan);
                    SqlCommand com2 = new SqlCommand(query2, conn);
                    com2.CommandType = CommandType.Text;
                    com2.ExecuteNonQuery();
                    MessageBox.Show("Update Thành Công 2222");
                    button2.Enabled = false;
                }
                    else if(dataGridView1.Rows.Count == 1 )
                {
                    /*string a = dataGridView1.Rows[0].Cells[3].Value.ToString().Substring(0,10);
                    string b = dataGridView1.Rows[0].Cells[4].Value.ToString().Substring(0, 5);
                    string c = dataGridView1.Rows[0].Cells[5].Value.ToString().Substring(0, 5);
                    if(a == ClassMain.datSanstc.ngay & b== ClassMain.datSanstc.timeStart & c== ClassMain.datSanstc.timeEnd)
                    {
                        String query2 = String.Format("UPDATE DatSan Set SoSan = '{0}',Ngay='{1}',TimeStart='{2}',TimeEnd='{3}',Gia='{4}',TenDoi1='{5}',IdDoiBong1='{6}',TenDoi2='{7}',IdDoiBong2='{8}',SDT_Datsan='{9}'  where Ngay ='{1}' and TimeStart = '{2}'", comboBox9.Text, strDate, start, end, textBox9.Text, textBox1.Text, textBox4.Text, textBox2.Text, textBox5.Text, textBox6.Text);
                        SqlCommand com2 = new SqlCommand(query2, conn);
                        com2.CommandType = CommandType.Text;
                        com2.ExecuteNonQuery();
                        MessageBox.Show("Update Thành Công");
                        button2.Enabled = false;
                    }*/
                    if(dataGridView1.Rows[0].Cells[0].Value.ToString() == ClassMain.datSanstc.idDatSan)
                    {
                        string[] x = ClassMain.datSanstc.ngay.Split('/');
                        string d = x[2] + x[1] + x[0];
                        String query2 = String.Format("UPDATE DatSan Set idSannho = '{0}',Ngay='{1}',TimeStart='{2}',TimeEnd='{3}',Gia='{4}',IdDoiBong1='{5}',IdDoiBong2='{6}', SDT_Datsan='{7}' where idDatsan ='{8}'", comboBox9.Text, strDate, start, end, textBox9.Text, textBox4.Text, textBox5.Text, textBox6.Text, ClassMain.datSanstc.idDatSan);
                        SqlCommand com2 = new SqlCommand(query2, conn);
                        com2.CommandType = CommandType.Text;
                        com2.ExecuteNonQuery();
                        MessageBox.Show("Update Thành Công 333");
                        button2.Enabled = false;
                    }   
                    else
                    {
                        MessageBox.Show("Bị Trùng Giờ, Mã lỗi 1");
                    }    
                    
                }
                else if (dataGridView1.Rows.Count > 1)
                {
                    MessageBox.Show("Bị Trùng Giờ,  Mã lỗi 2");
                }    
                    conn.Close();

            }
            }
            catch
            {
                MessageBox.Show("Lỗi");
            } 
            


        }

        private void comboBox9_SelectedValueChanged(object sender, EventArgs e)
        {
            /*conn.Open();
            String query = String.Format("Select GiaSan, UuDai from SanBanhNho  where idSan = '{0}'", ClassMain.nvStatic.id);
            SqlCommand com = new SqlCommand();
            com.Connection = conn;
            com.CommandText = query;
            using (DbDataReader reader = com.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int giasan = unchecked((int)Convert.ToInt64(reader.GetValue(0)));
                        double Uudai = unchecked((int)Convert.ToInt64(reader.GetValue(1)));
                        double x = giasan - Uudai * giasan / 100.0;
                        textBox9.Text = x.ToString();
                    }
                    reader.Dispose();
                }
            }
            conn.Close();*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            conn.Open();
            String query = String.Format("Delete from DatSan where idDatsan ='{0}'", ClassMain.datSanstc.idDatSan);
            SqlCommand com = new SqlCommand(query, conn);
            com.CommandType = CommandType.Text;
            com.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Đã Xóa");
            show();

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            show();
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 s = new Form2();
            s.Show();
        }

        private void sânBóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 r = new Form3();
            r.Show();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idSannho = 0;
            String query = String.Format("select idSannho from Sanbanhnho where idSan ='{1}' and SoSan ='{0}'", comboBox9.Text, ClassMain.nvStatic.idSanbong);
            conn.Open();
            SqlCommand com = new SqlCommand(query , conn);
            using (DbDataReader reader = com.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       ClassMain.idSannho = reader.GetValue(0).ToString();
                        try
                        {
                            idSannho = unchecked((int)Convert.ToInt64(reader.GetValue(0)));
                        }
                        catch
                        {

                        }
                    }
                    reader.Dispose();
                }
            }

            String query2 = String.Format("Select GiaSan, UuDai from SanBanhNho  where idSannho = '{0}'", idSannho.ToString());
            
            SqlCommand com2 = new SqlCommand(query2,conn);

            using (DbDataReader reader = com2.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int giasan = unchecked((int)Convert.ToInt64(reader.GetValue(0)));
                        
                        int Uudai = unchecked((int)Convert.ToInt64(reader.GetValue(1)));

                        DateTime c = dateTimePicker2.Value;
                        DateTime d = dateTimePicker2.Value;
                        try
                        {
                            int h = Int32.Parse(comboBox1.Text);
                            int m = Int32.Parse(comboBox3.Text);
                            DateTime a = new DateTime(c.Year, c.Month, c.Day, h, m, 0);
                            int h2 = Int32.Parse(comboBox2.Text);
                            int m2 = Int32.Parse(comboBox4.Text);
                            DateTime b = new DateTime(d.Year, d.Month, d.Day, h2, m2, 0);
                            TimeSpan interval = b.Subtract(a);

                            int sophut = interval.Hours * 60 + interval.Minutes;
                            double t1 = (giasan / 60.0) * sophut;
                            double x = t1 - Uudai * t1 / 100.0;
                            textBox9.Text = x.ToString();
                        }
                        catch
                        {

                        }
                        

                        
                    }
                    reader.Dispose();
                }
            }
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime iDate = dateTimePicker2.Value;
            string strDate = iDate.ToString("yyyyMMdd");
            string strDate2 = iDate.ToString("dd/MM/yyyy");
            string start = comboBox1.Text + ":" + comboBox3.Text;
            string end = comboBox2.Text + ":" + comboBox4.Text;
            string query0 = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, Name  , phone, IdDoiBong2  from (select [DatSan].idDatsan ,[DatSan].idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan, IdDoiBong1,  IdDoiBong2  from [DatSan] inner join [SanBanhNho] on DatSan.idSannho = SanBanhNho.idSannho  where [SanBanhNho].idSan = '{0}' ) as tb_show inner join DoiBong on [tb_show].IdDoiBong1 = [DoiBong].idDoibong", ClassMain.nvStatic.idSanbong);
            string query = String.Format("select idDatsan,idSannho, SoSan, Ngay, TimeStart, TimeEnd, Gia, SDT_Datsan,idDoibong1, tb.Name  as [Tên đội 1], tb.phone as[Liên hệ] , IdDoiBong2, [DoiBong].Name as  [Tên đội 2], [DoiBong].phone as[Liên hệ] from ({0}) as tb inner join [DoiBong]  on tb.IdDoiBong2 = [DoiBong].idDoibong where Ngay = '{2}'", query0, ClassMain.idSannho, strDate);
            conn.Open();

            SqlCommand com = new SqlCommand(query, conn);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Report f = new Form_Report();
            f.Show();
        }

        private void độiBóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
        }
    }
}