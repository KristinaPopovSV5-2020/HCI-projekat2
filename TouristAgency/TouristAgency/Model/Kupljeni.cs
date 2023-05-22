using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TouristAgency.Model
{
    class Kupljeni
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        private string id { get; set; }

        private Korisnik korisnik;

        private Putovanje putovanje;


        public Kupljeni(string id, Korisnik korisnik, Putovanje putovanje)
        {
            this.id = id;
            this.korisnik = korisnik;
            this.putovanje = putovanje;
        }

        public string Id { get => id; set => id = value; }

        public Korisnik Korisnik { get => korisnik; set => korisnik = value; }

        public Putovanje Putovanje { get => putovanje; set => putovanje = value; }
    }
}
