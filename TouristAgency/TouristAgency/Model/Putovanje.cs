using System;
using System.Collections.Generic;
using System.Text;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TouristAgency.Model
{
    class Putovanje
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id { get; set; }

        private string ime;
        private string brojDana;
        private string cena;
        private DateTime datum;

        private List<Smestaj> smestaji;

        private List<Restoran> restorani;

        private List<Atrakcija> atrakcije;



        public Putovanje(string id,string ime, string brojDana, string cena, DateTime datum)
        {
            this.id = id;
            this.ime = ime;
            this.brojDana = brojDana;
            this.cena = cena;
            this.datum = datum;
            this.smestaji = new List<Smestaj>();
            this.restorani = new List<Restoran>();
            this.atrakcije = new List<Atrakcija>();
        }

        public string Id { get => id; set => id = value; }

        public string Ime { get => ime; set => ime = value; }

        public string BrojDana { get => brojDana; set => brojDana = value; }

        public string Cena { get => cena; set => cena = value; }

        public DateTime Datum { get => datum; set => datum = value; }

        public List<Smestaj> Smestaji { get => smestaji; set => smestaji = value; }

        public List<Restoran> Restorani { get => restorani; set => restorani = value; }

        public List<Atrakcija> Atrakcije { get => atrakcije; set => atrakcije = value; }



    }
}
