using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TouristAgency.Model
{
    class Kupljeni
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        private string username;

        private Putovanje putovanje;


        public string Username { get => username; set => username = value; }

        public Putovanje Putovanje { get => putovanje; set => putovanje = value; }
    }
}
