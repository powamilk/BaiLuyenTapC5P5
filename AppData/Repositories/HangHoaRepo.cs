using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public class HangHoaRepo : IHangHoaRepo
    {
        private readonly AppDbContext _context;

        public HangHoaRepo(AppDbContext context)
        {
            _context = context; 
        }

        public async Task AddAsync(HangHoa hangHoa)
        {
            await _context.HangHoas.AddAsync(hangHoa);
            await _context.SaveChangesAsync();  
        }

        public async Task DeleteAsync(Guid id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa != null)
            {
                _context.HangHoas.Remove(hangHoa);
                await _context.SaveChangesAsync();  
            }
        }

        public async Task<IEnumerable<HangHoa>> GetAllAsync()
        {
            return await _context.HangHoas.ToListAsync();   
        }

        public async Task<HangHoa> GetByIdAsync(Guid id)
        {
            return await _context.HangHoas.FindAsync(id);
        }

        public Task UpdateAsync(HangHoa hangHoa)
        {
            _context.HangHoas.Update(hangHoa);
            return _context.SaveChangesAsync();
        }
    }
}
