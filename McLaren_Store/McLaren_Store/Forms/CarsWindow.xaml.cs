using McLaren_Store.Assets;
using McLaren_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace McLaren_Store.Forms
{
	/// <summary>
	/// Логика взаимодействия для Cars.xaml
	/// </summary>
	public partial class CarsWindow : Window
	{
		public CarsWindow()
		{
			InitializeComponent();
			LoadCarsAsync();
		}

		private async void LoadCarsAsync()
		{
			var cars = await DBHelper.Instance.GetAllCarsAsync();
			CarsItemsControl.ItemsSource = cars; // Обновлено с CarsListView на CarsItemsControl
		}


		private void CarsItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (e.OriginalSource is FrameworkElement fe && fe.DataContext is CarViewModel selectedCar)
			{
				// Открываем новое окно с подробной информацией об автомобиле
				var carDetailsWindow = new CarDetailsWindow(selectedCar);
				carDetailsWindow.Show();
			}
		}

		private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
		{
			var border = sender as Border;
			if (border != null)
			{
				border.Background = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230)); // Цвет при наведении
			}
		}

		private void ListViewItem_MouseLeave(object sender, MouseEventArgs e)
		{
			var border = sender as Border;
			if (border != null)
			{
				border.Background = Brushes.White; // Цвет по умолчанию
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
