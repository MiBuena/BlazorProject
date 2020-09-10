using ListGenerator.Client.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ListGenerator.Client.ViewModels
{
    public class Heading
    {
        public int Id { get; set; }

        public string ThTitle { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public SortingDirection SortingDirection { get; set; }
        public OverviewTableHeading HeadingRule { get; set; }
    }
}
