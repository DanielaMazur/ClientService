using ClientService.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ClientService.Entities
{
     class Client : IClient
     {
          public readonly int Id = Guid.NewGuid().GetHashCode();
          public BlockingCollection<Action> clientTasks = new();

          public Client()
          {
               new Thread(() =>
               {

               }).Start();
          }

          public void SendOrder()
          {
               throw new NotImplementedException();
          }

          public void GetRestaurantsMenus()
          {
               throw new NotImplementedException();
          }

          public void PickUpOrder()
          {
               throw new NotImplementedException();
          }
     }
}
