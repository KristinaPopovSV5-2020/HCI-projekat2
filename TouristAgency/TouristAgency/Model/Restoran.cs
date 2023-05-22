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
        private string lokacija;


        public Restoran(string id,string naziv, string lokacija)
        {
            this.id = id;
            this.naziv = naziv;
            this.lokacija = lokacija;
        }

        public string Id { get => id; set => id = value; }


        public string Naziv { get => naziv; set => naziv = value; }

        public string Lokacija { get => lokacija; set => lokacija = value; }
    }
}
