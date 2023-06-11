using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TouristAgency.Model;
using TouristAgency.Repozitorijum;
using MongoDB.Driver.Linq;


namespace TouristAgency.Servis
{
    class PutovanjaServis
    {

        public Baza Baza = new Baza();


        public PutovanjaServis() { }



        public async Task<ObservableCollection<Atrakcija>> SveAtrakcijeAsync()
        {
            ObservableCollection<Atrakcija> atrakcije = new ObservableCollection<Atrakcija>();
            var documents = await Baza.AtrakcijeKol.Find(new BsonDocument()).ToListAsync();
            foreach (var document in documents)
            {
                atrakcije.Add(new Atrakcija(document["_id"].AsString, document["naziv"].AsString, document["opis"].AsString, document["adresa"].AsString));
            }
            return atrakcije;
        }

        public void DodajAtrakciju(Atrakcija atrakcija)
        {
            var document = new BsonDocument
            {
                { "_id", atrakcija.Id },
                { "naziv", atrakcija.Naziv },
                { "opis", atrakcija.Opis },
                { "adresa", atrakcija.Adresa },
            };

            Baza.AtrakcijeKol.InsertOne(document);
        }

        public void IzmeniAtrakciju(Atrakcija atrakcija)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", atrakcija.Id);
            
            var update = Builders<BsonDocument>.Update
                .Set("naziv", atrakcija.Naziv)
                .Set("opis", atrakcija.Opis)
                .Set("adresa", atrakcija.Adresa);

            var updateResult = Baza.AtrakcijeKol.UpdateOne(filter, update);
            IzmeniAtrakcijuUPutovanju(atrakcija);
        }

        public void IzmeniAtrakcijuUPutovanju(Atrakcija a)
        {
            var documents = Baza.PutovanjaKol.Find(_ => true).ToList();

            foreach (var document in documents)
            {
                
                var atrakcije = document["atrakcije"].AsBsonArray;
                var filter = Builders<BsonDocument>.Filter.Eq("atrakcije.Id", a.Id);
                var update = Builders<BsonDocument>.Update.Set("atrakcije.$.naziv", a.Naziv)
                                                          .Set("atrakcije.$.opis", a.Opis)
                                                          .Set("atrakcije.$.adresa", a.Adresa)
                                                          .Set("atrakcije.$.Naziv", a.Naziv)
                                                          .Set("atrakcije.$.Opis", a.Opis)
                                                          .Set("atrakcije.$.Adresa", a.Adresa);

                Baza.PutovanjaKol.UpdateOne(filter, update);
                
            }
        }



        public async Task<ObservableCollection<Restoran>> FiltriranjeRestorana(string min, string max)
        {
            ObservableCollection<Restoran> restorani = new ObservableCollection<Restoran>();

            var f2 = Builders<BsonDocument>.Filter.Gte("ocena", min) & Builders<BsonDocument>.Filter.Lte("ocena", max);

            var documents = await Baza.RestoraniKol.Find(f2).ToListAsync();

            foreach (var document in documents)
            {
                restorani.Add(new Restoran(document["_id"].AsString, document["adresa"].AsString, document["naziv"].AsString, document["ocena"].AsString));
            }
            return restorani;
        }

        public async Task<ObservableCollection<Smestaj>> FiltriranjeSmestaja(List<string> list, string min, string max)
        {
            ObservableCollection<Smestaj> smestaji = new ObservableCollection<Smestaj>();

            var f1 = Builders<BsonDocument>.Filter.In("tipSmestaja", list);
            var f2 = Builders<BsonDocument>.Filter.Gte("ocena", min) & Builders<BsonDocument>.Filter.Lte("ocena", max);
            var filter = Builders<BsonDocument>.Filter.And(f1, f2);

            var documents = await Baza.SmestajiKol.Find(filter).ToListAsync();

            foreach (var document in documents)
            {
                smestaji.Add(new Smestaj(document["_id"].AsString, document["naziv"].AsString, document["adresa"].AsString, (TipSmestaja)Enum.Parse(typeof(TipSmestaja), document["tipSmestaja"].AsString), document["ocena"].AsString));
            }
            return smestaji;

        }

        public void ObrisiAtrakciju(Atrakcija atrakcija)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", atrakcija.Id);

