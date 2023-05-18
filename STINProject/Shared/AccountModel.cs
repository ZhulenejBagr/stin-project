using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STINProject.Shared
{
    public class AccountModel
    {
        public Guid AccountId { get; set; }
        public string Currency { get; set; }
        public double Balance { get; set; }

        public AccountModel() { }
        
    }
}
