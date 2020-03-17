using System;
using System.Collections.Generic;


namespace Cinemaconsole.Data.Login
{
	public class LoginCredentials
	{

		public Dictionary<string, string> credentials = new Dictionary<string, string>();


		public LoginCredentials()
		{
			credentials.Add("sybrendj 1234", "Admin");
			credentials.Add("mark010 4567", "Admin");
			credentials.Add("timf 6789", "Admin");
			credentials.Add("janjaap 1010", "Retailer");
			credentials.Add("falco 2020", "Retailer");
			credentials.Add("kees 4040", "Ticketsalesman");
			credentials.Add("henk 8080", "Ticketsalesman");
		}
	}
}
