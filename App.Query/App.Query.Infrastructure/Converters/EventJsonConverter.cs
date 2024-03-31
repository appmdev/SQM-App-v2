using App.Common.Events;
using CQRS.Core.Events;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace App.Query.Infrastructure.Converters
{
    public class EventJsonConverter : JsonConverter<BaseEvent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
        }
        public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}!");
            }
            if (!doc.RootElement.TryGetProperty("Type", out var type))
            {
                throw new JsonException("Could not detect the Type discriminator property");
            }

            var typeDiscriminator = type.GetString();
            var json = doc.RootElement.GetRawText();

            return typeDiscriminator switch
            {
                nameof(MapCreatedEvent) => JsonSerializer.Deserialize<MapCreatedEvent>(json, options),
                nameof(MapRemovedEvent) => JsonSerializer.Deserialize<MapRemovedEvent>(json, options),
                nameof(PointcloudAddedEvent) => JsonSerializer.Deserialize<PointcloudAddedEvent>(json, options),
                nameof(StateAddedEvent) => JsonSerializer.Deserialize<StateAddedEvent>(json, options),
                _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
            };
        }

        public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}