using System;

public struct Edge
{
    public int Src;
    public int Dest;
    public int Wt;
}

public struct Graph
{
    public int V;
    public int E;
    public Edge[] Edges;

    public static Graph Create(int v, int e)
    {
        return new Graph
        {
            V = v,
            E = e,
            Edges = new Edge[e]
        };
    }
}

public struct Subset
{
    public int Parent;
    public int Rank;
}

public class Kruskal
{
    private static int Find(Subset[] subs, int i)
    {
        if (subs[i].Parent != i)
            subs[i].Parent = Find(subs, subs[i].Parent);

        return subs[i].Parent;
    }

    private static void Union(Subset[] subs, int x, int y)
    {
        int xRoot = Find(subs, x);
        int yRoot = Find(subs, y);

        if (subs[xRoot].Rank < subs[yRoot].Rank)
            subs[xRoot].Parent = yRoot;

        else if (subs[xRoot].Rank > subs[yRoot].Rank)
            subs[yRoot].Parent = xRoot;

        else
        {
            subs[yRoot].Parent = xRoot;
            subs[xRoot].Rank++;
        }
    }

    private static void Print(Edge[] res, int e)
    {
        Console.WriteLine("Edges in the MST:");
        for (int i = 0; i < e; i++)
            Console.WriteLine($"From {res[i].Src} To {res[i].Dest}, Weight = {res[i].Wt}");

    }

    public static void Run(Graph g)
    {
        int v = g.V;
        Edge[] res = new Edge[v];
        int e = 0, i = 0;

        Array.Sort(g.Edges, (a, b) => a.Wt.CompareTo(b.Wt));

        Subset[] subs = new Subset[v];
        for (int j = 0; j < v; j++)
        {
            subs[j].Parent = j;
            subs[j].Rank = 0;
        }

        while (e < v - 1)
        {
            Edge next = g.Edges[i++];
            int x = Find(subs, next.Src);
            int y = Find(subs, next.Dest);

            if (x != y)
            {
                res[e++] = next;
                Union(subs, x, y);
            }
        }

        Print(res, e);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter number of vertices:");
        int v = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter number of edges:");
        int e = int.Parse(Console.ReadLine());

        Graph g = Graph.Create(v, e);

        Console.WriteLine("Enter edges (Src Dest Wt):");
        for (int i = 0; i < e; i++)
        {
            Console.WriteLine($"Edge {i + 1}:");
            string[] input = Console.ReadLine().Split();
            g.Edges[i] = new Edge
            {
                Src = int.Parse(input[0]),
                Dest = int.Parse(input[1]),
                Wt = int.Parse(input[2])
            };
        }

        Console.WriteLine("\nKruskal's Algorithm: ");
        Kruskal.Run(g);
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
