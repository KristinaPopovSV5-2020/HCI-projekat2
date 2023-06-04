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

        [BsonElement("naziv")]
        private string naziv;

        [BsonElement("brojDana")]
        private string brojDana;

        [BsonElement("cena")]
        private string cena;

        [BsonElement("datum")]
        private DateTime datum;

        [BsonElement("smestaji")]
        private List<Smestaj> smestaji;

        [BsonElement("restorani")]
        private List<Restoran> restorani;

        [BsonElement("atrakcije")]
        private List<Atrakcija> atrakcije;

        public Putovanje() { }

        public Putovanje(string id,string naziv, string brojDana, string cena, DateTime datum, List<Atrakcija> atrakcije,  List<Smestaj> smestaji, List<Restoran> restorani)
        {
            this.id = id;
            this.naziv = naziv;
            this.brojDana = brojDana;
            this.cena = cena;
            this.datum = datum;
            this.smestaji = smestaji;
            this.restorani = restorani;
            this.atrakcije = atrakcije;
        }

        public Putovanje(string id, string naziv, string brojDana, string cena, DateTime datum)
        {
            this.id = id;
            this.naziv = naziv;
            this.brojDana = brojDana;
            this.cena = cena;
            this.datum = datum;
        }

        public string Id { get => id; set => id = value; }

        public string Naziv { get => naziv; set => naziv = value; }

        public string BrojDana { get => brojDana; set => brojDana = value; }

        public string Cena { get => cena; set => cena = value; }

        public DateTime Datum { get => datum; set => datum = value; }

        public string Polazak { get => datum.ToShortDateString(); }
        public string Povratak { get => datum.AddDays(int.Parse(brojDana)).ToShortDateString(); }


        public List<Smestaj> Smestaji { get => smestaji; set => smestaji = value; }

        public List<Restoran> Restorani { get => restorani; set => restorani = value; }

        public List<Atrakcija> Atrakcije { get => atrakcije; set => atrakcije = value; }
        
    }
}
