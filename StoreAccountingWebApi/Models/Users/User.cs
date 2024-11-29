using StoreAccountingWebApi.Models.Sales;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StoreAccountingWebApi.Models.Users
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string phoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        [JsonIgnore]
        public List<Sale> Sales { get; set; } = new List<Sale>();

    }

    public enum Role
    {
        client,
        admin
    }
}
