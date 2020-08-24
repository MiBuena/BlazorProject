using ListGenerator.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ListGenerator.Common.Models
{
    public class JsonHelper : IJsonHelper
    {
        public string Serialize<T>(T model)
        {
            string json = JsonSerializer.Serialize(model);
            return json;
        }

        public async Task<T> Deserialize<T>(Stream value)
        {       
            var deserializedObject = await JsonSerializer.DeserializeAsync<T>(value, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return deserializedObject;
        }
    }
}
