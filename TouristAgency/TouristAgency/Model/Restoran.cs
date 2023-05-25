using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TouristAgency.Model
{
    class Restoran
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id { get; set; }

        private string naziv;
        private string adresa;
        private string ocena;


        public Restoran(string id,string naziv, string adresa, string ocena)
        {
            this.id = id;
            this.naziv = naziv;
            this.adresa = adresa;
            this.ocena = ocena;
        }

        public string Id { get => id; set => id = value; }


        public string Naziv { get => naziv; set => naziv = value; }

        public string Adresa { get => adresa; set => adresa = value; }

        public string Ocena { get => ocena; set => ocena = value; }
    }
}
