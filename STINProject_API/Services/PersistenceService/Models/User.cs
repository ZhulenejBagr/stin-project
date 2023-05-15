<<<<<<< HEAD
<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
>>>>>>> fa5c31d53c1373a304a89e6f19ac0359047091e0
=======
﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
>>>>>>> parent of c2a5f9a (refactoring, update tests)

namespace STINProject_API.Services.PersistenceService.Model
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; }

        // TODO hash password
        [Required]
        public string Password { get; set; }

        // TODO verify if email address is valid
        [Required]
        public string Email { get; set; }

        public IEnumerable<Account> Accounts { get; set; }

        public User(string username, string password, string email)
        {
            UserId = new Guid();
            Username = username;
            Password = password;
            Email = email;
        }

        public User() { }
    }
}
