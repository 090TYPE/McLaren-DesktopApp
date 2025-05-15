using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using McLaren_Store.DataBase;
using McLaren_Store.ViewModels;
using static Syncfusion.Windows.Forms.TabBar;

namespace McLaren_Store.Assets
{

	internal class DBHelper
	{

		private static DBHelper _instance; // Экземпляр класса
		private readonly McLaren_StoreEntities3 _context; // Контекст базы данных

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
			_context = new McLaren_StoreEntities3();
		}

		public async Task<bool> IsUserNameAvailable(string userName)
		{
			var isCustomerExists = _context.Customers.Any(c => c.UserName == userName);
			var isEmployeeExists = _context.Employees.Any(e => e.UserName == userName);

			return !isCustomerExists && !isEmployeeExists;
		}
		public async Task<bool> HideSaleAsync(int saleId)
		{
			var sale = await _context.Sales.FirstOrDefaultAsync(s => s.SaleID == saleId);
			if (sale == null)
				return false;

			sale.visible = false;  // Скрываем заказ
			await _context.SaveChangesAsync();
			return true;
		}
		// Метод для добавления нового клиента
		public async Task<bool> RegisterCustomer(string userName, string password, string firstName, string lastName, string phoneNumber, string email, string address)
		{
			// Проверка на уникальность имени пользователя и email
			if (_context.Customers.Any(c => c.UserName == userName || c.Email == email))
			{
				return false; // Имя пользователя или email уже существуют
			}

			// Валидация пароля и других полей
			if (password.Length < 8 || !HasSpecialCharacter(password) || !HasUpperCase(password))
			{
				throw new ArgumentException("Пароль должен быть не менее 8 символов, содержать заглавные буквы и специальные символы.");
			}

			var customer = new Customers
			{
				UserName = userName,
				Password = password,
				FirstName = firstName,
				LastName = lastName,
				PhoneNumber = phoneNumber,
				Email = email,
				Address = address
			};

			_context.Customers.Add(customer);
			await _context.SaveChangesAsync();
			return true;
		}




		private bool HasSpecialCharacter(string input) => input.Any(ch => !char.IsLetterOrDigit(ch));
		private bool HasUpperCase(string input) => input.Any(char.IsUpper);


		public async Task<(UserType userType, int userId, string firstName, string lastName, string email, string address, string phoneNumber)> AuthenticateUser(string userName, string password)
		{
			// Проверка в таблице Customers
			var customer = await Task.Run(() => _context.Customers.FirstOrDefault(c => c.UserName == userName && c.Password == password));
			if (customer != null)
				return (UserType.Customer, customer.CustomerID, customer.FirstName, customer.LastName, customer.Email, customer.Address, customer.PhoneNumber);

			// Проверка в таблице Employees
			var employee = await Task.Run(() => _context.Employees.FirstOrDefault(e => e.UserName == userName && e.Password == password));
			if (employee != null)
				return (UserType.Employee, employee.EmployeeID, employee.FirstName, employee.LastName, null, null, null); 

			return (UserType.None, 0, null, null, null, null, null); // Если пользователь не найден
		}

