using product.backend.domain.Products.Domain;
using product.backend.domain.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product.backend.domain.Products.Interfaces
{
    public interface IProductRepositoryCommand
    {
        Task<Product> create(Product product);
    }
}
