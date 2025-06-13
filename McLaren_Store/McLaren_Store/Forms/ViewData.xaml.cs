using System;
using System.Collections.Generic;
using System.Data.Entity;  // Для .Load()
using System.Windows;
using System.Windows.Controls;
using McLaren_Store.DataBase;

namespace McLaren_Store.Forms
{
	public partial class ViewData : Window
	{
		private McLaren_StoreEntities3 _context = new McLaren_StoreEntities3();
		private string _currentTable = "";

		public ViewData()
		{
			InitializeComponent();
			TableSelector.ItemsSource = new List<string>
			{
				"Cars", "Customers", "Employees", "Sales", "ManagerOrders", "Roles"
			};
		}

		private void TableSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			_currentTable = TableSelector.SelectedItem?.ToString();
			LoadData();
		}

		private void LoadData()
		{
			switch (_currentTable)
			{
				case "Cars":
					_context.Cars.Load();
					DataGridView.ItemsSource = _context.Cars.Local;
					break;
				case "Customers":
					_context.Customers.Load();
					DataGridView.ItemsSource = _context.Customers.Local;
					break;
				case "Employees":
					_context.Employees.Load();
					DataGridView.ItemsSource = _context.Employees.Local;
					break;
				case "Sales":
					_context.Sales.Load();
					DataGridView.ItemsSource = _context.Sales.Local;
					break;
				case "ManagerOrders":
					_context.ManagerOrders.Load();
					DataGridView.ItemsSource = _context.ManagerOrders.Local;
					break;
				case "Roles":
					_context.Roles.Load();
					DataGridView.ItemsSource = _context.Roles.Local;
					break;
				default:
					DataGridView.ItemsSource = null;
					break;
			}
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				_context.SaveChanges();
				MessageBox.Show("Изменения успешно сохранены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void Refresh_Click(object sender, RoutedEventArgs e)
		{
			// Обновляем контекст — создаём новый
			_context.Dispose();
			_context = new McLaren_StoreEntities3();
			LoadData();
		}
	}
}
