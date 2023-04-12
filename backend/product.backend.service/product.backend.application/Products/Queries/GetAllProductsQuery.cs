using MediatR;
using product.backend.domain.Products.Domain;
using product.backend.domain.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product.backend.application.Products.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ResponseProduct>>
    {
        public int Id { get; set; }
        public string MyProperty { get; set; }
    }
}
