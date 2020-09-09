using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.ViewModels
{
    public class Heading
    {
        public int Id { get; set; }

        public string ThTitle { get; set; }

        public string PropertyName { get; set; }

        public int SortingDirection { get; set; }
        public OverviewTableHeading HeadingRule { get; set; }
    }
}
