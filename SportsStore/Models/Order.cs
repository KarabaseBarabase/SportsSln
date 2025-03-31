using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsStore.Models
{
    public class Order
    {
		//public Order()
		//{
		//	Lines = new List<CartLine>(); 
		//}
		[BindNever] // не позволяет пользователю указывать значения для таких свойств в HTTP-запросе.
        public int OrderID { get; set; }
        

        [BindNever] // Блокирует применение инфраструктурой ASP.NET Core значения из HTTP-запроса при заполнении конфиденциальных или важных свойств модели.
        public ICollection<CartLine> Lines { get; set; } 

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please, enter the first address line")]
        public string Line1 { get; set; }
        
		public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please, enter the city name line")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please, enter a state name line")]
        public string State { get; set; }

        public string Zip {  get; set; }

        [Required(ErrorMessage = "Please, enter a country name line")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }

        [BindNever]
        public bool Shipped { get; set; }
    }
}
