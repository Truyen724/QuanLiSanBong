using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSanBong.Object
{
    class DatSan
    {
        //idSan, SoSan, Ngay, TimeStart, TimeEnd, Gia, TenDoi1, IdDoiBong1, TenDoi2,IdDoiBong2, SDT_Datsan
        public String idSan;
        public String soSan;
        public String ngay;
        public String timeStart;
        public String timeEnd;
        public String gia;
        public String tenDoi1;
        public String idDoiBong1;
        public String tenDoi2;
        public String idDoiBong2;
        public String sDT_Datsan;
        public DatSan() { }
        public DatSan(String idSan, String SoSan, String Ngay, String TimeStart, String TimeEnd, String Gia, String TenDoi1, String IdDoiBong1, String TenDoi2, String IdDoiBong2, String SDT_Datsan)
        {
            this.idSan = idSan;
            this.soSan = SoSan;
            this.ngay = Ngay;
            this.timeStart = TimeStart;
            this.timeEnd = TimeEnd;
            this.gia = Gia;
            this.tenDoi1 = TenDoi1;
            this.idDoiBong1 = IdDoiBong1;
            this.tenDoi2 = TenDoi2;
            this.idDoiBong2 = IdDoiBong2;
            this.sDT_Datsan = SDT_Datsan;
        }

    }
}
