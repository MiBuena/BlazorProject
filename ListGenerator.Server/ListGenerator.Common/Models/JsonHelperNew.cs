using ListGenerator.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Common.Models
{
    class JsonHelperNew
    {
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
