using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TigerSpikeLandMarks.Entities.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
    }
}
