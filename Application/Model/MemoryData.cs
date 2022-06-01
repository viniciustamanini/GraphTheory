using Application.Entities;

namespace Application.Model
{
    public class MemoryData
    {
        public int GraphIterator { get; set; } = 0;
        public int VertexIterator { get; set; } = 0;
        public int EdgeIterator { get; set; } = 0;
        public List<Graph> GraphList { get; set; } = new List<Graph>();
    }
}
