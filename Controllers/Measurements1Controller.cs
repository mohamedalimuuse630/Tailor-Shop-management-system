using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tailor_shop.Data;
using Tailor_shop.Models;
using Tailor_shop.ViewModel;

namespace Tailor_shop.Controllers
{
    public class Measurements1Controller : Controller
    {
        private readonly WebDbContext _context;

        public Measurements1Controller(WebDbContext context)
        {
            _context = context;
        }

        // GET: Measurements1
        public async Task<IActionResult> Index()
        {
            var measurements = await _context.Measurements
                .Include(m => m.Customer)
                .Include(m => m.Product)
                .Select(m => new MeasurementViewModel
                {
                    MeasurementId = m.MeasurementId,
                    CustomerId = m.CustomerId,
                    ProductId = m.ProductId,
                    Neck = m.Neck,
                    Chest = m.Chest,
                    Waist = m.Waist,
                    Hips = m.Hips,
                    Inseam = m.Inseam
                })
                .ToListAsync();

            return View(measurements);
        }

        // GET: Measurements1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements
                .Include(m => m.Customer)
                .Include(m => m.Product)
                .Where(m => m.MeasurementId == id)
                .Select(m => new MeasurementViewModel
                {
                    MeasurementId = m.MeasurementId,
                    CustomerId = m.CustomerId,
                    ProductId = m.ProductId,
                    Neck = m.Neck,
                    Chest = m.Chest,
                    Waist = m.Waist,
                    Hips = m.Hips,
                    Inseam = m.Inseam
                })
                .FirstOrDefaultAsync();

            if (measurement == null)
            {
                return NotFound();
            }

            return View(measurement);
        }

        // GET: Measurements1/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: Measurements1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeasurementViewModel measurementViewModel)
        {
            if (ModelState.IsValid)
            {
                var measurement = new Measurement
                {
                    CustomerId = measurementViewModel.CustomerId,
                    ProductId = measurementViewModel.ProductId,
                    Neck = measurementViewModel.Neck,
                    Chest = measurementViewModel.Chest,
                    Waist = measurementViewModel.Waist,
                    Hips = measurementViewModel.Hips,
                    Inseam = measurementViewModel.Inseam
                };

                _context.Add(measurement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", measurementViewModel.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", measurementViewModel.ProductId);
            return View(measurementViewModel);
        }

        // GET: Measurements1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            var measurementViewModel = new MeasurementViewModel
            {
                MeasurementId = measurement.MeasurementId,
                CustomerId = measurement.CustomerId,
                ProductId = measurement.ProductId,
                Neck = measurement.Neck,
                Chest = measurement.Chest,
                Waist = measurement.Waist,
                Hips = measurement.Hips,
                Inseam = measurement.Inseam
            };

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", measurementViewModel.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", measurementViewModel.ProductId);
            return View(measurementViewModel);
        }

        // POST: Measurements1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MeasurementViewModel measurementViewModel)
        {
            if (id != measurementViewModel.MeasurementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var measurement = new Measurement
                    {
                        MeasurementId = measurementViewModel.MeasurementId,
                        CustomerId = measurementViewModel.CustomerId,
                        ProductId = measurementViewModel.ProductId,
                        Neck = measurementViewModel.Neck,
                        Chest = measurementViewModel.Chest,
                        Waist = measurementViewModel.Waist,
                        Hips = measurementViewModel.Hips,
                        Inseam = measurementViewModel.Inseam
                    };

                    _context.Update(measurement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasurementExists(measurementViewModel.MeasurementId))
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

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", measurementViewModel.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", measurementViewModel.ProductId);
            return View(measurementViewModel);
        }

        // GET: Measurements1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measurement = await _context.Measurements
                .Include(m => m.Customer)
                .Include(m => m.Product)
                .Where(m => m.MeasurementId == id)
                .Select(m => new MeasurementViewModel
                {
                    MeasurementId = m.MeasurementId,
                    CustomerId = m.CustomerId,
                    ProductId = m.ProductId,
                    Neck = m.Neck,
                    Chest = m.Chest,
                    Waist = m.Waist,
                    Hips = m.Hips,
                    Inseam = m.Inseam
                })
                .FirstOrDefaultAsync();

            if (measurement == null)
            {
                return NotFound();
            }

            return View(measurement);
        }

        // POST: Measurements1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement != null)
            {
                _context.Measurements.Remove(measurement);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MeasurementExists(int id)
        {
            return _context.Measurements.Any(e => e.MeasurementId == id);
        }
    }
}
