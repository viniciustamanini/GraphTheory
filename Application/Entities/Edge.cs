namespace Application.Entities
{
    public class Edge
    {
        public Edge(int id, float weight, Vertex origin, Vertex destiny)
        {
            Id = id;
            Weight = weight;
            Origin = origin;
            Destiny = destiny;
        }

        public int Id { get; set; }
        public float Weight { get; set; } = 0;
        public virtual Vertex Origin { get; set; }
        public virtual Vertex Destiny { get; set; }
    }
}
