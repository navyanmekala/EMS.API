using Microsoft.AspNetCore.Mvc;
using EMS1.API.Models;
using EMS1.API.Data;

namespace EMS1.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
