using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public interface IHangHoaRepo
    {
        Task<IEnumerable<HangHoa>> GetAllAsync();
        Task<HangHoa> GetByIdAsync(Guid id);
        Task AddAsync (HangHoa hangHoa);    
        Task UpdateAsync (HangHoa hangHoa); 
        Task DeleteAsync (Guid id);
    }
}
