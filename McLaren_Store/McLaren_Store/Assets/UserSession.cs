using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McLaren_Store.Assets
{
	public class UserSession
	{
		private static UserSession _instance;
		public static UserSession Instance => _instance ?? (_instance = new UserSession());

		public int UserId { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public bool IsEmployee { get; private set; }

		private UserSession() { }

		public void SetUserData(int userId, string firstName, string lastName, bool isEmployee)
		{
			UserId = userId;
			FirstName = firstName;
			LastName = lastName;
			IsEmployee = isEmployee;
		}

		public void ClearSession()
		{
			UserId = 0;
			FirstName = null;
			LastName = null;
			IsEmployee = false;
		}
	}
}
