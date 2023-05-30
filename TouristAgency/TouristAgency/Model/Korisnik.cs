using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TouristAgency.Model
{
    class Korisnik
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id { get; set; }

        private string korisnickoIme;
        private string lozinka;

        public Korisnik()
        {

        }

        public Korisnik(string id,string korisniko, string lozinka)
        {
            this.id = id;
            this.korisnickoIme = korisniko;
            this.lozinka = lozinka;
        }

        public string Id { get => id; set => id = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
    }
}
