using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McLaren_Store.Assets
{
	internal class ManagerOrderViewModel
	{
		public int OrderId { get; set; }
		public string ClientName { get; set; }
		public string CarModel { get; set; }
		public decimal? Price { get; set; }
		public DateTime? OrderDate { get; set; }
		public string Status { get; set; }
	}
}
