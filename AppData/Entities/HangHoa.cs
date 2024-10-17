using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class HangHoa
    {
        public Guid Id { get; set; }
        public string TenHangHoa { get; set; }  
        public decimal TrongLuong { get; set; } 
        public string KichThuoc { get; set; }   
        public string DiaDiemXuatPhat { get; set; } 
        public string DiaDiemGiaoHang { get; set; } 
        public DateTime NgayGiaoDuKien { get; set; }
        public decimal ChiPhiVanChuyen { get; set; }
    }
}
