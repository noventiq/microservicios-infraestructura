using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using product.backend.application.Products.Commands.Create;
using product.backend.application.Products.Queries;
using product.backend.domain.Products.Domain;
using product.backend.domain.Products.DTO;
using product.backend.domain.Products.Interfaces;
using product.backend.shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace product.backend.application.Products
{
    public class ProductApp : BaseApp<ProductApp>
    {
        private readonly IProductRepositoryQuery _productRepository;
        private readonly IProductRepositoryCommand _productRepositoryCommand;
        private readonly IMediator _mediator;
        public ProductApp(
            IProductRepositoryQuery productRepository,
            IProductRepositoryCommand productRepositoryCommand,
            IMediator mediator,
            ILogger<BaseApp<ProductApp>> logger) : base(logger)
        {
            _mediator = mediator;
            _productRepository = productRepository;
            _productRepositoryCommand = productRepositoryCommand;
        }

        public async Task<StatusResponse<IEnumerable<ResponseProduct>>> List()
        {
            GetAllProductsQuery query = new GetAllProductsQuery();
            IEnumerable<ResponseProduct> response = await _mediator.Send(query);
            return new StatusResponse<IEnumerable<ResponseProduct>>(true, "") { Data = response };
            //return await this.complexProcess(() => _productRepository.List(), "");
            //StatusResponse<IEnumerable<Product>> status = await this.complexProcess(() => _productRepository.List(), "");
            //return status;
            //IEnumerable<Product> lista = null;
            //try
            //{
            //    lista = await _productRepository.List();
            //}
            //catch (Exception ex)
            //{
            //    this._logger.LogError(ex, "");
            //    throw;
            //}

            //return new StatusResponse<IEnumerable<Product>>(true, "") { Data = lista };
        }

        public async Task<StatusResponse<Product>> Create(CreateProductCommand command)
        {
            return await this.complexProcess(()=> _mediator.Send(command), "");
        }
    }
}
