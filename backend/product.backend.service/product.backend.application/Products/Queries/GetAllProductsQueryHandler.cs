using MediatR;
using Microsoft.Extensions.Logging;
using product.backend.domain.Products.Domain;
using product.backend.domain.Products.DTO;
using product.backend.domain.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product.backend.application.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ResponseProduct>>
    {
        private readonly IProductRepositoryQuery _productRepository;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;

        public GetAllProductsQueryHandler(IProductRepositoryQuery productRepository, ILogger<GetAllProductsQueryHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ResponseProduct>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.List();
        }
    }
}
