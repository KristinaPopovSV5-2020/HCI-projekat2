using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TouristAgency.Model
{
    class Atrakcija
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id { get; set; }

        private string naziv;
        private string opis;


        public Atrakcija(string id,string naziv, string opis)
        {
            this.id = id;
            this.naziv = naziv;
            this.opis = opis;
        }

        public string Id { get => id; set => id = value; }

        public string Naziv { get => naziv; set => naziv = value; }

        public string Opis { get => opis; set => opis = value; }
    }
}
