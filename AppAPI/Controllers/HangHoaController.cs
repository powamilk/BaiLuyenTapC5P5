using AppData;
using AppData.Entities;
using AppData.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly IHangHoaRepo _hangHoaRepo;
        private readonly IValidator<HangHoa> _validator;
        private readonly AppDbContext _context;

        public HangHoaController(IHangHoaRepo hangHoarepo, IValidator<HangHoa> validator, AppDbContext context)
        {
            _hangHoaRepo = hangHoarepo;
            _validator = validator;
            _context = context;
        }

        [HttpPost("tongchiphi")]
        public async Task<IActionResult> TinhTongChiPhiVanChuyen([FromBody] List<Guid> hangHoaIds)
        {
            if (hangHoaIds == null || !hangHoaIds.Any())
            {
                return BadRequest("Danh sách ID hàng hóa không được để trống.");
            }
            var hangHoas = await _context.HangHoas
                                         .Where(h => hangHoaIds.Contains(h.Id))
                                         .ToListAsync();

            if (hangHoas == null || !hangHoas.Any())
            {
                return NotFound("Không tìm thấy hàng hóa nào với danh sách ID đã cung cấp.");
            }

            decimal tongChiPhiVanChuyen = hangHoas.Sum(h => h.ChiPhiVanChuyen);

            return Ok(new { TongChiPhiVanChuyen = tongChiPhiVanChuyen });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hangHoa = await _hangHoaRepo.GetAllAsync();
            return Ok(hangHoa);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var hangHoa = await _hangHoaRepo.GetByIdAsync(id);  
            if(hangHoa == null) return NotFound();  
            return Ok(hangHoa); 
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(HangHoa hangHoa)
        {
            var validatorResult = await _validator.ValidateAsync(hangHoa);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }
                ));
            } 
                
            await _hangHoaRepo.AddAsync(hangHoa);
            return Ok(hangHoa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, HangHoa hangHoa)
        { 
            if( id != hangHoa.Id)
            {
                return BadRequest("Id Khong Khop");
            }
            var validatorResult = await _validator.ValidateAsync(hangHoa);
            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage,
                }
                ));
            }
            await _hangHoaRepo.UpdateAsync(hangHoa);
            return Ok(hangHoa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id )
        {
            await _hangHoaRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
