using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ListGenerator.Common.Interfaces
{
    public interface IJsonHelper
    {
        string Serialize<T>(T model);
        Task<T> Deserialize<T>(Stream value);
    }
}
