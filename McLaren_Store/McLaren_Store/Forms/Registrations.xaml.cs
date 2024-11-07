using McLaren_Store.Assets;
using System;
using System.Net;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
			string phoneNumber = PhoneNumberTextBox.Text;
			string email = EmailTextBox.Text;
			string address = AddressTextBox.Text;

			try
			{
				bool success = await dbHelper.RegisterCustomer(userName, password, firstName, lastName, phoneNumber, email, address);

				if (success)
				{
					MessageBox.Show("Регистрация прошла успешно!");
				}
				else
				{
					MessageBox.Show("Ошибка: логин или email уже существует.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка: {ex.Message}");
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
