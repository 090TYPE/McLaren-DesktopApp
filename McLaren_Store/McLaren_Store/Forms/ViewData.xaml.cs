using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using McLaren_Store.Assets;
using McLaren_Store.DataBase;

namespace McLaren_Store.Forms
{

	public partial class ViewData : Window
	{
		public ObservableCollection<Cars> CarsCollection { get; set; }
		public ObservableCollection<Customers> CustomersCollection { get; set; }
		public ObservableCollection<Sales> SalesCollection { get; set; }
		public ObservableCollection<Employees> EmployeesCollection { get; set; }
		public ObservableCollection<Roles> RolesCollection { get; set; }

		public ViewData()
		{
			
			InitializeComponent();
			LoadAllDataAsync();
			DataContext = this;
		}

		private async Task LoadAllDataAsync()
		{
			try
			{
				CarsCollection = new ObservableCollection<Cars>(await DBHelper.Instance.GetAllCarsDBAsync());
				CustomersCollection = new ObservableCollection<Customers>(await DBHelper.Instance.GetAllCustomersAsync());
				SalesCollection = new ObservableCollection<Sales>(await DBHelper.Instance.GetAllSalesAsync());
				EmployeesCollection = new ObservableCollection<Employees>(await DBHelper.Instance.GetAllEmployeesAsync());
				RolesCollection = new ObservableCollection<Roles>(await DBHelper.Instance.GetAllRolesAsync());

				CarsDataGrid.ItemsSource = CarsCollection;
				CustomersDataGrid.ItemsSource = CustomersCollection;
				SalesDataGrid.ItemsSource = SalesCollection;
				EmployeesDataGrid.ItemsSource = EmployeesCollection;
				RolesDataGrid.ItemsSource = RolesCollection;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
			}
		}
		private async void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
		{
			if (e.EditAction == DataGridEditAction.Commit)
			{
				try
				{
					await DBHelper.Instance.SaveChangesAsync();
					MessageBox.Show("Изменения успешно сохранены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
		private async void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			try
			{
				await DBHelper.Instance.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private async void SaveChanges_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				await DBHelper.Instance.SaveChangesAsync();
				MessageBox.Show("Изменения успешно сохранены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка сохранения изменений: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
