using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.Employee
{
	class RestaurantProduct
	{
		private int Pid { get; set; } = ProductID();
		private string name { get; set; }
		private double price { get; set; }

		public RestaurantProduct(string productName, double productPrice)
		{
			name = productName;
			price = productPrice;
		}

		public Tuple<int, string, double> getProductInfo()
		{
			int IDP = Pid;
			string pName = name;
			double pPrice = price;

			return Tuple.Create(IDP, pName, pPrice);
		}

		private static int ProductID()
		{
			int IDP;
			for (int i = 0; i < ProductList.productList.Count; i++)
			{
				IDP = i + 1;
				if (ProductList.productList[i] == null)
				{
					return IDP;
				}
				else if (ProductList.productList[i].getProductInfo().Item1 != IDP)
				{
					return IDP;
				}
			}
			return ProductList.productList.Count + 1;
		}
	}
}
