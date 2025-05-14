using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace McLaren_Store.ViewModels
{
	public class CarViewModel
	{
		public int CarID { get; set; }
		public string Model { get; set; }
		public string Color { get; set; }
		public string EngineType { get; set; }
		public string Transmission { get; set; }
		public decimal? Price { get; set; }
		public bool? Available { get; set; }
		public string Brand { get; set; }

		// Property to get the image as an ImageSource
		public ImageSource CarImage
		{
			get
			{
				if (Image != null)
				{
					using (var ms = new MemoryStream(Image))
					{
						var bitmap = new BitmapImage();
						bitmap.BeginInit();
						bitmap.CacheOption = BitmapCacheOption.OnLoad;
						bitmap.StreamSource = ms;
						bitmap.EndInit();
						bitmap.Freeze();
						return bitmap;
					}
				}
				return null;
			}
		}

		public byte[] Image { get; set; } 
	}
}