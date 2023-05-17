using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace STINProject.Server.Services.PersistenceService.Models
{
    [ExcludeFromCodeCoverage]
    public class Transaction
    {
        [Key]
        public Guid TransactionID { get; set; }
        [ForeignKey("Account")]
        public Guid AccountID { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public Account Account { get; set; }

        public Transaction(Guid accountID, double value, DateTime date)
        {
            TransactionID = new Guid();
            AccountID = accountID;
            Value = value;
            Date = date;
        }

        public Transaction() { }
    }
}
