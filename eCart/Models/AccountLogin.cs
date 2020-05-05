using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCart.Models
{
    public class AccountLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AccountRegistration
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public int UserStatusId { get; set; }
        public int MasterCityId { get; set; }
        public int MasterAreaId { get; set; }
        public string Password  { get; set; }

    }
}