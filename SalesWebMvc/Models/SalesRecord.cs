using SalesWebMvc.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Date{ get; set; }

        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Amount { get; set; }
        public SalesStatus Status{ get; set; }
        public Saller Saller { get; set; }

        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Saller saller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Saller = saller;
        }
    }
}
