using Dapper;
using product.backend.domain.Common.Interfaces;
using product.backend.domain.Products.Domain;
using product.backend.domain.Products.DTO;
using product.backend.domain.Products.Interfaces;
using product.backend.shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product.backend.infraestructure.Products
{
    public class ProductRepositoryCommand : IProductRepositoryCommand
    {

        private readonly ISqlServerConnection _connection;
        public ProductRepositoryCommand(ISqlServerConnection connection)
        {
            _connection = connection;
        }

        public async Task<Product> create(Product product)
        {
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    product.id = await scope.ExecuteScalarAsync<int>("select MAX(id) from products");
                    product.id++;

                    await scope.ExecuteAsync(@"
                            INSERT INTO products(
                            id,
                            title,
                            description,
                            price,
                            discountPercentage,
                            rating,
                            stock,
                            brand,
                            category,
                            createdAt,
                            createdBy,
                            updatedAt,
                            updatedBy
                            )
                            VALUES(
                            @id,
                            @title,
                            @description,
                            @price,
                            @discountPercentage,
                            @rating,
                            @stock,
                            @brand,
                            @category,
                            @createdAt,
                            @createdBy,
                            @updatedAt,
                            @updatedBy
                            )
                            ", product, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Error al guardar producto", ex);
                }
            }

            return product;
        }
    }
}
