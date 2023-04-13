using Dapper;
using user.backend.domain.Common.Interfaces;
using user.backend.domain.Users.Domain;
using user.backend.domain.Users.DTO;
using user.backend.domain.Users.Interfaces;
using user.backend.shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.backend.infraestructure.Users
{
    public class UserRepositoryCommand : IUserRepositoryCommand
    {

        private readonly ISqlServerConnection _connection;
        public UserRepositoryCommand(ISqlServerConnection connection)
        {
            _connection = connection;
        }

        public async Task<User> create(User user)
        {
            using (var scope = await _connection.BeginConnection())
            {
                try
                {
                    //product.id = await scope.ExecuteScalarAsync<int>("select MAX(id) from products");
                    //product.id++;

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
                            ", user, commandType: CommandType.Text);
                }
                catch (Exception ex)
                {
                    throw new CustomException("Error al guardar usuario", ex);
                }
            }

            return user;
        }
    }
}
