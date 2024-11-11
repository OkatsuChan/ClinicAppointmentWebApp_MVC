using ClinicAppointment.Data;
using ClinicAppointment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointment.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAllPayment());
            //return View(await _context.Payments.ToListAsync());
        }


        [HttpGet]
        public IActionResult CreatePayment() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePayment(Payment payment) 
        {
            if (ModelState.IsValid)
            {
                payment.CreatedDate = DateOnly.FromDateTime(DateTime.Now);

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }
        [HttpGet]
        public async Task<IActionResult> DeletePayment(int id) 
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (payment == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePaymentConfirmed(int id)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
