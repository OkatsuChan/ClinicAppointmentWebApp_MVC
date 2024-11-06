using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string PatientName { get; set; }

        [Required]
        public decimal TreatmentCost { get; set; }

        [Required]
        public decimal AmountPaid { get;set; }
        public decimal AmountBalance => TreatmentCost - AmountPaid;
        public bool IsOldPatient { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