		public async Task<UserData> GetUserDataAsync(int userId)
		{
			var user = await _context.Customers
				.Include("Sales") // Получение связанных данных о продажах
				.FirstOrDefaultAsync(u => u.CustomerID == userId);

			if (user != null)
			{
				return new UserData
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					PhoneNumber = user.PhoneNumber,
					Email = user.Email,
					Address = user.Address,
				};
			}
			return null;
		}

		public async Task<List<Order>> GetOrdersByUserId(int userId)
		{
			var orders = await _context.Sales
				.Where(o => o.CustomerID == userId)
				.Select(o => new
				{
					SaleDate = o.SaleDate,
					Model = o.Cars.Model,
					Price = o.SalePrice
				})
				.ToListAsync();


			return orders.Select(o => new Order
			{
				OrderDate = o.SaleDate.HasValue ? o.SaleDate.Value.ToString("dd/MM/yyyy") : "N/A",
				Model = o.Model,
				Price = o.Price
			}).ToList();
		}

		public async Task<List<CarViewModel>> GetAllCarsAsync()
		{
			return await _context.Cars
			.Where(car => car.Available == true) // Проверяем только доступные машины
			.Select(car => new CarViewModel
			{
				CarID = car.CarID,
				Model = car.Model,
				Price = car.Price,
				Image = car.Image,
				Available = car.Available,
				Brand = car.Brand,
				Transmission = car.Transmission,
				EngineType = car.EngineType,
				Color = car.Color
			})
			.ToListAsync();
		}

		public async Task AddCarAsync(Cars car)
		{
			_context.Cars.Add(car);
			await _context.SaveChangesAsync();
		}

		public async Task<List<DailySalesStatistics>> GetDailySalesStatisticsAsync()
		{
			var salesData = await _context.Sales
				.Where(sale => sale.SaleDate.HasValue) // Учитываем только записи с не-null датой
				.GroupBy(sale => DbFunctions.TruncateTime(sale.SaleDate)) // TruncateTime обрезает время
				.Select(group => new DailySalesStatistics
				{
					Date = group.Key.Value, // Используем обрезанную дату (без времени)
					SalesCount = group.Count()
				})
				.ToListAsync();

			return salesData;
		}


		public class DailySalesStatistics
		{
			public DateTime Date { get; set; } // Дата продажи
			public int SalesCount { get; set; } // Общая сумма продаж за день
		}

		public async Task AddSaleAsync(int carId, int customerId, decimal? salePrice)
		{
			if (salePrice.HasValue)
			{
				// Найти автомобиль по ID
				var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarID == carId);
				if (car == null)
				{
					throw new ArgumentException("Автомобиль с указанным ID не найден");
				}

				// Создание новой записи о продаже
				var sale = new Sales
				{
					CarID = carId,
					CustomerID = customerId,
					SaleDate = DateTime.Now,
					SalePrice = salePrice.Value
				};

				// Добавление записи о продаже
				_context.Sales.Add(sale);

				// Обновление статуса доступности автомобиля
				car.Available = false;

				// Сохранение изменений
				await _context.SaveChangesAsync();
			}
			else
			{
				throw new ArgumentException("Цена продажи не может быть null");
			}
		}
		// Пример: метод для получения клиента по имени пользователя
		public async Task<Customers> GetCustomerByUserName(string userName)
		{
			return await Task.Run(() => _context.Customers.FirstOrDefault(c => c.UserName == userName));
		}

		public async Task<List<Customers>> GetAllCustomersAsync()
		{
			return await _context.Customers.ToListAsync();
		}

		public async Task<List<Sales>> GetAllSalesAsync()
		{
			return await _context.Sales.ToListAsync();
		}

		public async Task<List<Employees>> GetAllEmployeesAsync()
		{
			return await _context.Employees.ToListAsync();
		}
		public async Task<List<Roles>> GetAllRolesAsync()
		{
			return await _context.Roles.ToListAsync();
		}
		public async Task<List<Cars>> GetAllCarsDBAsync()
		{
			return await _context.Cars.ToListAsync();
		}

		public async Task SaveChangesAsync()
		{
			// Обновите состояние изменённых и добавленных объектов
			foreach (var entry in _context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
			{
				_context.Entry(entry.Entity).State = entry.State;
			}

			await _context.SaveChangesAsync();
		}
		public async Task<List<SalesViewModel>> GetAllSalesDataAsync()
		{
			return await _context.Sales
				.Include(s => s.Cars)
				.Select(sale => new SalesViewModel
				{
					SaleDate = sale.SaleDate,
					Model = sale.Cars.Model,
					Price = sale.Cars.Price
				})
				.ToListAsync();
		}

		public class SalesViewModel
		{
			public DateTime? SaleDate { get; set; }
			public string Model { get; set; }
			public decimal? Price { get; set; }
		}
		public async Task AssignOrderToManagerAsync(int saleId, int managerId)
		{
			var exists = await _context.ManagerOrders.AnyAsync(mo => mo.SaleID == saleId);
			if (exists)
				return;

			var newManagerOrder = new ManagerOrders
			{
				SaleID = saleId,
				EmployeeID = managerId,
				OrderStatus = "Принят в работу"
			};

			_context.ManagerOrders.Add(newManagerOrder);

			// Скрываем заказ из списка доступных
			var sale = await _context.Sales.FirstOrDefaultAsync(s => s.SaleID == saleId);
			if (sale != null)
			{
				sale.visible = false;
			}

			await _context.SaveChangesAsync();
		}
		public async Task<List<Sales>> GetVisibleSalesAsync()
		{
			return await _context.Sales.Where(s => s.visible == true).ToListAsync();
		}
		public async Task<List<ManagerOrderViewModel>> GetOrdersForManagerAsync(int managerId)
		{
			var orders = await _context.ManagerOrders
				.Where(mo => mo.EmployeeID == managerId)
				.Include(mo => mo.Sales.Customers)
				.Include(mo => mo.Sales.Cars)
				.ToListAsync();

			return orders.Select(mo => new ManagerOrderViewModel
			{
				OrderId = mo.OrderID,
				ClientName = mo.Sales.Customers.FirstName + " " + mo.Sales.Customers.LastName,
				CarModel = mo.Sales.Cars.Model,
				Price = mo.Sales.SalePrice,
				OrderDate = mo.Sales.SaleDate,
				Status = mo.OrderStatus
			}).ToList();
		}
		public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
		{
			try
			{
				var order = await _context.ManagerOrders.FindAsync(orderId);
				if (order == null)
					return false;

				order.OrderStatus = newStatus;
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}
		public async Task<Sales> GetSaleByIdAsync(int? saleId)
		{
			return await _context.Sales.FirstOrDefaultAsync(s => s.SaleID == saleId);
		}

		public async Task<Customers> GetCustomerByIdAsync(int? customerId)
		{
			return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerID == customerId);
		}

		public async Task<Cars> GetCarByIdAsync(int? carId)
		{
			return await _context.Cars.FirstOrDefaultAsync(c => c.CarID == carId);
		}

	}
}
