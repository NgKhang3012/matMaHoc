using System;
using System.IO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace Person
{
    public class PersonModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string pubkeyname { get; set; }
        public string pubkeycontent { get; set; }
    }
}