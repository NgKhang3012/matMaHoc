using System;
using System.IO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace FM
{
    public class FModel
    {
        public string Id { get; set; }

        [BsonElement("FilePdfName")]
        public string FilePdfName { get; set; }

        [BsonElement("ContentPdf")]
        public byte[] ContentPdf { get; set; }

        [BsonElement("FileSigName")]
        public string FileSigName { get; set; }

        [BsonElement("ContentSig")]
        public string ContentSig { get; set; }

        [BsonElement("FilePubName")]
        public string FilePubName { get; set; }

        [BsonElement("ContentPub")]
        public string ContentPub { get; set; }
        public string IdSeller { get; set; }
    }
}