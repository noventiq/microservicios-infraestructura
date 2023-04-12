using product.backend.domain.Products.Domain;
using product.backend.domain.Products.Interfaces;
using product.backend.shared;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using product.backend.domain.Common.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using product.backend.domain.Products.DTO;

namespace product.backend.infraestructure.Products
{
    public class ProductRepositoryQuery : IProductRepositoryQuery
    {
        //private readonly ISqlServerConnection _connection;
        //public ProductRepositoryQuery(ISqlServerConnection connection)
        //{
        //    _connection = connection;
        //}

        //public async Task<IEnumerable<Product>> List()
        //{
        //    IEnumerable<Product> lista;

        //    using (var scope = await _connection.BeginConnection())
        //    {
        //        try
        //        {
        //            lista = await scope.QueryAsync<Product>("SELECT * FROM DimProduct", commandType: CommandType.Text);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new CustomException("Error al listar productos", ex);
        //        }
        //    }

        //    return lista;
        //}

        private MongoClient _client;
        private IMongoDatabase _database;
        public ProductRepositoryQuery(IConfiguration config)
        {
            var mongoUrl = new MongoUrl(config.GetValue<string>("MongoDatabaseSettings:ConnectionString"));

            _client = new MongoClient(mongoUrl);
            _database = _client.GetDatabase("products_microservice");
        }

        public async Task<IEnumerable<ResponseProduct>> List()
        {
            return (await _database.GetCollection<ResponseProduct>("products").FindAsync(prd => true)).ToEnumerable();
        }
    }
}
