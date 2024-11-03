using McLaren_Store.Assets;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace McLaren_Store.Forms
{
	public partial class PersonalAccount : Window
	{
		private MainForm _mainForm;

		public PersonalAccount()
		{
			InitializeComponent();
			DataContext = UserSession.Instance;
			LoadOrdersAsync();
		}

		private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void CloseWindow_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private async void LoadOrdersAsync()
		{
			var orders = await DBHelper.Instance.GetOrdersByUserId(UserSession.Instance.CurrentUser.Id);

			if (orders == null || orders.Count == 0)
			{
				OrdersListView.ItemsSource = new List<Order>(); 
			}
			else
			{
				OrdersListView.ItemsSource = orders;
			}
		}

		private void BackToMenu_Click(object sender, RoutedEventArgs e)
		{
			if (_mainForm == null || !_mainForm.IsVisible)
			{
				_mainForm = new MainForm();
				_mainForm.Closed += (s, args) => _mainForm = null;
				_mainForm.Show();
				this.Close();
			}
			else
			{
				_mainForm.Activate();
			}
		}
	}
}
