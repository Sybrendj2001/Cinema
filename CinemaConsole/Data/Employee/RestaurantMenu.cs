using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CinemaConsole.Data.Employee
{
	class RestaurantMenu
	{



		List<Tuple<string, double>> ProductList = new List<Tuple<string, double>>();

		void addItem(string name, double price)
		{
			for (int i = 0; i < ProductList.length; i++)
			{
				ProductList.Add(new Tuple<string, double>(name, price));
			}
		}

		void removeItem(string name)
		{
			for (int i = 0; i < ProductList.length; i++)
			{
				if (name == ProductList[i].Item1)
				{
					ProductList.RemoveAt(i);
				}
			}
		}
	}
}
