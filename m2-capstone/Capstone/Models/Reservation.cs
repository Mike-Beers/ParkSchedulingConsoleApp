﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    class Reservation
    {
        public int Reservation_id { get; set; }
        public int Site_id { get; set; }
        public string Reservation_name { get; set; }
        public DateTime Reservation_from_date { get; set; }
        public DateTime Reservation_to_date { get; set; }
        public DateTime Reservation_create_date { get; set; }
        public int Number_of_nights { get; set; }

        public override string ToString()
        {
            return $"";
        }
        private string GetCost(int numberOfNights)
        {
            string cost = "";

            return cost;
        }
    }
}
