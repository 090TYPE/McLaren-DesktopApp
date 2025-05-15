using McLaren_Store.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McLaren_Store.Assets
{
	public class ManagerOrder
	{
		public int Id { get; set; }

		public int OrderId { get; set; } // ← Это должно быть!

		public int ManagerId { get; set; }

		public string Status { get; set; }

		public virtual Sales Sale { get; set; }     // Навигационное свойство
		public virtual Employees Employee { get; set; } // опционально
	}
}
