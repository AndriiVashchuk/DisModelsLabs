using System;
using System.Collections.Generic;

class Graph
{
	private List<int>[] adj;

    public int Vertices { get; }

    Graph(int numOfVertices)
	{
		Vertices = numOfVertices;
		InitGraph();
	}

	private void InitGraph()
	{
		adj = new List<int>[Vertices];
		for (int i = 0; i < Vertices; i++)
		{
			adj[i] = new List<int>();
		}
	}

	private void AddEdge(int u, int v)
	{
		adj[u].Add(v);
		adj[v].Add(u);
	}

	private void RemoveEdge(int u, int v)
	{
		adj[u].Remove(v);
		adj[v].Remove(u);
	}

	private void printEulerTour()
	{
		int u = 0;
		for (int i = 0; i < Vertices; i++)
		{
			if (adj[i].Count % 2 == 1)
			{
				u = i;
				break;
			}
		}

		PrintEulerUtil(u: u);
		Console.WriteLine();
	}

	private void PrintEulerUtil(int u)
	{
		for (int i = 0; i < adj[u].Count; i++)
		{
			int v = adj[u][i];

			if (IsValidNextEdge(u, v))
			{
				Console.Write(u + "-" + v + " ");

				RemoveEdge(u, v);
				PrintEulerUtil(v);
			}
		}
	}

	private bool IsValidNextEdge(int u, int v)
	{
		if (adj[u].Count == 1)
		{
			return true;
		}

		var isVisited = new bool[Vertices];
		var count1 = DfsCount(u, isVisited);

		RemoveEdge(u, v);
		isVisited = new bool[this.Vertices];
		int count2 = DfsCount(u, isVisited);

		AddEdge(u, v);
		return count1 <= count2;
	}

	private int DfsCount(int v, IList<bool> isVisited)
	{
		isVisited[v] = true;
		var count = 1;

        for (var index = 0; index < adj[v].Count; index++)
        {
            var i = adj[v][index];
            if (!isVisited[i])
            {
                count += DfsCount(i, isVisited);
            }
        }

        return count;
	}

	public static void Main(String[] a)
	{
		Graph g1 = new Graph(5);
		g1.AddEdge(0, 1);
		g1.AddEdge(1, 2);
		g1.AddEdge(2, 3);
        g1.printEulerTour();

		Graph g3 = new Graph(5);
		g3.AddEdge(1, 0);
		g3.AddEdge(0, 2);
		g3.AddEdge(2, 1);
		g3.AddEdge(0, 3);
		g3.AddEdge(3, 4);
		g3.AddEdge(3, 2);
		g3.AddEdge(3, 1);
		g3.AddEdge(2, 4);
		g3.printEulerTour();

		Console.ReadKey();
	}
}

