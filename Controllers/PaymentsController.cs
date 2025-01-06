using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TailorShop.Models;
using Tailor_shop.Data;
using Tailor_shop.ViewModel;

namespace Tailor_shop.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly WebDbContext _context;

        public PaymentsController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var payments = await _context.Payments
                .Include(p => p.Order)
                .ToListAsync();

            // Map Payments to PaymentVeiwModel
            var paymentViewModels = payments.Select(p => new PaymentVeiwModel
            {
                PaymentId = p.PaymentId,
                OrderId = p.OrderId,
                AmountPaid = p.AmountPaid,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod
            }).ToList();

            return View(paymentViewModels);
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.PaymentId == id);

            if (payment == null)
            {
                return NotFound();
            }

            // Map Payment to PaymentVeiwModel
            var paymentViewModel = new PaymentVeiwModel
            {
                PaymentId = payment.PaymentId,
                OrderId = payment.OrderId,
                AmountPaid = payment.AmountPaid,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
            };

            return View(paymentViewModel);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            // Provide a list of orders for selection
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "PaymentStatus");
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,OrderId,AmountPaid,PaymentDate,PaymentMethod")] PaymentVeiwModel paymentViewModel)
        {
            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    OrderId = paymentViewModel.OrderId,
                    AmountPaid = paymentViewModel.AmountPaid,
                    PaymentDate = paymentViewModel.PaymentDate,
                    PaymentMethod = paymentViewModel.PaymentMethod
                };

                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "PaymentStatus", paymentViewModel.OrderId);
            return View(paymentViewModel);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            var paymentViewModel = new PaymentVeiwModel
            {
                PaymentId = payment.PaymentId,
                OrderId = payment.OrderId,
                AmountPaid = payment.AmountPaid,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
            };

            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "PaymentStatus", payment.OrderId);
            return View(paymentViewModel);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,OrderId,AmountPaid,PaymentDate,PaymentMethod")] PaymentVeiwModel paymentViewModel)
        {
            if (id != paymentViewModel.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var payment = await _context.Payments.FindAsync(id);
                    if (payment != null)
                    {
                        payment.OrderId = paymentViewModel.OrderId;
                        payment.AmountPaid = paymentViewModel.AmountPaid;
                        payment.PaymentDate = paymentViewModel.PaymentDate;
                        payment.PaymentMethod = paymentViewModel.PaymentMethod;

                        _context.Update(payment);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(paymentViewModel.PaymentId))
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

            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "PaymentStatus", paymentViewModel.OrderId);
            return View(paymentViewModel);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(m => m.PaymentId == id);

            if (payment == null)
            {
                return NotFound();
            }

            var paymentViewModel = new PaymentVeiwModel
            {
                PaymentId = payment.PaymentId,
                OrderId = payment.OrderId,
                AmountPaid = payment.AmountPaid,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod
            };

            return View(paymentViewModel);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
