using ListGenerator.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Common.Models
{
    public class JsonHelper : IJsonHelper
    {
        public string Serialize<T>(T model)
        {
            string json = JsonConvert.SerializeObject(model);
            return json;
        }
    }
}
