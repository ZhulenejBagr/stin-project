﻿using STINProject_API.Services.PersistenceService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STINProject_API.Services.PersistenceService.Model
{
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }
        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }

        // TODO create list of currencies
        // TODO create currency object
        [Required]
        public string Currency { get; set; }
        // TODO exclude incorrect values
        [Required]
        public double Balance { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

        public User Owner { get; set; }

        public Account(Guid ownerId, string currency)
        {
            AccountId = new Guid();
            OwnerId = ownerId;
            Currency = currency;
            Balance = 0;
        }

        public Account() { }
    }
}
