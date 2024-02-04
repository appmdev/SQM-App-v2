using CQRS.Core.Maps;

namespace CQRS.Core.Events
{
    public class BaseEvent : Map
    {
        protected BaseEvent(string type)
        {
            this.Type = type;
        }

        public int Version { get; set; }
        public string Type { get; set; }
    }
}