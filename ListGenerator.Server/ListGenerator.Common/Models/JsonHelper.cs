using ListGenerator.Common.Interfaces;
using Newtonsoft.Json;
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
        public string Serialize2<T>(T model)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(model);
            return json;
        }

        public async Task<T> Deserialize2<T>(Stream value)
        {       
            var deserializedObject = await System.Text.Json.JsonSerializer.DeserializeAsync<T>(value, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return deserializedObject;
        }

        public string Serialize<T>(T model)
        {
            string json = JsonConvert.SerializeObject(model);
            return json;
        }

        public T Deserialize<T>(string value)
        {
            var deserializedObject = JsonConvert.DeserializeObject<T>(value);
            return deserializedObject;
        }
    }
}
