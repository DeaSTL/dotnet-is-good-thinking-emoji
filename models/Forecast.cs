using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models {
  class Forecast {
    public ObjectId Id { get; set; }
    public required string Description { get; set; }
    public required int Temp {get; set;}
    public required int Windspeed {get; set;}
  }
}
