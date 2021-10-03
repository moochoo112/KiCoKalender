using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Domain
{
    [OpenApiExample(typeof(DummyUserExample))]
    public class User
    {
        [OpenApiProperty(Description = "Gets or sets the ID.")]
        public long? userId { get; set; }

        [OpenApiProperty(Description = "Gets or sets the name.")]
        [JsonRequired]
        public string userFirstName { get; set; }

        [OpenApiProperty(Description = "Gets or sets the name.")]
        [JsonRequired]
        public string userLastName { get; set; }

        [OpenApiProperty(Description = "Gets or sets the age.")]
        [JsonRequired]
        public int userAge { get; set; }

        [OpenApiProperty(Description = "Gets or sets the Role for the user.")]
        [JsonRequired]
        public Role userRole { get; set; }

        [OpenApiProperty(Description = "Gets or sets the adres.")]
        public string userAddress { get; set; }

        [OpenApiProperty(Description = "Gets or sets the postcode.")]
        public string userZipcode { get; set; }

        [OpenApiProperty(Description = "Gets or sets created date.")]
        [JsonRequired]
        public DateTime userCreated { get; set; }

        public User()
        {

        }

        public User(long? userId, string userFirstName, string userLastName, int userAge, Role userRole)
        {
            this.userId = userId;
            this.userFirstName = userFirstName;
            this.userLastName = userLastName;
            this.userAge = userAge;
            this.userRole = userRole;

            //PartitionKey = userId;
            //RowKey = userLastName + userFirstName;
        }
    }

    public class DummyUserExample : OpenApiExample<User>
    {
        public override IOpenApiExample<User> Build(NamingStrategy NamingStrategy = null)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Dirk", "This is Dirk's summary", new User() {userFirstName = "Dirk", userLastName = "Dirksma",userRole = Role.Parent, userAge = 20, userAddress = "street", userZipcode = "1234AB", userCreated = DateTime.Now }, NamingStrategy));

            return this;
        }
    }
}
