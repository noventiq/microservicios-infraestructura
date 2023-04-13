using user.backend.domain.Users.Domain;
using user.backend.domain.Users.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.backend.domain.Users.Interfaces
{
    public interface IUserRepositoryQuery
    {
        Task<IEnumerable<ResponseUser>> List();
    }
}
