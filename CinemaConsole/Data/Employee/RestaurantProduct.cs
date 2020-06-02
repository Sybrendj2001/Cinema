using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data.Employee
{
	class RestaurantProduct
	{
		/// <summary>Product ID</summary>
		private int Pid { get; set; } = ProductID();
		/// <summary>Product Name</summary>
		private string Name { get; set; }
		/// <summary>Product Price</summary>
		private double Price { get; set; }

		/// <summary>
		/// Create a product
		/// </summary>
		/// <param name="ProductName">Name of the product</param>
		/// <param name="ProductPrice">Price of the product</param>
		public RestaurantProduct(string ProductName, double ProductPrice)
		{
			Name = ProductName;
			Price = ProductPrice;
		}

		/// <summary>
		/// Gets the info of a product
		/// </summary>
		/// <returns>Info of the products</returns>
		public Tuple<int, string, double> getProductInfo()
		{
			int IDP = Pid;
			string pName = Name;
			double pPrice = Price;

			return Tuple.Create(IDP, pName, pPrice);
		}

		/// <summary>
		/// Checks the ID of the product that it would get
		/// </summary>
		/// <returns>The ID of the product</returns>
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
