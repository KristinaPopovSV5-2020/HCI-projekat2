﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace TouristAgency.Model
{
    public enum TipSmestaja
    {
        APARTMAN,
        HOTEL,
        SOBA
    }
    public class Smestaj
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string id { get; set; }

        private string naziv;
        private string adresa;
        [BsonRepresentation(BsonType.String)]
        private TipSmestaja tipSmestaja;
        private string ocena;

        public Smestaj()
        {

        }
        public Smestaj(string id,string naziv, string adresa, TipSmestaja tip, string ocena)
        {
            this.id = id;
            this.naziv = naziv;
            this.adresa = adresa;
            this.tipSmestaja = tip;
            this.ocena = ocena;
        }

        public string Id { get => id; set => id = value; }

        public string Naziv { get => naziv; set => naziv = value; }
        public string Adresa { get => adresa; set => adresa = value; }

        public TipSmestaja Tip { get => tipSmestaja; set => tipSmestaja = value; }

        public string Ocena { get => ocena; set => ocena = value; }
    }

    public class SmestajArgs : EventArgs
    {
        public Smestaj PovratnaVrednost { get; set; }
    }

    public class SmestajiArgs : EventArgs
    {
        public ObservableCollection<Smestaj> PovratnaVrednost { get; set; }
    }
}
