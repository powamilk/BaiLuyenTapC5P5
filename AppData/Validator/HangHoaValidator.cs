using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using FluentValidation;

namespace AppData.Validator
{
    public class HangHoaValidator : AbstractValidator<HangHoa>
    {
        public HangHoaValidator()
        {
            RuleFor(g => g.TenHangHoa).NotEmpty().WithMessage("Tên Hàng Hóa không được để trống");
            RuleFor(g => g.TrongLuong).GreaterThan(0).WithMessage("trọng lượng phải lớn hơn 0");
            RuleFor(g => g.KichThuoc).NotEmpty().WithMessage("Kích THước không được để trống");
            RuleFor(g => g.DiaDiemGiaoHang).NotEmpty().WithMessage("Địa Điểm Giao Hàng không được để trống");
            RuleFor(g => g.DiaDiemXuatPhat).NotEmpty().WithMessage("ĐỊa điểm xuất phát không được để trống");
            RuleFor(g => g.ChiPhiVanChuyen).GreaterThan(0).WithMessage("Chi Phí vận chuyển phải lớn hơn 0");
            RuleFor(g => g.NgayGiaoDuKien).GreaterThanOrEqualTo(DateTime.Now).WithMessage("Ngày giao hàng phải là trong tương lai");
        }
    }
}
