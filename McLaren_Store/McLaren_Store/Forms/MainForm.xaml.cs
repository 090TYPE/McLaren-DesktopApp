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
	/// Логика взаимодействия для MainForm.xaml
	/// </summary>

	public partial class MainForm : Window
	{
		private Authorization _authorizationWindow;
		private PersonalAccount _personalAccountWindow;
		private CarsWindow _cars;
		private ToLearnMore _learnMore;

		public MainForm()
		{
			InitializeComponent();

		}

		private void MinimizeWindow_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void CloseWindow_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void PersonalAccount_Click(object sender, RoutedEventArgs e)
		{
			// Проверяем, есть ли активный пользователь в UserSession
			if (!UserSession.Instance.IsLoggedIn)
			{
				if (_authorizationWindow == null || !_authorizationWindow.IsVisible)
				{
					_authorizationWindow = new Authorization();
					_authorizationWindow.Closed += (s, args) => _authorizationWindow = null;
					_authorizationWindow.Show();
					this.Close();
				}
				else
				{
					_authorizationWindow.Activate();
				}
			}
			else
			{
				OpenPersonalAccount(); // Открываем личный кабинет, если пользователь авторизован
			}
		}
		private void LearnMore_Click(object sender, RoutedEventArgs e)
		{
			// Проверяем, есть ли активный пользователь в UserSession
			
				if (_learnMore == null || !_learnMore.IsVisible)
				{
					_learnMore = new ToLearnMore();
					_learnMore.Closed += (s, args) => _learnMore = null;
					_learnMore.Show();
					this.Close();
				}
				else
				{
					_learnMore.Activate();
				}
			

		}

		private void OpenPersonalAccount()
		{
			if (_personalAccountWindow == null || !_personalAccountWindow.IsVisible)
			{
				_personalAccountWindow = new PersonalAccount();
				_personalAccountWindow.Closed += (s, args) => _personalAccountWindow = null;
				_personalAccountWindow.Show();
				this.Close();
			}
			else
			{
				_personalAccountWindow.Activate();
			}
		}

		private void Buy_Click(object sender, RoutedEventArgs e)
		{
			// Проверяем, есть ли активный пользователь в UserSession
			if (!UserSession.Instance.IsLoggedIn)
			{
				if (_authorizationWindow == null || !_authorizationWindow.IsVisible)
				{
					_authorizationWindow = new Authorization();
					_authorizationWindow.Closed += (s, args) => _authorizationWindow = null;
					_authorizationWindow.Show();
					this.Close();
				}
				else
				{
					_authorizationWindow.Activate();
				}
			}
			else
			{
				// Если пользователь авторизован, открываем окно с автомобилями
				if (_cars == null || !_cars.IsVisible)
				{
					_cars = new CarsWindow();
					_cars.Closed += (s, args) => _cars = null;
					_cars.Show();
					this.Close();
				}
				else
				{
					_cars.Activate(); // Если окно уже открыто, активируем его
				}
			}
		}
	}
}

