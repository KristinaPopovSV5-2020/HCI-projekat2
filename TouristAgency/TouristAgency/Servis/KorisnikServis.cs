using MongoDB.Bson;
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

        

        public bool Prijava(string korisnickoIme, string lozinka)
        {


            var filter = Builders<BsonDocument>.Filter.Eq("korisnickoIme", korisnickoIme) & Builders<BsonDocument>.Filter.Eq("lozinka", lozinka);

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

        public bool ProveraKorisnickogImena(string korisnickoIme)
        {


            var filter = Builders<BsonDocument>.Filter.Eq("korisnickoIme", korisnickoIme);

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

        public void Registracija(string korisnickoIme, string lozinka)
        {
            var korisnik = new BsonDocument
            {
                { "korisnickoIme", korisnickoIme },
                { "lozinka", lozinka }
            };

            Baza.KorisniciiKol.InsertOne(korisnik);

        }


    }
}
