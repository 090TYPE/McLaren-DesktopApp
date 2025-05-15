using McLaren_Store.Assets;
using System.Collections.ObjectModel;
using System.Windows;

namespace McLaren_Store.Forms
{
	public partial class AllOrders : Window
	{
		public ObservableCollection<OrderViewModel> Orders { get; set; }

		public AllOrders()
		{
			InitializeComponent();
			Orders = new ObservableCollection<OrderViewModel>();
			OrdersListView.ItemsSource = Orders;

			LoadOrders();
		}

		private async void LoadOrders()
		{
			Orders.Clear();
			var allSales = await DBHelper.Instance.GetVisibleSalesAsync();

			foreach (var sale in allSales)
			{
				var customer = await DBHelper.Instance.GetCustomerByIdAsync(sale.CustomerID);
				var car = await DBHelper.Instance.GetCarByIdAsync(sale.CarID);

				Orders.Add(new OrderViewModel
				{
					OrderId = sale.SaleID,
					ClientName = customer != null ? $"{customer.FirstName} {customer.LastName}" : "Неизвестно",
					CarModel = car?.Model ?? "Неизвестно",
					Price = sale.SalePrice.HasValue ? sale.SalePrice.Value.ToString("C") : "N/A",
					OrderDate = sale.SaleDate?.ToString("dd/MM/yyyy") ?? "N/A"
				});
			}
		}

		private async void TakeOrder_Click(object sender, RoutedEventArgs e)
		{
			if (OrdersListView.SelectedItem is OrderViewModel selectedOrder)
			{
				// Назначаем заказ менеджеру из UserSession
				int managerId = UserSession.Instance.CurrentUser.Id;
				await DBHelper.Instance.AssignOrderToManagerAsync(selectedOrder.OrderId, managerId);
				
				MessageBox.Show("Заказ взят в работу");
				LoadOrders();
			}
		}
		private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void CloseWindow_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}

	public class OrderViewModel
	{
		public int OrderId { get; set; }          // SaleID
		public string ClientName { get; set; }    // Имя клиента из Customers
		public string CarModel { get; set; }      // Модель машины из Cars
		public string Price { get; set; }         // SalePrice в строковом формате
		public string OrderDate { get; set; }     // SaleDate в строковом формате (например, "dd.MM.yyyy")
	}
}
