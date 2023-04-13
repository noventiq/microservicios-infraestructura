using MediatR;
using Microsoft.Extensions.Logging;
using user.backend.application.Users.Queries;
using user.backend.domain.Users.Domain;
using user.backend.domain.Users.DTO;
using user.backend.domain.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.backend.application.Users.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepositoryCommand _userRepository;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IUserRepositoryCommand userRepository, ILogger<CreateProductCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            DateTime dateNow = DateTime.UtcNow;
            User user = new User();
            //product.id = command.id ; 
            //product.title = command.title;
            //product.description = command.description;
            //product.price = command.price;
            //product.discountPercentage = command.discountPercentage;
            //product.rating = command.rating;
            //product.stock = command.stock;
            //product.brand = command.brand;
            //product.category = command.category;
            //product.createdAt = dateNow;
            //product.createdBy = "sa";
            //product.updatedAt = dateNow;
            //product.updatedBy = "sa";

            return await this._userRepository.create(user);
        }
    }
}
