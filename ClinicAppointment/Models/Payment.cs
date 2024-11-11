using System.ComponentModel.DataAnnotations;

namespace ClinicAppointment.Models
{
    public enum PaymentType
    {
        Cash,
        EPayment
    }

    public class Payment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string PatientName { get; set; }

        [Required]
        public PaymentType TypeofPayment { get; set; }

        [Required]
        public decimal TreatmentCost { get; set; }

        [Required]
        public decimal AmountPaid { get; set; }

        // If AmountPaid is greater than TreatmentCost, calculate the change.
        public decimal Change => AmountPaid > TreatmentCost ? AmountPaid - TreatmentCost : 0;

        // If TreatmentCost is greater than AmountPaid, calculate the balance.
        public decimal AmountBalance => TreatmentCost > AmountPaid ? TreatmentCost - AmountPaid : 0;

        public bool IsOldPatient { get; set; }

        public DateTime CreatedDate { get; set; }
    }
    
}
