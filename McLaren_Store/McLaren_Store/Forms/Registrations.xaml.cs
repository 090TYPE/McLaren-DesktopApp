using McLaren_Store.Assets;
using System;
using System.Windows;

namespace McLaren_Store.Forms
{
	/// <summary>
	/// Логика взаимодействия для Registrations.xaml
	/// </summary>
	public partial class Registrations : Window
	{
		private Authorization _authorizationWindow;

		public Registrations()
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

		private async void Reg_Click(object sender, RoutedEventArgs e)
		{
			// Используйте синглтон DBHelper
			var dbHelper = DBHelper.Instance;

			string userName = LoginTextBox.Text;
			string password = PasswordBox1.Password;
			string firstName = FirstNameTextBox.Text;
			string lastName = LastNameTextBox.Text;

			bool success = await dbHelper.RegisterCustomer(userName, password, firstName, lastName);

			if (success)
			{
				MessageBox.Show("Регистрация прошла успешно!");
			}
			else
			{
				MessageBox.Show("Ошибка: логин уже существует или произошла другая ошибка.");
			}
		}

		private void Back_Click(object sender, RoutedEventArgs e)
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
	}
}
