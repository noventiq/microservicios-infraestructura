using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user.backend.domain.Users.DTO
{
    public class ResponseUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string _id { get; set; }
        public int id {get; set; }
        public string AboutMe {get; set; }
        public int? Age {get; set; }
        public DateTime CreationDate    {get; set; }
        public string DisplayName {get; set; }
        public int DownVotes   {get; set; }
        public string? EmailHash   {get; set; }
        public DateTime LastAccessDate  {get; set; }
        public string? Location    {get; set; }
        public int Reputation  {get; set; }
        public int UpVotes {get; set; }
        public int Views   {get; set; }
        public string? WebsiteUrl  {get; set; }
        public int AccountId { get; set; }
        public int profile_id { get; set; }
        public string profile_name { get; set; }
    }
}
