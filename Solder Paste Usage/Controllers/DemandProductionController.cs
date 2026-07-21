using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolderPasteUsage.Data;
using SolderPasteUsage.Models;

namespace SolderPasteUsage.Controllers
{
    public class DemandProductionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DemandProductionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // INDEX
        // Menampilkan seluruh data Demand Production
        // =====================================================

        public async Task<IActionResult> Index()
        {
            var demandProductions = await _context.DemandProduction
                .OrderByDescending(x => x.WOStart)
                .ThenByDescending(x => x.DemandId)
                .ToListAsync();

            return View(demandProductions);
        }


        // =====================================================
        // CREATE - GET
        // Menampilkan halaman tambah data
        // =====================================================

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // =====================================================
        // CREATE - POST
        // Menyimpan data baru
        // =====================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            DemandProduction demandProduction)
        {
            if (ModelState.IsValid)
            {
                _context.DemandProduction.Add(
                    demandProduction
                );

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] =
                    "Demand Production berhasil ditambahkan.";

                return RedirectToAction(nameof(Index));
            }

            return View(demandProduction);
        }


        // =====================================================
        // EDIT - GET
        // Menampilkan halaman edit
        // =====================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var demandProduction =
                await _context.DemandProduction
                    .FirstOrDefaultAsync(
                        x => x.DemandId == id
                    );

            if (demandProduction == null)
            {
                return NotFound();
            }

            return View(demandProduction);
        }


        // =====================================================
        // EDIT - POST
        // Menyimpan perubahan data
        // =====================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            DemandProduction demandProduction)
        {
            if (id != demandProduction.DemandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.DemandProduction.Update(
                        demandProduction
                    );

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] =
                        "Demand Production berhasil diperbarui.";

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemandProductionExists(
                        demandProduction.DemandId))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            return View(demandProduction);
        }


        // =====================================================
        // DELETE - GET
        // Menampilkan halaman konfirmasi delete
        // =====================================================

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var demandProduction =
                await _context.DemandProduction
                    .FirstOrDefaultAsync(
                        x => x.DemandId == id
                    );

            if (demandProduction == null)
            {
                return NotFound();
            }

            return View(demandProduction);
        }


        // =====================================================
        // DELETE - POST
        // Menghapus data
        // =====================================================

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
            int id)
        {
            var demandProduction =
                await _context.DemandProduction
                    .FirstOrDefaultAsync(
                        x => x.DemandId == id
                    );

            if (demandProduction != null)
            {
                _context.DemandProduction.Remove(
                    demandProduction
                );

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] =
                    "Demand Production berhasil dihapus.";
            }

            return RedirectToAction(nameof(Index));
        }


        // =====================================================
        // CHECK DATA EXIST
        // =====================================================

        private bool DemandProductionExists(int id)
        {
            return _context.DemandProduction
                .Any(
                    x => x.DemandId == id
                );
        }
    }
}