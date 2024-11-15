using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using McLaren_Store.Assets;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace McLaren_Store.Forms
{
	public partial class Report : Window
	{
		public Report()
		{
			InitializeComponent();
		}

		// Генерация отчёта о купленных машинах
		private async void OnCarsPurchasedReportClick(object sender, RoutedEventArgs e)
		{
			var data = await DBHelper.Instance.GetDailySalesStatisticsAsync();

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Купленные машины");
				worksheet.Cell(1, 1).Value = "Дата";
				worksheet.Cell(1, 2).Value = "Количество продаж";

				int row = 2;
				foreach (var item in data)
				{
					worksheet.Cell(row, 1).Value = item.Date.ToString("dd.MM.yyyy");
					worksheet.Cell(row, 2).Value = item.SalesCount;
					row++;
				}

				SaveAndOpenWorkbook(workbook, "CarsPurchasedReport.xlsx");
			}
		}




		// Генерация полного отчёта о продажах
		private async void OnSalesReportClick(object sender, RoutedEventArgs e)
		{
			var salesData = await DBHelper.Instance.GetAllSalesDataAsync(); // Используем правильный метод

			using (var workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Все продажи");
				worksheet.Cell(1, 1).Value = "Дата";
				worksheet.Cell(1, 2).Value = "Модель";
				worksheet.Cell(1, 3).Value = "Цена";

				int row = 2;
				foreach (var item in salesData)
				{
					worksheet.Cell(row, 1).Value = item.SaleDate?.ToString("dd.MM.yyyy") ?? "N/A"; // Учитываем возможное null-значение
					worksheet.Cell(row, 2).Value = item.Model;
					worksheet.Cell(row, 3).Value = item.Price?.ToString("C") ?? "N/A";
					row++;
				}

				SaveAndOpenWorkbook(workbook, "AllSalesReport.xlsx");
			}
		}


		// Метод для сохранения и открытия Excel-файла
		private void SaveAndOpenWorkbook(XLWorkbook workbook, string fileName)
		{
			var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
			workbook.SaveAs(filePath);
			MessageBox.Show($"Отчёт сохранён на рабочем столе: {filePath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
			System.Diagnostics.Process.Start(filePath);
		}
	}
}
