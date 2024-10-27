using System.Linq;
using System.Threading.Tasks;
using McLaren_Store.DataBase;

namespace McLaren_Store.Assets
{
	internal class DBHelper
	{
		private static DBHelper _instance; // Экземпляр класса
		private readonly McLaren_StoreEntities _context; // Контекст базы данных

		// Синглтон для доступа к классу
		public static DBHelper Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DBHelper();
				}
				return _instance;
			}
		}

		public enum UserType
		{
			None,
			Customer,
			Employee
		}

		private DBHelper()
		{
			_context = new McLaren_StoreEntities();
		}

		public async Task<bool> IsUserNameAvailable(string userName)
		{
			var isCustomerExists = _context.Customers.Any(c => c.UserName == userName);
			var isEmployeeExists = _context.Employees.Any(e => e.UserName == userName);

			return !isCustomerExists && !isEmployeeExists;
		}

		// Метод для добавления нового клиента
		public async Task<bool> RegisterCustomer(string userName, string password, string firstName, string lastName)
		{
			if (!await IsUserNameAvailable(userName))
				return false;

			var newCustomer = new Customers
			{
				UserName = userName,
				Password = password,
				FirstName = firstName,
				LastName = lastName
			};

			_context.Customers.Add(newCustomer);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<(UserType userType, int userId, string firstName, string lastName)> AuthenticateUser(string userName, string password)
		{
			// Проверка в таблице Customers
			var customer = await Task.Run(() => _context.Customers.FirstOrDefault(c => c.UserName == userName && c.Password == password));
			if (customer != null)
				return (UserType.Customer, customer.CustomerID, customer.FirstName, customer.LastName);

			// Проверка в таблице Employees
			var employee = await Task.Run(() => _context.Employees.FirstOrDefault(e => e.UserName == userName && e.Password == password));
			if (employee != null)
				return (UserType.Employee, employee.EmployeeID, employee.FirstName, employee.LastName);

			return (UserType.None, 0, null, null); // Если пользователь не найден
		}

		// Пример: метод для получения клиента по имени пользователя
		public async Task<Customers> GetCustomerByUserName(string userName)
		{
			return await Task.Run(() => _context.Customers.FirstOrDefault(c => c.UserName == userName));
		}
	}
}
