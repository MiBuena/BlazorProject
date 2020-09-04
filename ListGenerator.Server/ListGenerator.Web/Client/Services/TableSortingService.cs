using ListGenerator.Web.Client.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Services
{
    public class TableSortingService : ITableSortingService
    {
        //public List<OverviewTableHeadings> SortingRules { get; set; } = new List<OverviewTableHeadings>();

        //public List<Heading> TableHeadings { get; set; } = new List<Heading>();


        //public List<Heading> Initialize()
        //{
        //    this.SortingRules.Add(new OverviewTableHeadings()
        //    {
        //        ImageUrl = "/Images/sort_both.png",
        //        SortingDirection = 0
        //    });

        //    this.SortingRules.Add(new OverviewTableHeadings()
        //    {
        //        ImageUrl = "/Images/sort_asc.png",
        //        SortingDirection = 1
        //    });

        //    this.SortingRules.Add(new OverviewTableHeadings()
        //    {
        //        ImageUrl = "/Images/sort_desc.png",
        //        SortingDirection = 2
        //    });

        //    var defaultHeadingRule = this.SortingRules.First(x => x.SortingDirection == 0);

        //    TableHeadings.Add(
        //        new Heading()
        //        {
        //            ThTitle = "Name",
        //            HeadingRule = defaultHeadingRule
        //        }
        //    ); ;

        //    TableHeadings.Add(
        //        new Heading()
        //        {
        //            ThTitle = "1 piece is consumed for (days)",
        //            HeadingRule = defaultHeadingRule
        //        });


        //    TableHeadings.Add(
        //        new Heading()
        //        {
        //            ThTitle = "Last purchase quantity",
        //            HeadingRule = defaultHeadingRule
        //        }
        //    );

        //    TableHeadings.Add(
        //        new Heading()
        //        {
        //            ThTitle = "Last purchase date",
        //            HeadingRule = defaultHeadingRule
        //        });

        //    TableHeadings.Add(
        //        new Heading()
        //        {
        //            ThTitle = "Last purchase date",
        //            HeadingRule = defaultHeadingRule
        //        });

        //    return TableHeadings;
        //}

        //public void ChangeDirection(string directionName)
        //{
        //    var heading = this.TableHeadings.FirstOrDefault(x => x.ThTitle == directionName);

        //    heading.SortingDirection = (heading.SortingDirection + 1) % 3;

        //    heading.HeadingRule = this.SortingRules.FirstOrDefault(x => x.SortingDirection == heading.SortingDirection);
        //}
    }
}
