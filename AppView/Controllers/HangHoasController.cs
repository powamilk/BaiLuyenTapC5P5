using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppData;
using AppData.Entities;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class HangHoasController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _context;

        public HangHoasController(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7085/api/");
        }

        // GET: HangHoas
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7085/api/api/HangHoa");
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var hangHoaEntities = JsonConvert.DeserializeObject<List<AppData.Entities.HangHoa>>(content);
                 return View(hangHoaEntities);
                
            }
            return View(new List<HangHoa>());
        }

        // GET: HangHoas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var hangHoa = await _httpClient.GetFromJsonAsync<HangHoa>($"HangHoa/{id}");
            if (hangHoa == null)
            {
                return NotFound();
            }   
            return View(hangHoa);
        }

        // GET: HangHoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HangHoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenHangHoa,TrongLuong,KichThuoc,DiaDiemXuatPhat,DiaDiemGiaoHang,NgayGiaoDuKien,ChiPhiVanChuyen")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                hangHoa.Id = Guid.NewGuid();
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hangHoa);
        }

        // GET: HangHoas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return View(hangHoa);
        }

        // POST: HangHoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TenHangHoa,TrongLuong,KichThuoc,DiaDiemXuatPhat,DiaDiemGiaoHang,NgayGiaoDuKien,ChiPhiVanChuyen")] HangHoa hangHoa)
        {
            if (id != hangHoa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hangHoa);
        }

        // GET: HangHoas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: HangHoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hangHoa = await _context.HangHoas.FindAsync(id);
            if (hangHoa != null)
            {
                _context.HangHoas.Remove(hangHoa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(Guid id)
        {
            return _context.HangHoas.Any(e => e.Id == id);
        }
    }
}
