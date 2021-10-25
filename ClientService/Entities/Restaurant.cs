using System.Collections.Generic;

namespace ClientService.Entities
{
     class Restaurant
     {
          public int Id { get; set; }
          public string Name { get; set; }
          public string Address { get; set; }
          public int MenuItems { get; set; }
          public List<MenuItem> Menu { get; set; }
          public int Rating { get; set; }
     }
}
