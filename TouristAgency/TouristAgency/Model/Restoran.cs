using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TouristAgency.Model
{
    public class Restoran
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id;

        private string adresa;

        private string naziv;
        
        private string ocena;


        public Restoran(string id,string adresa, string naziv, string ocena)
        {
            this.id = id;
            this.adresa = adresa;
            this.naziv = naziv;
            
            this.ocena = ocena;
        }

        public string Id { get => id; set => id = value; }


        public string Naziv { get => naziv; set => naziv = value; }

        public string Adresa { get => adresa; set => adresa = value; }

        public string Ocena { get => ocena; set => ocena = value; }
    }
}
