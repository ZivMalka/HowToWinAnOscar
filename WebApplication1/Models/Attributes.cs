using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class Attributes
    {
        //Mongodb id
        [BsonId] public ObjectId ID { get; set; }

        [BsonElement("year")] public int Year { get; set; }
        [BsonElement("category")] public string Category { get; set; }
        [BsonElement("film")] public string Film { get; set; }
        [BsonElement("name")] public string Name { get; set; }
        [BsonElement("BAFTA")] public int BAFTA { get; set; }
        [BsonElement("Golden Globe")] public int GoldenGlobe { get; set; }
        [BsonElement("Guild")] public int Guild { get; set; }
        [BsonElement("Oscar")] public int Oscar { get; set; }
        [BsonElement("running_time")] public int Running_time { get; set; }
        [BsonElement("box_office")] public double Box_office { get; set; }
        [BsonElement("imdb_score")] public double Imdb_score { get; set; }
        [BsonElement("rt_audience_score")] public int Rt_audience_score { get; set; }
        [BsonElement("rt_critic_score")] public int Rt_critic_score { get; set; }
        [BsonElement("stars_count")] public int Stars_count { get; set; }
        [BsonElement("writers_count")] public int Writers_count { get; set; }
        [BsonElement("produced_USA")] public int Produced_USA { get; set; }
        [BsonElement("R")] public int R { get; set; }
        [BsonElement("PG")] public int PG { get; set; }
        [BsonElement("PG13")] public int PG13 { get; set; }
        [BsonElement("G")] public int G { get; set; }
        [BsonElement("q1_release")] public int Q1_release { get; set; }
        [BsonElement("q2_release")] public int Q2_release { get; set; }
        [BsonElement("q3_release")] public int Q3_release { get; set; }
        [BsonElement("q4_release")] public int Q4_release { get; set; }

    }
}