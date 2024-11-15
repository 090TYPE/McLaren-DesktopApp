using LiveCharts;
using LiveCharts.Wpf;
using McLaren_Store.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace McLaren_Store.Forms
{
	public partial class AdminPanel : Window
	{
		public SeriesCollection SalesSeries { get; set; }
		public List<string> Dates { get; set; }
		public Func<double, string> Formatter { get; set; }
		private AddCar _addcarWindow;
		private Report _reportWindow;
		private ViewData _viewdataWindow;

		public AdminPanel()
		{
			InitializeComponent();
			LoadSalesData();
			
		}

		private async void LoadSalesData()
		{
			var salesData = await DBHelper.Instance.GetDailySalesStatisticsAsync();

			if (salesData == null || !salesData.Any())
			{
				MessageBox.Show("Нет данных для отображения.");
				return;
			}

			Dates = salesData.Select(data => data.Date.ToString("dd.MM")).ToList();
			SalesSeries = new SeriesCollection
			{
				new LineSeries
				{
					Title = "Продажи",
					Values = new ChartValues<int>(salesData.Select(data => data.SalesCount))
				}
			};

			Formatter = value => value.ToString("N");

			// Обновляем DataContext и уведомляем об изменениях
			DataContext = null;
			DataContext = this;
		}


		public void AddCar_Click(object sender, EventArgs e)
		{
			if (_addcarWindow == null || !_addcarWindow.IsVisible)
			{
				_addcarWindow = new AddCar();
				_addcarWindow.Closed += (s, args) => _addcarWindow = null;
				_addcarWindow.Show();
			}
			else
			{
				_addcarWindow.Activate();
			}
		}

		public void Report_Click(object sender, EventArgs e)
		{
			if (_reportWindow == null || !_reportWindow.IsVisible)
			{
				_reportWindow = new Report();
				_reportWindow.Closed += (s, args) => _reportWindow = null;
				_reportWindow.Show();
			}
			else
			{
				_reportWindow.Activate();
			}
		}

		public void Database_Click(object sender, EventArgs e)
		{
			if (_viewdataWindow == null || !_viewdataWindow.IsVisible)
			{
				_viewdataWindow = new ViewData();
				_viewdataWindow.Closed += (s, args) => _viewdataWindow = null;
				_viewdataWindow.Show();
			}
			else
			{
				_viewdataWindow.Activate();
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
