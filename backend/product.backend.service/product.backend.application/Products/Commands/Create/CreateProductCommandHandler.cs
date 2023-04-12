using MediatR;
using Microsoft.Extensions.Logging;
using product.backend.application.Products.Queries;
using product.backend.domain.Products.Domain;
using product.backend.domain.Products.DTO;
using product.backend.domain.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product.backend.application.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepositoryCommand _productRepository;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IProductRepositoryCommand productRepository, ILogger<CreateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            DateTime dateNow = DateTime.UtcNow;
            Product product = new Product();
            //product.id = command.id ; 
            product.title = command.title;
            product.description = command.description;
            product.price = command.price;
            product.discountPercentage = command.discountPercentage;
            product.rating = command.rating;
            product.stock = command.stock;
            product.brand = command.brand;
            product.category = command.category;
            product.createdAt = dateNow;
            product.createdBy = "sa";
            product.updatedAt = dateNow;
            product.updatedBy = "sa";

            return await this._productRepository.create(product);
        }
    }
}
