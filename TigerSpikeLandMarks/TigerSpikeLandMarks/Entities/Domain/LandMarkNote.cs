using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TigerSpikeLandMarks.Entities
{
    public class LandMarkNote
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string UserNote { get; set; }
        public bool IsActive { get; set; }
        public virtual User User { get; set; }
        public LandMarkNote()
        {
            IsActive = false;
            CreationDate = DateTime.UtcNow;
        }
    }
}
