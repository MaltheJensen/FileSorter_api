using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nederman_api.Model
{
    public class Company
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string ContactPerson { get; set; }
        public List<string> JobInstrutionsFileName { get; set; }
        public List<byte[]> JobInstrutions { get; set; }
        public List<string> RapportsFileName { get; set; }
        public List<byte[]> Rapports { get; set; }

    }
}
