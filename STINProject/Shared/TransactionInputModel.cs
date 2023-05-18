using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STINProject.Shared
{
    public class TransactionInputModel
    {
        public Guid UserId { get; set; }
        public string CurrencyCode { get; set; }
        public string Value { get; set; }
        public TransactionInputModel() { }
    }
}
