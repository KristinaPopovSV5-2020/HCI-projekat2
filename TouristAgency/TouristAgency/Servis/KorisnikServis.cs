using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using TouristAgency.Model;
using TouristAgency.Repozitorijum;

namespace TouristAgency.Servis
{
    class KorisnikServis
    {

        public Baza Baza = new Baza();

        public KorisnikServis() { }

        

        public Boolean prijava(string korisnickoIme, string lozinka)
        {
            var filter = Builders<Korisnik>.Filter.Eq(x => x.KorisnickoIme, korisnickoIme) &
             Builders<Korisnik>.Filter.Eq(x => x.Lozinka, lozinka);

            var rezultat = Baza.KorisniciiKol.Find(filter).FirstOrDefault();

            if (rezultat != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
