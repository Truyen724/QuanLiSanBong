using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSanBong.Object
{
    class NhanvienSB
    {
        public int id;
        public string tennv;
        public int idSanbong;
        public string tenSan;
        public String phone;
        public string matkhau;
        private NhanvienSB() { }
        public NhanvienSB(int Id, string Tennv, int IdSanbong, String TenSan, String Phone, String Matkhau)
        {
            this.id = Id;
            this.tennv = Tennv;
            this.phone = Phone;
            this.idSanbong = IdSanbong;
            this.matkhau = Matkhau;
            this.tenSan = TenSan;
        }
    }
}
