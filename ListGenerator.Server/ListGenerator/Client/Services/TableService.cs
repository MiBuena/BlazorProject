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
        public TableHeading NoSortingTableHeading { get; set; }
       
        public TableHeading AscendingSortingTableHeading { get; set; }

        public TableHeading DescendingSortingTableHeading { get; set; }


        public TableService()
        {
            InitializeSortingDirections();
        }

        private void InitializeSortingDirections()
        {
            this.NoSortingTableHeading = new TableHeading()
            {
                ImageUrl = "/Images/sort_both.png",
                SortingDirection = SortingDirection.NoSorting
            };

            this.AscendingSortingTableHeading = new TableHeading()
            {
                ImageUrl = "/Images/sort_asc.png",
                SortingDirection = SortingDirection.Ascending
            };

            this.DescendingSortingTableHeading = new TableHeading()
            {
                ImageUrl = "/Images/sort_desc.png",
                SortingDirection = SortingDirection.Descending
            };
        }

        public Table<T> Sort<T>(int id, Table<T> table)
        {
            var headingToSort = table.Headings.FirstOrDefault(x => x.Id == id);
            var headingsToRegenerate = table.Headings.Where(x => x.Id != id).ToList();
            var tableHeadings = RegenerateHeadings(headingsToRegenerate);
         
            if (headingToSort.HeadingRule.SortingDirection == SortingDirection.Descending)
            {
                headingToSort.HeadingRule = AscendingSortingTableHeading;
                table.Items = table.Items.OrderBy(x => headingToSort.PropertyInfo.GetValue(x, null)).ToList();
            }
            else
            {
                headingToSort.HeadingRule = DescendingSortingTableHeading;
                table.Items = table.Items.OrderByDescending(x => headingToSort.PropertyInfo.GetValue(x, null)).ToList();
            }

            tableHeadings.Add(headingToSort);

            return table;
        }

        public Table<ItemOverviewViewModel> GetItemsOverviewTable(IEnumerable<ItemOverviewViewModel> items)
        {
            var headings = GetItemsOverviewHeadings();

            return new Table<ItemOverviewViewModel>()
            {
                Items = items,
                Headings = headings
            };
        }

        private ICollection<Heading> RegenerateHeadings(ICollection<Heading> headingsToRegenerate)
        {
            foreach (var heading in headingsToRegenerate)
            {
                heading.HeadingRule = this.NoSortingTableHeading;
            }

            return headingsToRegenerate;
        }

        private List<Heading> GetItemsOverviewHeadings()
        {
            var tableHeadings = new List<Heading>();

            tableHeadings.Add(
                new Heading()
                {
                    Id = 0,
                    ThTitle = "Name",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("Name"),
                    HeadingRule = NoSortingTableHeading,
                }
            ); ;

            tableHeadings.Add(
                new Heading()
                {
                    Id = 1,
                    ThTitle = "1 piece is consumed for (days)",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("ReplenishmentPeriod"),
                    HeadingRule = NoSortingTableHeading,
                });


            tableHeadings.Add(
                new Heading()
                {
                    Id = 2,
                    ThTitle = "Last purchase quantity",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("LastReplenishmentQuantity"),
                    HeadingRule = NoSortingTableHeading,
                }
            );

            tableHeadings.Add(
                new Heading()
                {
                    Id = 3,
                    ThTitle = "Last purchase date",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("LastReplenishmentDate"),
                    HeadingRule = NoSortingTableHeading,
                });

            tableHeadings.Add(
                new Heading()
                {
                    Id = 4,
                    ThTitle = "Next replenishment date",
                    PropertyInfo = typeof(ItemOverviewViewModel).GetProperty("NextReplenishmentDate"),
                    HeadingRule = AscendingSortingTableHeading,
                });

            return tableHeadings;
        }
    }
}
