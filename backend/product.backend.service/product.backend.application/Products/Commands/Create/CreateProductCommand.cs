using MediatR;
using product.backend.domain.Products.Domain;
using product.backend.domain.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product.backend.application.Products.Commands.Create
{
    public class CreateProductCommand: IRequest<Product>
    {
        public string title { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal discountPercentage { get; set; }
        public decimal rating { get; set; }
        public int stock { get; set; }
        public string brand { get; set; }
        public string category { get; set; }
    }
}
