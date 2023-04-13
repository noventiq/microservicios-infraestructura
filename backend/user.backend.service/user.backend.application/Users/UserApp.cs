using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using user.backend.application.Users.Commands.Create;
using user.backend.application.Users.Queries;
using user.backend.domain.Users.Domain;
using user.backend.domain.Users.DTO;
using user.backend.domain.Users.Interfaces;
using user.backend.shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace user.backend.application.Users
{
    public class UserApp : BaseApp<UserApp>
    {
        private readonly IUserRepositoryQuery _userRepository;
        private readonly IUserRepositoryCommand _userRepositoryCommand;
        private readonly IMediator _mediator;
        public UserApp(
            IUserRepositoryQuery productRepository,
            IUserRepositoryCommand productRepositoryCommand,
            IMediator mediator,
            ILogger<BaseApp<UserApp>> logger) : base(logger)
        {
            _mediator = mediator;
            _userRepository = productRepository;
            _userRepositoryCommand = productRepositoryCommand;
        }

        public async Task<StatusResponse<IEnumerable<ResponseUser>>> List()
        {
            GetAllUsersQuery query = new GetAllUsersQuery();
            IEnumerable<ResponseUser> response = await _mediator.Send(query);
            return new StatusResponse<IEnumerable<ResponseUser>>(true, "") { Data = response };
        }

        public async Task<StatusResponse<User>> Create(CreateUserCommand command)
        {
            return await this.complexProcess(()=> _mediator.Send(command), "");
        }
    }
}
