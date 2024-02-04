using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CQRS.Core.Events
{
    public class EventModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set; }
        public DateTime TimeStamp {get; set; }
        public Guid AggreagateIdentifier{ get; set; }
        public string AggregateType {get; set; }
        public int Version {get; set; }
        public string EventType {get; set; }
        public BaseEvent EventData {get; set; }
    }
}