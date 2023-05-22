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
        private string opis;
        private string cena;
        private DateTime datum;

        private List<Smestaj> smestaji;

        private List<Restoran> restorani;

        private List<Atrakcija> atrakcije;



        public Putovanje(string id,string ime, string opis, string cena, DateTime datum, List<Smestaj> smestaji, List<Restoran> restorani, List<Atrakcija> atrakcije)
        {
            this.id = id;
            this.ime = ime;
            this.opis = opis;
            this.cena = cena;
            this.datum = datum;
            this.smestaji = smestaji;
            this.restorani = restorani;
            this.atrakcije = atrakcije;
        }

        public string Id { get => id; set => id = value; }

        public string Ime { get => ime; set => ime = value; }

        public string Opis { get => opis; set => opis = value; }

        public string Cena { get => cena; set => cena = value; }

        public DateTime Datum { get => datum; set => datum = value; }

        public List<Smestaj> Smestaji { get => smestaji; set => smestaji = value; }

        public List<Restoran> Restorani { get => restorani; set => restorani = value; }

        public List<Atrakcija> Atrakcije { get => atrakcije; set => atrakcije = value; }



    }
}
