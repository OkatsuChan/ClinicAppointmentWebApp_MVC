using ClinicAppointment.Data;
using ClinicAppointment.Models;
using ClosedXML.Excel;
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


        public ActionResult ExportToExcel()
        {
            var payments = _context.Payments.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Payments");

                // Set headers
                worksheet.Cell(1, 1).Value = "Patient Name";
                worksheet.Cell(1, 2).Value = "Treatment Cost";
                worksheet.Cell(1, 3).Value = "Amount Paid";
                worksheet.Cell(1, 4).Value = "Amount Balance";
                worksheet.Cell(1, 5).Value = "Is Old Patient";
                worksheet.Cell(1, 6).Value = "Created Date";

                // Insert data into rows
                int row = 2;
                foreach (var payment in payments)
                {
                    worksheet.Cell(row, 1).Value = payment.PatientName;
                    worksheet.Cell(row, 2).Value = payment.TreatmentCost;
                    worksheet.Cell(row, 3).Value = payment.AmountPaid;
                    worksheet.Cell(row, 4).Value = payment.AmountBalance;
                    worksheet.Cell(row, 5).Value = payment.IsOldPatient ? "Yes" : "No";
                    worksheet.Cell(row, 6).Value = payment.CreatedDate.ToString("yyyy-MM-dd");
                    row++;
                }

                // Adjust column widths
                worksheet.Columns().AdjustToContents();

                // Save to memory stream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Payments.xlsx"
                    );
                }
            }
        }



            [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Payments.ToListAsync());
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
                payment.CreatedDate = DateTime.Now;

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
