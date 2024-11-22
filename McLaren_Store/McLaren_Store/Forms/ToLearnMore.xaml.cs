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
	/// Логика взаимодействия для ToLearnMore.xaml
	/// </summary>
	public partial class ToLearnMore : Window
	{
		public ToLearnMore()
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
		private void BackToMainForm_Click(object sender, RoutedEventArgs e)
		{
			// Открываем MainForm
			MainForm mainForm = new MainForm();
			mainForm.Show();

			// Закрываем текущее окно
			this.Close();
		}
	}
}
