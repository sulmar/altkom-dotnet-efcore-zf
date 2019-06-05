using System;

namespace Altkom.ZF.Models
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProductImage Image { get; set; }

        public decimal Price { get; set; }
    }

    public class ProductImage : BaseEntity
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public byte[] Photo { get; set; }
    }

}