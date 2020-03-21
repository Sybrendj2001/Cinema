using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaConsole.Data
{
    class RestaurantMenu
    {
		List<> ProductList = new List<>();

        void printList()
        {
            for (int i = 0; i < ProductList.length; i++)
			{
                Console.WriteLine("[" + i + "]" + " " + ProductList[i.Item1] + ", " + ProductList[i.Item2]);
			}

        }

        void addItem(string name, double price)
        {
            ProductList.Add(Tuple.Create(name, price));
        }

        void removeItem(string name)
        {
            for (int i = 0; i < ProductList.Length; i++)
            {
                if(ProductList[i.Item1] == name)
                {
                    ProductList.RemoveAt(i);
                }
            }
        }
    }
}
