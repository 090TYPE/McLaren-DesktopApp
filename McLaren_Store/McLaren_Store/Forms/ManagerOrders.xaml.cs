using McLaren_Store.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace McLaren_Store.Forms
{
	/// <summary>
	/// Логика взаимодействия для ManagerOrders.xaml
	/// </summary>
	public partial class ManagerOrders : Window
	{
		public ManagerOrders()
		{
			InitializeComponent();
			LoadManagerOrders();
			StatusComboBox.SelectedIndex = 0; // по умолчанию первый статус выбран
		}

		private async void LoadManagerOrders()
		{
			int currentManagerId = UserSession.Instance.CurrentUser.Id; // текущий менеджер
			var orders = await DBHelper.Instance.GetOrdersForManagerAsync(currentManagerId);
			ManagerOrdersListView.ItemsSource = orders;
		}

		private async void ChangeStatus_Click(object sender, RoutedEventArgs e)
		{
			// Получаем выбранный заказ
			var selectedOrder = ManagerOrdersListView.SelectedItem as ManagerOrderViewModel;
			if (selectedOrder == null)
			{
				MessageBox.Show("Выберите заказ для изменения статуса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Получаем выбранный статус из ComboBox
			var selectedStatusItem = StatusComboBox.SelectedItem as ComboBoxItem;
			if (selectedStatusItem == null)
			{
				MessageBox.Show("Выберите статус.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			string newStatus = selectedStatusItem.Content.ToString();

			// Обновляем статус в базе
			bool success = await DBHelper.Instance.UpdateOrderStatusAsync(selectedOrder.OrderId, newStatus);
			if (success)
			{
				MessageBox.Show("Статус заказа обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
				LoadManagerOrders(); // Обновить список заказов
			}
			else
			{
				MessageBox.Show("Ошибка при обновлении статуса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
}
