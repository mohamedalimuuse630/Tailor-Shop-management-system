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
    public class OrdersController : Controller
    {
        private readonly WebDbContext _context;

        public OrdersController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(o => o.Customer).ToListAsync();
            var orderViewModels = orders.Select(o => new OrderVeiwModel
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                DeliveryDate = o.DeliveryDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                PaymentStatus = o.PaymentStatus
            }).ToList();

            return View(orderViewModels);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = new OrderVeiwModel
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                PaymentStatus = order.PaymentStatus
            };

            return View(orderViewModel);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderDate,DeliveryDate,Status,TotalAmount,PaymentStatus")] OrderVeiwModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    CustomerId = orderViewModel.CustomerId,
                    OrderDate = orderViewModel.OrderDate,
                    DeliveryDate = orderViewModel.DeliveryDate,
                    Status = orderViewModel.Status,
                    TotalAmount = orderViewModel.TotalAmount,
                    PaymentStatus = orderViewModel.PaymentStatus
                };

                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", orderViewModel.CustomerId);
            return View(orderViewModel);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = new OrderVeiwModel
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                PaymentStatus = order.PaymentStatus
            };

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", order.CustomerId);
            return View(orderViewModel);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,OrderDate,DeliveryDate,Status,TotalAmount,PaymentStatus")] OrderVeiwModel orderViewModel)
        {
            if (id != orderViewModel.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var order = await _context.Orders.FindAsync(id);
                    if (order != null)
                    {
                        order.CustomerId = orderViewModel.CustomerId;
                        order.OrderDate = orderViewModel.OrderDate;
                        order.DeliveryDate = orderViewModel.DeliveryDate;
                        order.Status = orderViewModel.Status;
                        order.TotalAmount = orderViewModel.TotalAmount;
                        order.PaymentStatus = orderViewModel.PaymentStatus;

                        _context.Update(order);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(orderViewModel.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", orderViewModel.CustomerId);
            return View(orderViewModel);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = new OrderVeiwModel
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                PaymentStatus = order.PaymentStatus
            };

            return View(orderViewModel);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
