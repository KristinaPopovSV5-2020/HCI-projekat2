using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using TouristAgency.Model;

namespace TouristAgency.Repozitorijum
{
    class Baza
    {
        string connectionString = "mongodb://localhost:27017";
        private MongoClient mongoKlijent;

        private IMongoCollection<Korisnik> korisniciKol;
        private IMongoCollection<Atrakcija> atrakcijeKol;
        private IMongoCollection<Kupljeni> kupljeniKol;
        private IMongoCollection<Putovanje> putovanjaKol;
        private IMongoCollection<Restoran> restoraniKol;
        private IMongoCollection<Smestaj> smestajiKol;


        public Baza()
        {
            this.mongoKlijent = new MongoClient(connectionString);
            var database = mongoKlijent.GetDatabase("Baza");
            this.korisniciKol = database.GetCollection<Korisnik>("Korisnici");
            this.atrakcijeKol = database.GetCollection<Atrakcija>("Atrakcije");
            this.kupljeniKol = database.GetCollection<Kupljeni>("Kupljeni");
            this.putovanjaKol = database.GetCollection<Putovanje>("Putovanja");
            this.restoraniKol = database.GetCollection<Restoran>("Restorani");
            this.smestajiKol = database.GetCollection<Smestaj>("Smestaji");
           
        }

        public IMongoCollection<Korisnik> KorisniciiKol { get => korisniciKol; set => korisniciKol = value; }

        public IMongoCollection<Kupljeni> KupljeniKol { get => kupljeniKol; set => kupljeniKol = value; }

        public IMongoCollection<Putovanje> PutovanjaKol { get => putovanjaKol; set => putovanjaKol = value; }

        public IMongoCollection<Smestaj> SmestajiKol { get => smestajiKol; set => smestajiKol = value; }

        public IMongoCollection<Restoran> RestoraniKol { get => restoraniKol; set => restoraniKol = value; }

        public IMongoCollection<Atrakcija> AtrakcijeKOl { get => atrakcijeKol; set => atrakcijeKol = value; }


    }
}
