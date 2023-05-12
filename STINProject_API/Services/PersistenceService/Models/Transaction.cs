﻿using STINProject_API.Services.PersistenceService.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STINProject_API.Services.PersistenceService.Models
{
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
