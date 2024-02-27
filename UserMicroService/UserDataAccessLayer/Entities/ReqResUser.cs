﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDataAccessLayer.Entities
{
    public class ReqResUser
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
    }
}
