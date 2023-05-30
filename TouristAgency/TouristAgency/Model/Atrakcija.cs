using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TouristAgency.Model
{
    public class Atrakcija
    {

        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id { get; set; }

        [BsonElement("naziv")]
        private string naziv { get; set; }
        [BsonElement("opis")]
        private string opis { get; set; }
        [BsonElement("adresa")]
        private string adresa { get; set; }


        public Atrakcija(string id,string naziv, string opis, string adresa)
        {
            this.id = id;
            this.naziv = naziv;
            this.opis = opis;
            this.adresa = adresa;
        }

        public Atrakcija()
        {

        }

        public string Id { get => id; set => id = value; }

        public string Naziv { get => naziv; set => naziv = value; }

        public string Opis { get => opis; set => opis = value; }

        public string Adresa { get => adresa; set => adresa = value; }
    }

    public class AtrakcijaArgs : EventArgs
    {
        public Atrakcija PovratnaVrednost { get; set; }
    }
}
