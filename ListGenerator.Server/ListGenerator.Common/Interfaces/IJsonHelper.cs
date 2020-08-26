using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Common.Interfaces
{
    public interface IJsonHelper
    {
        string Serialize2<T>(T model);
        Task<T> Deserialize2<T>(Stream value);

        string Serialize<T>(T model);

        T Deserialize<T>(string value);
    }
}
