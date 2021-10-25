using System;

namespace ClientService.Entities
{
     class OrderItem
     {
          public readonly int Id;
          public readonly double MaxWait;
          public int[] Items { get; set; }
          public int Priority { get; set; }
          public int RestaurantId { get; set; }
          public long CreatedTime { get; set; }
          public OrderItem(int id, int[] items, int priority, int restaurantId, double maxWait)
          {
               Id = id;
               Items = items;
               Priority = priority;
               MaxWait = maxWait;
               RestaurantId = restaurantId;
               CreatedTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
          }
     }
}
