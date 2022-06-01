using Application.Entities;
using Application.Model;

namespace Application
{
    public class App
    {
        public App()
        {

        }

        public void Execute()
        {
            int option = 0;
            MemoryData memoryData = new();
            ShowMenu();
            while(option != 9)
            {
                option = InputValidate();
                ExecuteOption(option, memoryData);
            }

        }

        private void ExecuteOption(int option, MemoryData memoryData)
        {
            switch (option)
            {
                case 1:
                    AddGraph(memoryData);
                    break;
                case 2:
                    AddVertex(memoryData);
                    break;
                case 3:
                    AddEdge(memoryData);
                    break;
                case 4:
                    PrintVertexByGraph(memoryData);
                    break;
                case 9:
                    Console.WriteLine("- Saindo...");
                    break;
            }
        }

        private static void AddGraph(MemoryData memoryData)
        {
            memoryData.GraphIterator++;
            Console.WriteLine("- O grafo é direcionado? 0:Não, 1:Sim");
            bool directed = InputValidate() == 1;
            Graph graph = new(memoryData.GraphIterator, directed);
            Console.WriteLine("- Novo grafo cadastrado: " + PrintGraphInfos(graph));
            memoryData.GraphList.Add(graph);
        }

        private static string PrintGraphInfos(Graph graph)
        {
            return $"Id: {graph.Id}, {(graph.Directed ? "Direcionado" : "Não direcionado")}, Vertices: {graph.Vertices.Count}";
        }

        private static bool PrintGraphs(MemoryData memoryData)
        {
            if (!memoryData.GraphList.Any())
            {
                Console.WriteLine("- Erro: É necessário cadastrar grafos antes de usar essa opção.");
                return false;
            }

            Console.WriteLine("--- LISTA DE GRAFOS ---");
            memoryData.GraphList.ForEach(x => Console.WriteLine(PrintGraphInfos(x)));
            return true;
        }

        private static bool GraphIsNull(Graph graph)
        {
            if (graph is null)
            {
                Console.WriteLine("- Erro: Grafo não encontrado.");
                return true;
            }

            return false;
        }

        private static void PrintVertexByGraph(MemoryData memoryData)
        {
            if (!PrintGraphs(memoryData))
                return;

            Console.WriteLine("- Listar vértices de qual grafo?");
            int idGraph = InputValidate();
            Graph graph = memoryData.GraphList.Find(x => x.Id == idGraph);
            if (GraphIsNull(graph))
                return;

            PrintVertices(graph);
        }

        private static bool PrintVertices(Graph graph)
        {
            if (!graph.Vertices.Any())
            {
                Console.WriteLine("- Erro: É necessário cadastrar vértices antes de usar essa opção.");
                return false;
            }

            Console.WriteLine($"--- LISTA DE VÉRTICES - GRAFO {graph.Id} ---");
            foreach (Vertex vertex in graph.Vertices)
            {
                Console.WriteLine($"Id: {vertex.Id}, Identificador: {vertex.Identifier}, Grau: E:{vertex.Degree.In} S:{vertex.Degree.Out}");
                if (vertex.Adjacencies == null)
                    continue;

                foreach (Edge edge in vertex.Adjacencies)
                    Console.WriteLine("    " + PrintEdgeInfos(edge));
            }
            return true;
        }

        private static void AddVertex(MemoryData memoryData)
        {
            if (!PrintGraphs(memoryData))
                return;

            Console.WriteLine("- Cadastrar vértices para qual dos grafos?");
            int idGraph = InputValidate();
            Graph graph = memoryData.GraphList.Find(x => x.Id == idGraph);
            if (GraphIsNull(graph))
                return;

            memoryData.VertexIterator++;
            Console.WriteLine("- Adicione um identificador para o vértice: ");
            string identifier = Console.ReadLine();
            identifier = string.IsNullOrEmpty(identifier) ? $"V{(graph.Vertices.Count) + 1}" : identifier;

            Vertex vertex = new(memoryData.VertexIterator, identifier);
            graph.Vertices.Add(vertex);
            Console.WriteLine("- Grafo alterado: " + PrintGraphInfos(graph));
        }

        private static Vertex GetVertex(Graph graph, int id)
        {
            Vertex vertex = graph.Vertices.FirstOrDefault(x => x.Id == id);
            while (vertex == null)
            {
                Console.WriteLine($"- Erro: Vértice de id {id} não encontrado para este grafo.");
                Console.WriteLine("- Escolha outra vértice:");
                id = InputValidate();
                vertex = graph.Vertices.FirstOrDefault(x => x.Id == id);
            }
            return vertex;
        }

        private static void AddEdge(MemoryData memoryData)
        {
            if (!PrintGraphs(memoryData))
                return;

            Console.WriteLine("- Cadastrar arestas para vértice de qual dos grafos?");
            int idGraph = InputValidate();
            Graph graph = memoryData.GraphList.Find(x => x.Id == idGraph);
            if (GraphIsNull(graph))
                return;

            if(!PrintVertices(graph))
                return;

            Console.WriteLine("- Cadastrar destino para qual vértice?");
            int idOrigimVertex = InputValidate();
            Vertex origimVertex = GetVertex(graph, idOrigimVertex);
            Console.WriteLine("- Qual o destino?");
            int idDestinyVertex = InputValidate();
            Vertex destinyVertex = GetVertex(graph, idDestinyVertex);

            Console.WriteLine("- Qual o peso da aresta?");
            float weight;
            while (!float.TryParse(Console.ReadLine(), out weight))
            {
                Console.WriteLine($"\nInsira apenas valores numéricos\n");
            }

            memoryData.EdgeIterator++;
            Edge edge = new(memoryData.EdgeIterator, weight, origimVertex, destinyVertex);
            if (graph.Directed)
            {
                origimVertex.Adjacencies.Add(edge);
                origimVertex.Degree.Out++;
                destinyVertex.Degree.In++;
            }
            else
            {
                origimVertex.Adjacencies.Add(edge);
                destinyVertex.Adjacencies.Add(edge);
                origimVertex.Degree.In++;
                origimVertex.Degree.Out++;
                destinyVertex.Degree.In++;
                destinyVertex.Degree.Out++;
            }
            Console.WriteLine("- Aresta cadastrada: " + PrintEdgeInfos(edge));
        }

        private static string PrintEdgeInfos(Edge edge)
        {
            return $"Id: {edge.Id}, Origem: {edge.Origin.Identifier} - Destino: {edge.Destiny.Identifier}, Peso: {edge.Weight}.";
        }

        private static void ShowMenu()
        {
            Console.WriteLine($@"Selecione um dos itens do menu:
            1 – Cadastrar grafos não direcionados, ou dígrafos.
            2 - Cadastrar os vértices de um determinado grafo.
            3 - Cadastrar as arestas informando os vértices de origem e de destino.
            4 - Listar os vértices informados e as arestas.
            9 - Sair.");
        }

        private static int InputValidate()
        {
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine($"\nInsira apenas números inteiros\n");
            }
            return value;
        }
    }
}