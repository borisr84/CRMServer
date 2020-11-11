using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Persistence.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Name { get; set; }

        //[Required]
        [Column(TypeName = "nvarchar(200)")]
        public string? Description { get; set; }

        //[Required]
        public double Price { get; set; }

        //[Required]
        [Column(TypeName = "int")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? Location { get; set; }
    }
}
