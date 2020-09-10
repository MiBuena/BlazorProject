using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ListGenerator.Client.ViewModels
{
    public class Table<T>
    {
        public List<Heading> Headings { get; set; } = new List<Heading>();

        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}
