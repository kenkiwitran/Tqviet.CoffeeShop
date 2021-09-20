using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tqviet.CoffeeShop.Models
{
    [Table("CoffeeOrders")]
    public class CoffeeOrders:IDisposable
    {
        private bool disposedValue;

        [Key]
        public int CoffeeOrderId { get; set; }
        [Required]
        public DateTime CoffeeOrderDateTime { get; set; }
        [Required]
        public string CoffeeOrderClientIp { get; set; }
        [Required]
        public int? CoffeeOrderQuantity { get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
