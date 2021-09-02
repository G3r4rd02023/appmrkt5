using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appmrkt5.Data.Entities
{
    public class Order:IEntity
    {
		public int Id { get; set; }

		[Required(ErrorMessage = "El campo {0} es obligatorio.")]
		[Display(Name = "Fecha")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
		public DateTime OrderDate { get; set; }

		[Display(Name = "Fecha de Entrega")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
		public DateTime? DeliveryDate { get; set; }

		[Required]
		[Display(Name = "Usuario")]
		public User User { get; set; }

		public IEnumerable<OrderDetail> Items { get; set; }

		[Display(Name = "Líneas")]
		[DisplayFormat(DataFormatString = "{0:N0}")]
		public int Lines { get { return this.Items == null ? 0 : this.Items.Count(); } }

		[Display(Name = "Cantidad")]
		[DisplayFormat(DataFormatString = "{0:N2}")]
		public double Quantity { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Quantity); } }

		[Display(Name = "Valor")]
		[DisplayFormat(DataFormatString = "{0:C2}")]
		public decimal Value { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Value); } }


	}
}
