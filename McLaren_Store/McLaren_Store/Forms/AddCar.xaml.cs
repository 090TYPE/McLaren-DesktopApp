using McLaren_Store.Assets;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
namespace McLaren_Store.Forms
{
	/// <summary>
	/// Логика взаимодействия для AddCar.xaml
	/// </summary>
	public partial class AddCar : Window
	{
		private byte[] _carImage; // To store the image data

		public AddCar()
		{
			InitializeComponent();
		}

		private void LoadImage_Click(object sender, RoutedEventArgs e)
		{
			var openFileDialog = new OpenFileDialog
			{
				Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
				Title = "Выберите изображение для машины"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				// Load the image and display the file name
				_carImage = File.ReadAllBytes(openFileDialog.FileName);
				ImageFileNameTextBlock.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
			}
		}

		private async void AddCar_Click(object sender, RoutedEventArgs e)
		{
			// Validate inputs
			if (string.IsNullOrWhiteSpace(ModelTextBox.Text) ||
				string.IsNullOrWhiteSpace(YearTextBox.Text) ||
				string.IsNullOrWhiteSpace(ColorTextBox.Text) ||
				string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
				string.IsNullOrWhiteSpace(EngineTypeTextBox.Text) ||
				string.IsNullOrWhiteSpace(TransmissionTextBox.Text) ||
				string.IsNullOrWhiteSpace(BrandTextBox.Text) ||
				_carImage == null) // Ensure an image is selected
			{
				System.Windows.MessageBox.Show("Пожалуйста, заполните все поля и выберите изображение.");
				return;
			}

			var newCar = new McLaren_Store.DataBase.Cars
			{
				Model = ModelTextBox.Text,
				Year = int.TryParse(YearTextBox.Text, out int year) ? year : (int?)null,
				Color = ColorTextBox.Text,
				Price = decimal.TryParse(PriceTextBox.Text, out decimal price) ? price : (decimal?)null,
				EngineType = EngineTypeTextBox.Text,
				Transmission = TransmissionTextBox.Text,
				Brand = BrandTextBox.Text,
				Available = AvailableCheckBox.IsChecked,
				Image = _carImage
			};

			// Use DBHelper to add the car
			try
			{
				await DBHelper.Instance.AddCarAsync(newCar);
				MessageBox.Show("Машина добавлена успешно!");
				ClearFields();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при добавлении машины: {ex.Message}");
			}
		}

		private void ClearFields()
		{
			ModelTextBox.Clear();
			YearTextBox.Clear();
			ColorTextBox.Clear();
			PriceTextBox.Clear();
			EngineTypeTextBox.Clear();
			TransmissionTextBox.Clear();
			AvailableCheckBox.IsChecked = false;
			_carImage = null;
			ImageFileNameTextBlock.Text = string.Empty;
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
