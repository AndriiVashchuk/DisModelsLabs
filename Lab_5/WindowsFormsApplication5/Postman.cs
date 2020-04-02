using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5;

namespace lab2
{
    class Postman
    {
        private Graph _graph;
        private Graph _workGraph;
        private int _weigth;
        private List<Vertex> _cycleVertex;
        private List<Edge> _cycleEdges;

        public int Weigth { get { return _weigth; } }
        public List<Vertex> CycleVertex { get { return _cycleVertex; } }
        public List<Edge> CycleEdges { get { return _cycleEdges; } }
        public Graph WorkGraph { get { return _workGraph; } }

        public Postman(Graph graph)
        {
            _graph = graph;
            _weigth = 0;
            startPosman();
        }

        private void startPosman()
        {
            //Перевіримо, чи граф має Ейлерів цикл
            EulerCircle euler = new EulerCircle(_graph, _graph.Vertices.ToArray()[0]);
            List<Vertex> noPartVertex = euler.getNoPartVertex();
            if (noPartVertex.Count == 0)
            {
                //граф містить ейлерів цикл
                _workGraph = _graph;
            }
            else
            {
                createWorkGraph(noPartVertex);
            }
            calculeteEulerWay();
        }

        private void calculeteEulerWay()
        {
            //рахуємо ціну шляху
            foreach(Edge e in _workGraph.Edges)
            {
                _weigth += e.Weight;
            }
            EulerCircle euler = new EulerCircle(
                _workGraph, _workGraph.Vertices.ToArray()[0]);
            Cycles cycles = euler.getEulerCycle();
            _cycleEdges = cycles.edgesCycle;
            _cycleVertex = cycles.vertexCycle;
        }

        /// <summary>
        /// створює новий граф, якщо в ньому тільки дві непарні вершини
        /// </summary>
        private void createWorkGraph(List<Vertex> noPartVertex)
        {
            Dijkstra dijkstra = new Dijkstra(_graph, noPartVertex[0]);
            List<Edge> newE = dijkstra.getWayEdge(noPartVertex[1]);
            //дублюємо необхідні ребра
            _workGraph =  _graph.clone();
            foreach(Edge e in newE)
            {
                _workGraph.AddEdge(new Edge(e.Source, e.Target, e.Weight, e.Name+"*"));
            }
        }
  
    }

   

    struct Cycles
    {
        public List<Edge> edgesCycle;
        public List<Vertex> vertexCycle;
    }
}
