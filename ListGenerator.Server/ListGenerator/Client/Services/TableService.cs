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
        private List<OverviewTableHeading> GetSortingDirections()
        {
            var sortingRules = new List<OverviewTableHeading>();

            sortingRules.Add(new OverviewTableHeading()
            {
                ImageUrl = "/Images/sort_both.png",
                SortingDirection = SortingDirection.NoSorting
            });

            sortingRules.Add(new OverviewTableHeading()
            {
                ImageUrl = "/Images/sort_asc.png",
                SortingDirection = SortingDirection.Ascending
            });

            sortingRules.Add(new OverviewTableHeading()
            {
                ImageUrl = "/Images/sort_desc.png",
                SortingDirection = SortingDirection.Descending
            });

            return sortingRules;
        }

        public Table Sort(int id, IEnumerable<ItemOverviewViewModel> items)
        {
            var tableHeadings = GetItemsOverviewHeadings();

            var sortingRules = GetSortingDirections().ToList();

            var heading = tableHeadings.FirstOrDefault(x => x.Id == id);

            if (heading.SortingDirection == SortingDirection.NoSorting || heading.SortingDirection == SortingDirection.Ascending)
            {
                heading.SortingDirection++;
            }
            else
            {
                heading.SortingDirection--;
            }

            heading.HeadingRule = sortingRules.FirstOrDefault(x => x.SortingDirection == heading.SortingDirection);


            if (heading.SortingDirection == SortingDirection.Ascending)
            {
                items = items.OrderBy(x => heading.PropertyInfo.GetValue(x, null)).ToList();
            }
            else
            {
                items = items.OrderByDescending(x => heading.PropertyInfo.GetValue(x, null)).ToList();
            }


            return new Table()
            {
                Items = items,
                Headings = tableHeadings
            };
        }

        public Table GetTable(IEnumerable<ItemOverviewViewModel> items)
        {
            var headings = GetItemsOverviewHeadings();

            return new Table()
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
