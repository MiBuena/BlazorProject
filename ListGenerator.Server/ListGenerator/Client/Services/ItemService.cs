using AutoMapper;
using ListGenerator.Client.Models;
using ListGenerator.Shared.Dtos;
using ListGenerator.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListGenerator.Client.Interfaces;
using ListGenerator.Shared.Interfaces;
using System.Linq;
using ListGenerator.Client.Enums;

namespace ListGenerator.Client.Services
{
    public class ItemService : IItemService
    {
        private const string SaveItemSuccessMessage = "The item was saved successfully";
        private const string SaveItemErrorMessage = "An error occurred while saving the item";

        private readonly IApiClient _apiClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IMapper _mapper;

        public ItemService(IApiClient apiClient, IJsonHelper jsonHelper, IMapper mapper)
        {
            _apiClient = apiClient;
            _jsonHelper = jsonHelper;
            _mapper = mapper;
        }

        public List<OverviewTableHeading> GetSortingDirections()
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

            if(heading.SortingDirection == SortingDirection.NoSorting || heading.SortingDirection == SortingDirection.Ascending)
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


        public List<Heading> GetItemsOverviewHeadings()
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

        public async Task<IEnumerable<ItemOverviewDto>> GetItemsOverviewModels()
        {
            var dtos = await _apiClient.GetAsync<IEnumerable<ItemOverviewDto>>("api/items/overview");

            return dtos;
        }

        public async Task<ItemDto> GetItem(int id)
        {
            var dto = await _apiClient.GetAsync<ItemDto>($"api/items/{id}");
            return dto;
        }

        public async Task<ApiResponse> AddItem(ItemViewModel item)
        {
            var itemDto = _mapper.Map<ItemViewModel, ItemDto>(item);

            var itemJson = _jsonHelper.Serialize(itemDto);

            var response = await _apiClient.PostAsync("api/items", itemJson, SaveItemErrorMessage);

            return response;
        }

        public async Task<ApiResponse> UpdateItem(ItemViewModel item)
        {
            var itemDto = _mapper.Map<ItemViewModel, ItemDto>(item);

            var itemJson = _jsonHelper.Serialize(itemDto);

            var response = await _apiClient.PutAsync("api/items", itemJson, SaveItemErrorMessage);

            return response;
        }

        public async Task<ApiResponse> DeleteItem(int itemId)
        {
            var response = await _apiClient.DeleteAsync($"api/items/{itemId}");
            return response;
        }

        public async Task<IEnumerable<ItemDto>> GetShoppingListItems(DateTime secondReplenishmentDate)
        {
            var internationalDateTime = secondReplenishmentDate.ToString("dd-MM-yyyy");
            var dtos = await _apiClient.GetAsync<IEnumerable<ItemDto>>($"api/items/shoppinglist/{internationalDateTime}");

            return dtos;
        }
    }
}
