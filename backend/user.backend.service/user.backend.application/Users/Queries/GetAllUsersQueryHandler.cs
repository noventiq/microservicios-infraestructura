using MediatR;
using Microsoft.Extensions.Logging;
using user.backend.domain.Users.Domain;
using user.backend.domain.Users.DTO;
using user.backend.domain.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.backend.application.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<ResponseUser>>
    {
        private readonly IUserRepositoryQuery _userRepository;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(IUserRepositoryQuery userRepository, ILogger<GetAllUsersQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ResponseUser>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.List();
        }
    }
}
