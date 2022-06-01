namespace Application.Entities
{
    public class Vertex
    {
        public Vertex(int id, string identifier)
        {
            Id = id;
            Identifier = identifier;
        }

        public int Id { get; set; }
        public string Identifier { get; set; }
        public virtual Degree Degree { get; set; } = new Degree();
        public virtual List<Edge> Adjacencies { get; set; } = new List<Edge>();

    }
}
