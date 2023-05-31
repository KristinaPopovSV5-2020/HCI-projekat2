using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using TouristAgency.Model;

namespace TouristAgency.Repozitorijum
{
    class Baza
    {
        string connectionString = "mongodb://localhost:27017";
        private MongoClient mongoKlijent;

        private IMongoCollection<BsonDocument> korisniciKol;
        private IMongoCollection<BsonDocument> atrakcijeKol;
        private IMongoCollection<BsonDocument> kupljeniKol;
        private IMongoCollection<BsonDocument> putovanjaKol;
        private IMongoCollection<BsonDocument> restoraniKol;
        private IMongoCollection<BsonDocument> smestajiKol;


        public Baza()
        {
            this.mongoKlijent = new MongoClient(connectionString);
            var database = mongoKlijent.GetDatabase("Baza");
            this.korisniciKol = database.GetCollection<BsonDocument>("Korisnici");
            this.atrakcijeKol = database.GetCollection<BsonDocument>("Atrakcije");
            this.kupljeniKol = database.GetCollection<BsonDocument>("Kupljeni");
            this.putovanjaKol = database.GetCollection<BsonDocument>("Putovanja");
            this.restoraniKol = database.GetCollection<BsonDocument>("Restorani");
            this.smestajiKol = database.GetCollection<BsonDocument>("Smestaji");
           
        }

        public IMongoCollection<BsonDocument> KorisniciiKol { get => korisniciKol; set => korisniciKol = value; }

        public IMongoCollection<BsonDocument> KupljeniKol { get => kupljeniKol; set => kupljeniKol = value; }

        public IMongoCollection<BsonDocument> PutovanjaKol { get => putovanjaKol; set => putovanjaKol = value; }

        public IMongoCollection<BsonDocument> SmestajiKol { get => smestajiKol; set => smestajiKol = value; }

        public IMongoCollection<BsonDocument> RestoraniKol { get => restoraniKol; set => restoraniKol = value; }

        public IMongoCollection<BsonDocument> AtrakcijeKol { get => atrakcijeKol; set => atrakcijeKol = value; }


    }
}
