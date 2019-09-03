namespace SW.TechnicalAssignment.DataAccess
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class EnumerationDefaultConverter<T> : StringEnumConverter
    {
        private readonly T fallback;

        public EnumerationDefaultConverter(T fallback)
        {
            this.fallback = fallback;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException)
            {
                return fallback;
            }
        }
    }
}
