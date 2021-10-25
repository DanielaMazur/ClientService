using ClientService.Interfaces;
using ClientService.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClientService.Entities
{
     class Client : IClient
     {
          public readonly int Id = Guid.NewGuid().GetHashCode();
          public BlockingCollection<Action> clientTasks = new();

          private List<Restaurant> _restaurants = new();

          public Client()
          {
               new Thread(() =>
               {

               }).Start();

               GetRestaurantsMenus();
               SendOrder();
          }

          public void SendOrder()
          {
               var clientOrder = GenerateOrder();
               SendRequestService.SendPostRequest($"{Constants.FOOD_ORDERING_SERVICE_ADDRESS}/order", JsonConvert.SerializeObject(clientOrder));
          }

          public void GetRestaurantsMenus()
          {
               SendRequestService.SendGetRequest($"{Constants.FOOD_ORDERING_SERVICE_ADDRESS}/menu", out string response);
               var restaurantsMenuResponse = JsonConvert.DeserializeObject<RestaurantsMenuResponse>(response);
               this._restaurants = restaurantsMenuResponse.RestaurantsData;
          }

          public void PickUpOrder()
          {
               throw new NotImplementedException();
          }

          private ClientOrder GenerateOrder()
          {
               var random = new Random();
               var numberOfRestaurantsToOrder = random.Next(1, _restaurants.Count);
               var orderItems = new List<OrderItem>();
               var orderedRestaurantsIndexes = new List<int>();
               for (var i = 0; i < numberOfRestaurantsToOrder; i++)
               {
                    var restaurantIndex = random.Next(1, _restaurants.Count);
                    while (orderedRestaurantsIndexes.Contains(restaurantIndex))
                    {
                         restaurantIndex = random.Next(1, _restaurants.Count);
                    }
                    orderedRestaurantsIndexes.Add(restaurantIndex);
                    orderItems.Add(GenerateOrderFromRestaurant(_restaurants[restaurantIndex - 1]));
               }

               return new ClientOrder(this.Id, orderItems);
          }

          private OrderItem GenerateOrderFromRestaurant(Restaurant restaurant)
          {
               var random = new Random();
               var numberOfFoods = random.Next(1, restaurant.MenuItems);
               int[] foods = new int[numberOfFoods];
               for (var i = 0; i < numberOfFoods; i++)
               {
                    var foodId = random.Next(10);
                    while (foods.Contains(foodId))
                    {
                         foodId = random.Next(1, 10);
                    }
                    foods[i] = foodId;
               }
               var orderId = Guid.NewGuid().GetHashCode();
               var maxWait = restaurant.Menu.Where(item => foods.Contains(item.Id)).Select(item => item.PreparationTime).Max() * 1.3;
               return new OrderItem(orderId, foods, random.Next(1, 5), restaurant.Id, maxWait);
          }
     }
}