            DeleteResult result = Baza.AtrakcijeKol.DeleteOne(filter);
            ObrisiAtrakcijuUPutovanju(atrakcija);
        }

        public void ObrisiAtrakcijuUPutovanju(Atrakcija a)
        {
            var documents = Baza.PutovanjaKol.Find(_ => true).ToList();

            foreach (var document in documents)
            {

                var filter = Builders<BsonDocument>.Filter.Eq("atrakcije.Id", a.Id);
                var update = Builders<BsonDocument>.Update.PullFilter("atrakcije", Builders<BsonDocument>.Filter.Eq("Id", a.Id));

                Baza.PutovanjaKol.UpdateMany(filter, update);

            }
        }
        public async Task<ObservableCollection<Restoran>> SviRestoraniAsync()
        {
            ObservableCollection<Restoran> restorani = new ObservableCollection<Restoran>();
            var documents = await Baza.RestoraniKol.Find(new BsonDocument()).ToListAsync();
            foreach (var document in documents)
            {
                restorani.Add(new Restoran(document["_id"].AsString, document["adresa"].AsString, document["naziv"].AsString, document["ocena"].AsString));
            }
            return restorani;
        }

        public void DodajRestoran(Restoran restoran)
        {
            var document = new BsonDocument
            {
                { "_id", restoran.Id },
                { "adresa", restoran.Adresa },
                { "naziv", restoran.Naziv },
                 { "ocena", restoran.Ocena }
            };

            Baza.RestoraniKol.InsertOne(document);
        }

        public void IzmeniRestoran(Restoran restoran)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", restoran.Id);
            
            var update = Builders<BsonDocument>.Update
                .Set("naziv", restoran.Naziv)
                .Set("adresa", restoran.Adresa)
                .Set("ocena", restoran.Ocena);

            var updateResult = Baza.RestoraniKol.UpdateOne(filter, update);
            IzmeniRestoranUPutovanju(restoran);
        }

        public void IzmeniRestoranUPutovanju(Restoran r)
        {
            var documents = Baza.PutovanjaKol.Find(_ => true).ToList();

            foreach (var document in documents)
            {

                var restorani = document["restorani"].AsBsonArray;
                var filter = Builders<BsonDocument>.Filter.Eq("restorani.Id", r.Id);
                var update = Builders<BsonDocument>.Update.Set("restorani.$.Naziv", r.Naziv)
                                                          .Set("restorani.$.Ocena", r.Ocena)
                                                          .Set("restorani.$.Adresa", r.Adresa);

                Baza.PutovanjaKol.UpdateOne(filter, update);

            }
        }
        public void ObrisiRestoran(Restoran restoran)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", restoran.Id);

            DeleteResult result = Baza.RestoraniKol.DeleteOne(filter);
            ObrisiRestoranUPutovanju(restoran);
        }

        public void ObrisiRestoranUPutovanju(Restoran r)
        {
            var documents = Baza.PutovanjaKol.Find(_ => true).ToList();

            foreach (var document in documents)
            {

                var filter = Builders<BsonDocument>.Filter.Eq("restorani.Id", r.Id);
                var update = Builders<BsonDocument>.Update.PullFilter("restorani", Builders<BsonDocument>.Filter.Eq("Id", r.Id));

                Baza.PutovanjaKol.UpdateMany(filter, update);

            }
        }

        public async Task<ObservableCollection<Smestaj>> SviSmestajiAsync()
        {
            ObservableCollection<Smestaj> smestaji = new ObservableCollection<Smestaj>();
            var documents = await Baza.SmestajiKol.Find(new BsonDocument()).ToListAsync();
            foreach (var document in documents)
            {
                smestaji.Add(new Smestaj(document["_id"].AsString, document["naziv"].AsString, document["adresa"].AsString, (TipSmestaja)Enum.Parse(typeof(TipSmestaja), document["tipSmestaja"].AsString), document["ocena"].AsString));
            }
            return smestaji;
        }

        public void DodajSmestaj(Smestaj smestaj)
        {
            var document = new BsonDocument
            {
                { "_id", smestaj.Id },
                { "naziv", smestaj.Naziv },
                { "adresa", smestaj.Adresa },
                { "tipSmestaja", smestaj.Tip.ToString() },
                 { "ocena", smestaj.Ocena }
            };

            Baza.SmestajiKol.InsertOne(document);
        }

        public void IzmeniSmestaj(Smestaj smestaj)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", smestaj.Id);

            var update = Builders<BsonDocument>.Update
                .Set("naziv", smestaj.Naziv)
                .Set("adresa", smestaj.Adresa)
                .Set("tipSmestaja", smestaj.Tip.ToString())
                .Set("ocena", smestaj.Ocena);

            var updateResult = Baza.SmestajiKol.UpdateOne(filter, update);
            IzmeniSmestajUPutovanju(smestaj);
        }

        public void IzmeniSmestajUPutovanju(Smestaj s)
        {
            var documents = Baza.PutovanjaKol.Find(_ => true).ToList();

            foreach (var document in documents)
            {

                var filter = Builders<BsonDocument>.Filter.Eq("smestaji.Id", s.Id);
                var update = Builders<BsonDocument>.Update.Set("smestaji.$.Naziv", s.Naziv)
                                                          .Set("smestaji.$.Ocena", s.Ocena)
                                                          .Set("smestaji.$.Adresa", s.Adresa)
                                                          .Set("smestaji.$.Tip", s.Tip)
                                                          .Set("smestaji.$.tipSmestaja", s.Tip.ToString());

                Baza.PutovanjaKol.UpdateOne(filter, update);

            }
        }


        public void ObrisiSmestaj(Smestaj smestaj)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", smestaj.Id);

            DeleteResult result = Baza.SmestajiKol.DeleteOne(filter);
            ObrisiSmestajUPutovanju(smestaj);
        }

        public void ObrisiSmestajUPutovanju(Smestaj s)
        {
            var documents = Baza.PutovanjaKol.Find(_ => true).ToList();

            foreach (var document in documents)
            {

                var filter = Builders<BsonDocument>.Filter.Eq("smestaji.Id", s.Id);
                var update = Builders<BsonDocument>.Update.PullFilter("smestaji", Builders<BsonDocument>.Filter.Eq("Id", s.Id));

                Baza.PutovanjaKol.UpdateMany(filter, update);

            }
        }
        public List<Atrakcija> AtrakcijeZaPutovanje(string id)
        {
            List<Atrakcija> atrakcije = new List<Atrakcija>();
            var filter = new BsonDocument();
            var documents = Baza.AtrakcijeKol.Find(filter).ToList();
            foreach (var document in documents)
            {
                atrakcije.Add(new Atrakcija(document["_id"].AsString, document["naziv"].AsString, document["opis"].AsString, document["adresa"].AsString));
            }
            return atrakcije;
        }

        public List<Smestaj> SmestajZaPutovanje(string id)
        {
            List<Smestaj> smestaji = new List<Smestaj>();
            var filter = new BsonDocument();
            var documents = Baza.SmestajiKol.Find(filter).ToList();
            foreach (var document in documents)
            {
                smestaji.Add(new Smestaj(document["_id"].AsString, document["naziv"].AsString, document["adresa"].AsString, (TipSmestaja)Enum.Parse(typeof(TipSmestaja), document["tipSmestaja"].AsString), document["ocena"].AsString));
            }
            return smestaji;
        }
        public List<Restoran> RestoraniZaPutovanje(string id)
        {
            List<Restoran> restorani = new List<Restoran>();
            var filter = new BsonDocument();
            var documents = Baza.RestoraniKol.Find(filter).ToList();
            foreach (var document in documents)
            {
                restorani.Add(new Restoran(document["_id"].AsString, document["naziv"].AsString, document["adresa"].AsString, document["ocena"].AsString));
            }
            return restorani;
        }
        public void DodajPutovanje(string naziv, string brDana, string cena, DateTime dateTime, ObservableCollection<Atrakcija> atrakcije, ObservableCollection<Smestaj> smestaji, ObservableCollection<Restoran> restorani)
        {
            var atrakcijeDocuments = atrakcije.Select(a => a.ToBsonDocument()).ToList();
            var smestajiDocuments = smestaji.Select(s => s.ToBsonDocument()).ToList();
            var restoraniDocuments = restorani.Select(r => r.ToBsonDocument()).ToList();

            var document = new BsonDocument
            {
                { "naziv", naziv },
                { "brojDana", brDana },
                { "cena", cena },
                { "datum", dateTime },
                { "atrakcije", new BsonArray(atrakcijeDocuments) },
                { "smestaji", new BsonArray(smestajiDocuments) },
                { "restorani", new BsonArray(restoraniDocuments) }
            };

            Baza.PutovanjaKol.InsertOne(document);
        }

        public void IzmeniPutovanje(string id, string naziv, string brDana, string cena, DateTime dateTime, ObservableCollection<Atrakcija> atrakcije, ObservableCollection<Smestaj> smestaji, ObservableCollection<Restoran> restorani)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

            var update = Builders<BsonDocument>.Update
                .Set("naziv", naziv)
                .Set("brojDana", brDana)
                .Set("cena", cena)
                .Set("datum", dateTime)
                .Set("atrakcije", new BsonArray(atrakcije))
                .Set("smestaji", new BsonArray(smestaji))
                .Set("restorani", new BsonArray(restorani));

            var updateResult = Baza.PutovanjaKol.UpdateOne(filter, update);
        }

        public List<Putovanje> PronadjiPutovanja()
        {
            var documents = Baza.PutovanjaKol.Find(_ => true).ToList();
            var putovanja = documents.Select(p => BsonSerializer.Deserialize<Putovanje>(p)).ToList();
            return putovanja;
        }



        public void KupiPutovanje(string username, Putovanje putovanje)
        {
            var kupljeni = new Kupljeni
            {
                id = ObjectId.GenerateNewId().ToString(),
                Username = username,
                Putovanje = putovanje
            };

            Baza.KupljeniKol.InsertOne(kupljeni.ToBsonDocument());
        }
        public List<Putovanje> PronadjiKupovine(string username)
        {

            var documents = Baza.KupljeniKol.Find(_ => true).ToList();
            var rezervacije = documents.Select(p => BsonSerializer.Deserialize<Kupljeni>(p)).ToList();
            List<Putovanje> putovanja = new List<Putovanje>();
            foreach (var rezervacija in rezervacije)
            {

                if (rezervacija.Username == username)
                    putovanja.Add(rezervacija.Putovanje);

            }
            return putovanja;
        }

        public List<Izvestaj> IzvestajPoPutovanju(string naziv, string putovanjeId)
        {
            var documents = Baza.KupljeniKol.Find(_ => true).ToList();
            var kupljeni = documents.Select(p => BsonSerializer.Deserialize<Kupljeni>(p)).ToList();

            List<Kupljeni> kupljeniList = kupljeni
                .Where(k => k.Putovanje.Id == putovanjeId)
                .ToList();

            int brojKupljenih = kupljeniList.Count;

            List<Izvestaj> izvestajs = new List<Izvestaj>();

            Izvestaj izvestaj = new Izvestaj
            {
                RedniBroj = 1, 
                Naziv = naziv,  
                BrojKupljenih = brojKupljenih,
                KupljeniList = kupljeniList
            };
            izvestajs.Add(izvestaj);

            return izvestajs;
        }

        public List<Izvestaj> IzvestajPoDatumu(DateTime targetDate)
        {
            var documents = Baza.KupljeniKol.Find(_ => true).ToList();
            var kupljeni = documents.Select(p => BsonSerializer.Deserialize<Kupljeni>(p)).ToList();

            var kupljeniZaDatum = kupljeni
                .Where(k => k.Putovanje.Datum.Date == targetDate)
                .ToList();

            var putovanjeOccurrences = kupljeniZaDatum
                .GroupBy(k => k.Putovanje.Id)
                .Select(g => new { PutovanjeId = g.Key, PutovanjeNaziv = g.First().Putovanje.Naziv, Count = g.Count() })
                .ToList();

            List<Izvestaj> izvestaji = putovanjeOccurrences
                .Select((p, index) => new Izvestaj
                {
                    RedniBroj = index + 1,
                    Naziv = p.PutovanjeNaziv,
                    BrojKupljenih = p.Count,
                    KupljeniList = kupljeniZaDatum
                        .Where(k => k.Putovanje.Id == p.PutovanjeId)
                        .ToList()
                })
                .ToList();

            return izvestaji;
        }


        public List<Putovanje> PronadjiPopularneKupovine()
        {
            var documents = Baza.KupljeniKol.Find(_ => true).ToList();
            var rezervacije = documents.Select(p => BsonSerializer.Deserialize<Kupljeni>(p)).ToList();
            List<Putovanje> kupljeni = new List<Putovanje>();
            foreach (var k in rezervacije)
            {
                kupljeni.Add(k.Putovanje);

            }
            var mostOccurringIds = kupljeni
                .GroupBy(p => p.Id)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(3)
                .ToList();

            var kupljeniSet = new HashSet<Putovanje>(kupljeni);

            var mostOccurringPutovanja = kupljeniSet
                .Where(p => mostOccurringIds.Contains(p.Id))
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .Take(3)
                .ToList();
            return mostOccurringPutovanja;
        }

        public List<Putovanje> PronadjiRezervacije(string username)
        {

            var documents = Baza.RezervacijekOL.Find(_ => true).ToList();
            var rezervacije = documents.Select(p => BsonSerializer.Deserialize<Kupljeni>(p)).ToList();
            List<Putovanje> putovanja = new List<Putovanje>();
            foreach (var rezervacija in rezervacije)
            {

                if (rezervacija.Username == username)
                    putovanja.Add(rezervacija.Putovanje);

            }
            return putovanja;
        }

        public void RezervisiPutovanje(string username, Putovanje putovanje)
        {
            var rez = new Kupljeni
            {
                id = ObjectId.GenerateNewId().ToString(),
                Username = username,
                Putovanje = putovanje
            };

            Baza.RezervacijekOL.InsertOne(rez.ToBsonDocument());
        }

    }

}

