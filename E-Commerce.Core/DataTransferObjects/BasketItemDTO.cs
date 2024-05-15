using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DataTransferObjects
{
    public class BasketItemDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]

        public string ProductName { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]
       
        public int Quantity { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]

        public string TybeName { get; set; }
        [Required]

        public string PrandName { get; set; }
    }
}
