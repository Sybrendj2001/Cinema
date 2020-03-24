using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.Employee
{
	class RestaurantProduct
	{
		private string name { get; set; }
		private double price { get; set; }



		public RestaurantProduct(string productName, double productPrice)
		{
			productName = name;
			productPrice = price;
		}

		public Tuple<string, double> getProductInfo()
		{
			string pName = name;
			double pPrice = price;

			return Tuple.Create(pName, pPrice);
		}



	}
}
