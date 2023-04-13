using user.backend.domain.Users.Domain;
using user.backend.domain.Users.Interfaces;
using user.backend.shared;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user.backend.domain.Common.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using user.backend.domain.Users.DTO;
using System.Data.SqlClient;

namespace user.backend.infraestructure.Users
{
    public class UserRepositoryQuery : IUserRepositoryQuery
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private readonly ISqlServerConnection _connection;

        public UserRepositoryQuery(IConfiguration config, ISqlServerConnection connection)
        {
            var mongoUrl = new MongoUrl(config.GetValue<string>("MongoDatabaseSettings:ConnectionString"));

            _client = new MongoClient(mongoUrl);
            _database = _client.GetDatabase("users_microservice");

            _connection = connection;
        }

        public async Task<IEnumerable<ResponseUser>> List()
        {
            //Timer timer = new Timer(1000);
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            IEnumerable<ResponseUser> usuarios = (await _database.GetCollection<ResponseUser>("users").FindAsync(prd => true)).ToEnumerable();
            watch.Stop();

            await Console.Out.WriteLineAsync($"Tiempo de ejecución mongoDb: {watch.ElapsedMilliseconds}");

 //           watch.Restart();
 //           IEnumerable<ResponseUser> usuarios2;
 //           using (IDbConnection con = await _connection.BeginConnection())
 //           {
 //               usuarios2 = await con.QueryAsync<ResponseUser>(@"select 
	//u.id,
	//u.AboutMe,
	//u.Age,
	//u.CreationDate,
	//u.DisplayName,
	//u.DownVotes,
	//u.LastAccessDate,
	//u.Location,
	//u.Reputation,
	//u.UpVotes,
	//u.Views,
	//u.WebsiteUrl,
	//p.id as profile_id, 
	//p.name as profile_name
	//from users u 
	//inner join users_profiles up on u.id=up.user_id
	//inner join profiles p on up.profile_id=p.id");
 //           }
 //           watch.Stop();

 //           await Console.Out.WriteLineAsync($"Tiempo de ejecución SqlServer: {watch.ElapsedMilliseconds}");

            return usuarios;
        }
    }
}
