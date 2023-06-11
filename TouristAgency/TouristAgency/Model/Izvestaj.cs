using System;
using System.Collections.Generic;
using System.Text;

namespace TouristAgency.Model
{
    class Izvestaj
    {
        public int RedniBroj { get; set; }
        public string Naziv { get; set; }
        public List<Kupljeni> KupljeniList { get; set; }
        public int BrojKupljenih { get; set; }

    }
}
