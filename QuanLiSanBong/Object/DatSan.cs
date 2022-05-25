using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSanBong.Object
{
    class DatSan
    {
        //idSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, TenDoi1, IdDoiBong1, TenDoi2,IdDoiBong2, SDT_Datsan\
        public String idDatSan;
        public String idSanNho;
        public String ngay;
        public String timeStart;
        public String timeEnd;
        public String gia;
        public String tenDoi1;
        public String tenDoi2;
        public String sDT_Datsan;
        public DatSan() { }
        public DatSan(String idDatSan, String idSanNho, String Ngay, String TimeStart, String TimeEnd, String Gia, String TenDoi1, String IdDoiBong1, String TenDoi2, String IdDoiBong2, String SDT_Datsan)
        {
            this.idDatSan = idDatSan;
            this.idSanNho = idSanNho;
            this.ngay = Ngay;
            this.timeStart = TimeStart;
            this.timeEnd = TimeEnd;
            this.gia = Gia;
            this.tenDoi1 = TenDoi1;
            
            this.tenDoi2 = TenDoi2;
            
            this.sDT_Datsan = SDT_Datsan;
        }

    }
}
