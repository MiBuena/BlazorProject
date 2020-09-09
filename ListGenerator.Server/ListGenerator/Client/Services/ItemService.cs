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
                SortingDirection = 0
            });

            sortingRules.Add(new OverviewTableHeading()
            {
                ImageUrl = "/Images/sort_asc.png",
                SortingDirection = 1
            });

            sortingRules.Add(new OverviewTableHeading()
            {
                ImageUrl = "/Images/sort_desc.png",
                SortingDirection = 2
            });

            return sortingRules;
        }


        public Table Sort(int id, IEnumerable<ItemOverviewViewModel> items)
        {
            var tableHeadings = GetItemsOverviewHeadings();

            var sortingRules = GetSortingDirections().ToList();

            var heading = tableHeadings.FirstOrDefault(x => x.Id == id);

            heading.SortingDirection = (heading.SortingDirection + 1) % 3;

            heading.HeadingRule = sortingRules.FirstOrDefault(x => x.SortingDirection == heading.SortingDirection);

            var propertyInfo = typeof(ItemOverviewViewModel).GetProperty(heading.PropertyName);

            if (heading.SortingDirection == 1)
            {
                items = items.OrderBy(x => propertyInfo.GetValue(x, null));
            }
            else
            {
                items = items.OrderByDescending(x => propertyInfo.GetValue(x, null));
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
                    PropertyName = "Name",
                    HeadingRule = defaultHeadingRule,
                }
            ); ;

            tableHeadings.Add(
                new Heading()
                {
                    Id = 1,
                    ThTitle = "1 piece is consumed for (days)",
                    PropertyName = "ReplenishmentPeriod",
                    HeadingRule = defaultHeadingRule,
                });


            tableHeadings.Add(
                new Heading()
                {
                    Id = 2,
                    ThTitle = "Last purchase quantity",
                    PropertyName = "LastReplenishmentQuantity",
                    HeadingRule = defaultHeadingRule,
                }
            );

            tableHeadings.Add(
                new Heading()
                {
                    Id = 3,
                    ThTitle = "Last purchase date",
                    PropertyName = "LastReplenishmentDate",
                    HeadingRule = defaultHeadingRule,
                });

            tableHeadings.Add(
                new Heading()
                {
                    Id = 4,
                    ThTitle = "Next replenishment date",
                    PropertyName = "NextReplenishmentDate",
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
