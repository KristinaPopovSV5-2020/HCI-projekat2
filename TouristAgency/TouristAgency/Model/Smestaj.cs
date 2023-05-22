using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace TouristAgency.Model
{
    enum TipSmestaja
    {
        APARTMAN,
        HOTEL,
        SOBA
    }
    class Smestaj
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id { get; set; }

        private string ime;
        private string adresa;
        private TipSmestaja tipSmestaja;
        private string opis;


        public Smestaj(string id,string ime, string adresa, TipSmestaja tip, string opis)
        {
            this.id = id;
            this.ime = ime;
            this.adresa = adresa;
            this.tipSmestaja = tip;
            this.opis = opis;
        }

        public string Id { get => id; set => id = value; }

        public string Ime { get => ime; set => ime = value; }
        public string Adresa { get => adresa; set => adresa = value; }

        public TipSmestaja Tip { get => tipSmestaja; set => tipSmestaja = value; }

        public string Opis { get => opis; set => opis = value; }
    }
}
