using MongoDB.Bson;

namespace CourseWork.Models
{
    public abstract class Entity
    {
        public string Id { get; set; }
        public Entity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public Entity(string id)
        {
            Id = id;
        }
    }
}
