using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.JsonWorker
{
    public sealed class JsonWorkerClass
    {
        private readonly string _jsonFilePath;

        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public JsonWorkerClass(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }

        public List<T> ReadObjectsFromFile<T>(string file)
        {
            string path = Path.Combine(_jsonFilePath,file);

            using StreamReader streamReader = new StreamReader(path);
            var json = streamReader.ReadToEnd();
            List<T> objects= JsonSerializer.Deserialize<List<T>>(json,_options);
            return objects;
        }
    }
}
