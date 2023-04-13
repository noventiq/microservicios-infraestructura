using MediatR;
using user.backend.domain.Users.Domain;
using user.backend.domain.Users.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.backend.application.Users.Commands.Create
{
    public class CreateUserCommand: IRequest<User>
    {
        public string AboutMe { get; set; }
        public int Age { get; set; }
        public string DisplayName { get; set; }
        public int DownVotes { get; set; }
        public DateTime LastAccessDate { get; set; }
        public string Location { get; set; }
        public string WebsiteUrl { get; set; }
        public int AccountId { get; set; }
    }
}
