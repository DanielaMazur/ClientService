using ClientService.Entities;

namespace ClientService
{
     class Program
     {
          static void Main(string[] args)
          {
               HTTPServer server = new();
               server.Start();
               new Client();
          }
     }
}
