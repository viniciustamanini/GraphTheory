namespace Application.Entities
{
    public class Graph
    {
        public Graph(int id, bool directed)
        {
            Id = id;
            Directed = directed;
        }

        public int Id { get; set; }
        public bool Directed { get; set; }
        public virtual List<Vertex> Vertices { get; set; } = new List<Vertex>();
    }
}
