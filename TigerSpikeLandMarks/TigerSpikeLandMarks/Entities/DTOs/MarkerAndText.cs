using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TigerSpikeLandMarks.Entities.DTOs
{
    public class MarkerAndText
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string text { get; set; }
        public string userName { get; set; }
        public int userId { get; set; }
        public int id { get; set; }
    }
}
