﻿using Microsoft.Extensions.Configuration;
using product.backend.domain.Common.Interfaces;
using product.backend.shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace product.backend.infraestructure.Common
{
    public class SqlServerConnection : ISqlServerConnection
    {
        private string _connectionString;
        private readonly IConfiguration _config;
        private SqlConnection con;

        public SqlServerConnection(IConfiguration config)
        {
            this._config = config;
            _connectionString = config.GetValue<string>("SqlServerSettings:ConnectionString");
        }

        public async Task<IDbConnection> BeginConnection()
        {
            if (this.con == null)
                this.con = new SqlConnection(this._connectionString);

            if (this.con.State != System.Data.ConnectionState.Open)
            {
                if (string.IsNullOrEmpty(this.con.ConnectionString))
                    this.con.ConnectionString = this._connectionString;
                try
                {
                    await this.con.OpenAsync();
                }
                catch (Exception ex)
                {
                    throw new CustomException(string.Format("connectionString: {0}", this._connectionString), ex);
                }
            }
            return this.con;
        }

        public async Task CloseConnection()
        {
            await Task.Run(() =>
            {
                this.con.Close();
                this.con.Dispose();
            });
        }

    }
}
