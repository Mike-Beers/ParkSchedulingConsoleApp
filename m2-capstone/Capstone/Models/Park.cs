using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;
using Capstone.DAL;

namespace Capstone.Models
{
    public class Park
    {
        public int Park_id { get; set; }
        public string Park_name{ get; set; }
        public string Park_location { get; set; }
        public DateTime Established_dateTime { get; set; }
        public int Area { get; set; }
        public int Annual_visit_count { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Park_name.ToString()} National Park\nLocation: {Park_location.ToString().PadLeft(15)}\nEstablished: {Established_dateTime.ToShortDateString().PadLeft(15)}\nArea: {Area.ToString().PadLeft(15)}\nAnnual Visitors: {Annual_visit_count.ToString().PadLeft(15)}\n";
        }
    }
}
