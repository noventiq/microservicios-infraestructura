using MediatR;
using user.backend.domain.Users.Domain;
using user.backend.domain.Users.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.backend.application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<ResponseUser>>
    {
        public int Id { get; set; }
        public string MyProperty { get; set; }
    }
}
