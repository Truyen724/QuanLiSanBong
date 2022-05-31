using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiSanBong.Object;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
namespace QuanLiSanBong
{
    class ClassMain
    {
        public static int Quyen;
        public static string idSannho;
        public static NhanvienSB nvStatic;
        public static DatSan datSanstc;
        public static string ConnetionString = laychuoi();
        public static string laychuoi()
        {
            string path = string.Format(@"{0}\ChuoiKetnoi.txt", Application.StartupPath); ;
            string chuoi = "";
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string line = string.Empty;
                        List<string> list = new List<string>();

                        //listUser.Clear();
                        while ((line = sr.ReadLine()) != null)
                        {
                            list.Add(line);
                            //Kiểm tra xem line có giá trị hay không
                        }
                        string[] userArray = list.ToArray();
                        chuoi = userArray[0];
                    }
                }
            }
            
            return chuoi;
        }

        
    }
}
