using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product.backend.domain.Common.Interfaces
{
    public interface ISqlServerConnection
    {
        Task<IDbConnection> BeginConnection();
        Task CloseConnection();
    }
}
