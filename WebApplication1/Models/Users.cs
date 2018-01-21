using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Users
    {

        //Mongodb id
        [BsonId] public ObjectId ID { get; set; }

        [BsonElement("UserName")] public string UserName { get; set; }
        [BsonElement("Password")] public string Password { get; set; }
        [BsonElement("Email")] public String Email { get; set; }
        [BsonElement("PhoneNo")] public int PhoneNo { get; set; }
        [BsonElement("Address")] public String Address { get; set; }
    }
}