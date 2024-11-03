using McLaren_Store.Assets;
using McLaren_Store.DataBase;
using McLaren_Store.ViewModels;
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
	public partial class CarDetailsWindow : Window
	{
		private CarViewModel _car;

		public CarDetailsWindow(CarViewModel car)
		{
			InitializeComponent();
			DataContext = car;
			_car = car;
		}

		private async void BuyNow_Click(object sender, RoutedEventArgs e)
		{
			if (UserSession.Instance.IsLoggedIn)
			{
				var car = DataContext as CarViewModel;
				int customerId = UserSession.Instance.CurrentUser.Id;

				if (car != null)
				{
					await DBHelper.Instance.AddSaleAsync(car.CarID, customerId, car.Price);
					MessageBox.Show("Автомобиль успешно куплен!");
				}
			}
			else
			{
				MessageBox.Show("Для покупки автомобиля необходимо войти в систему.");
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