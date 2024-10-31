using Core.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.JsonConverter
{
    public class ProductDetailsConverter : JsonConverter<ProductDetailsModel>
    {
        public override ProductDetailsModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument document = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = document.RootElement;
                Console.WriteLine(root.ToString());

                // Odluka o tipu na osnovu prisutnih svojstava
                if (root.TryGetProperty("enginePower", out _) && root.TryGetProperty("yearManufactured", out _))
                {
                    return JsonSerializer.Deserialize<CarDetailsModel>(root.GetRawText(), options);
                }
                // Dodajte druge provere za DeviceDetailsModel, MovieDetailsModel, itd.

                // Podrazumevani slučaj ako ni jedan specifičan tip nije prepoznat
                throw new Exception("Jebem ti mater");
            }
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
