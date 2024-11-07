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
using static McLaren_Store.Assets.DBHelper;

namespace McLaren_Store.Forms
{
	/// <summary>
	/// Логика взаимодействия для Authorization.xaml
	/// </summary>
	public partial class Authorization : Window
	{
		private Registrations _registrationsWindow;

		public Authorization()
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

		private async void Vhod_Click(object sender, RoutedEventArgs e)
		{
			string username = LoginTextBox.Text;
			string password = PasswordBox.Password;

			var (userType, userId, firstName, lastName, Email, Adress,PhoneNumber) = await DBHelper.Instance.AuthenticateUser(username, password);

			if (userType != DBHelper.UserType.None)
			{
				// Сохраняем данные в сессию
				UserSession.Instance.SetUserData(userId, firstName, lastName, userType == DBHelper.UserType.Employee,Email,Adress,PhoneNumber);

				// Открываем нужное окно в зависимости от типа пользователя
				if (userType == DBHelper.UserType.Employee)
				{
					new AdminPanel().Show();
				}
				else
				{
					new PersonalAccount().Show();
				}

				this.Close();
			}
			else
			{
				MessageBox.Show("Неверные логин или пароль.");
			}
		}



		private void Reg_Click(object sender, RoutedEventArgs e)
		{
			if (_registrationsWindow == null || !_registrationsWindow.IsVisible)
			{
				_registrationsWindow = new Registrations();
				_registrationsWindow.Closed += (s, args) => _registrationsWindow = null;
				_registrationsWindow.Show();
				this.Close();
			}
			else
			{
				_registrationsWindow.Activate();
			}
		}


	}
}
