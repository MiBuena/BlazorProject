using ListGenerator.Client.Enums;
using ListGenerator.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Services
{
    public class TableService : ITableService
    {

        public OverviewTableHeading NoSortingTableHeading { get; set; }
       
        public OverviewTableHeading AscendingSortingTableHeading { get; set; }

        public OverviewTableHeading DescendingSortingTableHeading { get; set; }


        public TableService()
        {
            InitializeSortingDirections();
        }

        private void InitializeSortingDirections()
        {
            this.NoSortingTableHeading = new OverviewTableHeading()
            {
                ImageUrl = "/Images/sort_both.png",
                SortingDirection = SortingDirection.NoSorting
            };

            this.AscendingSortingTableHeading = new OverviewTableHeading()
            {
                ImageUrl = "/Images/sort_asc.png",
                SortingDirection = SortingDirection.Ascending
            };

            this.DescendingSortingTableHeading = new OverviewTableHeading()
            {
                ImageUrl = "/Images/sort_desc.png",
                SortingDirection = SortingDirection.Descending
            };
        }

        public Table<T> Sort<T>(int id, Table<T> table)
        {
            var heading = table.Headings.FirstOrDefault(x => x.Id == id);

            if (heading.HeadingRule.SortingDirection == SortingDirection.Descending)
            {
                heading.HeadingRule = AscendingSortingTableHeading;
                table.Items = table.Items.OrderByDescending(x => heading.PropertyInfo.GetValue(x, null)).ToList();
            }
            else
            {
                heading.HeadingRule = DescendingSortingTableHeading;
                table.Items = table.Items.OrderBy(x => heading.PropertyInfo.GetValue(x, null)).ToList();
            }

            return table;
        }

        public Table<T> GetTable<T>(IEnumerable<T> items)
        {
            var headings = GetItemsOverviewHeadings();

            return new Table<T>()
            {
                Items = items,
                Headings = headings
            };
        }

        private List<Heading> GetItemsOverviewHeadings()
        {
            var defaultHeadingRule = new OverviewTableHeading()
            {
                ImageUrl = "/Images/sort_both.png",
                SortingDirection = 0
            };

            var tableHeadings = new List<Heading>();

            tableHeadings.Add(
                new Heading()
                {
                    Id = 0,
                    ThTitle = "Name",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("Name"),
                    HeadingRule = defaultHeadingRule,
                }
            ); ;

            tableHeadings.Add(
                new Heading()
                {
                    Id = 1,
                    ThTitle = "1 piece is consumed for (days)",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("ReplenishmentPeriod"),
                    HeadingRule = defaultHeadingRule,
                });


            tableHeadings.Add(
                new Heading()
                {
                    Id = 2,
                    ThTitle = "Last purchase quantity",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("LastReplenishmentQuantity"),
                    HeadingRule = defaultHeadingRule,
                }
            );

            tableHeadings.Add(
                new Heading()
                {
                    Id = 3,
                    ThTitle = "Last purchase date",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("LastReplenishmentDate"),
                    HeadingRule = defaultHeadingRule,
                });

            tableHeadings.Add(
                new Heading()
                {
                    Id = 4,
                    ThTitle = "Next replenishment date",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("NextReplenishmentDate"),
                    HeadingRule = defaultHeadingRule,
                });

            return tableHeadings;
        }

    }
}
