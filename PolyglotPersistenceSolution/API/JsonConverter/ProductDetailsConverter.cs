using Core.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.JsonConverter
{
    public class ProductDetailsConverter : JsonConverter<ProductDetailsModel>
    {
        public override ProductDetailsModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("Deserialization is not implemented.");
        }

        public override void Write(Utf8JsonWriter writer, ProductDetailsModel value, JsonSerializerOptions options)
        {
            if (value is CarDetailsModel carDetails)
            {
                JsonSerializer.Serialize(writer, carDetails, options);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}
