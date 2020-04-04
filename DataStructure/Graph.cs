using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {
        private List<Node> nodes;
        public Graph()
        {
            this.nodes = new List<Node>();
        }
        public List<Node> ShortestPath(string begin, string end)
        {
            List<Node> resp = new List<Node>();

            return null;
        }

        public List<Node> BreadthFirstSearch(string begin)
        {
            Clear_Visited(this.nodes);
            List<Node> r = new List<Node>();
            Node ini = Find(begin);
            Queue<Node> lis = new Queue<Node>();
            ini.Visited = true;
            lis.Enqueue(ini);
            Node x;//= lis.Dequeue();
            //int i = 0;
            while (lis.Count!=0)
            {
                x = lis.Dequeue();
                r.Add(x);
                ///Console.WriteLine((i++).ToString() + " Loop\n");
                List<Node> vizinhos = GetVizinhos(x);
                if (vizinhos != null)
                {
                    foreach (Node n in vizinhos)
                    {
                        if (!n.Visited)
                        {
                            lis.Enqueue(n);//coloca na fila
                            n.Parent = x;//determina o Parent
                            n.Visited = true;//marca como visitado
                        }
                    }
                }
            }
            //Console.WriteLine(ini.ToString());

            return r;
        }

        public List<Node> DepthFirstSearch(string begin)
        {
            Clear_Visited(this.nodes);
            Stack<Node> stNode = new Stack<Node>();
            Node ini = Find(begin);
            ini.Visited = true;
            stNode.Push(ini);
            List<Node> r = new List<Node>();
            Node n = stNode.Peek();
            r.Add(n);
            while (stNode.Count() != 0)
            {

                List<Node> vizinhos = GetVizinhos(n);
                if(vizinhos!=null)
                {
                    int cont = 0;
                    foreach(Node v in vizinhos)
                    {
                        if(!v.Visited)
                        {
                            v.Visited = true;
                            v.Parent = n;
                            stNode.Push(v);
                        }
                        else
                        {
                            cont += 1;
                        }
                    }
                    if(cont == vizinhos.Count())
                    {
                        n  = stNode.Pop();
                        r.Add(n);
                    }
                }
            }
            return r;
        }

       /* public void AddNode(string name)
        {
            if (Find(name) == null) nodes.Add(new Node(name, 0));
        }*/
        public void AddNode(string name, object info)
        {
            if (nodes != null) { nodes.Add(new Node(name, info)); }
            else
            {
                nodes.Add(new Node(name, info));
            }
        }
        public void RemoveNode(string name)
        {
            Node n = Find(name);
            //para cada Nó contido no grafo, verifica todas as arestas para encontrar ocorrências do nó que vai ser
            //removido
            if (n != null)
            {
                foreach (Node x in this.Nodes)
                {
                    ArrayList eList = new ArrayList();
                    foreach (Edge e in x.Edges)
                    {
                        if (e.From == n || e.To == n)
                        {
                            eList.Add(e);
                        }
                    }
                    foreach (Edge e in eList)
                    {
                        x.RemoveEdge(e);
                    }
                }
                nodes.Remove(n);
            }
        }
        private Node Find(string name)
        {
            foreach (Node x in nodes)
            {
                if (name.Equals(x.Name))
                {
                    return x;
                }
            }
            return null;
        }

        public void AddEdge(string nameFrom, string nameTo, int cost)
        {
            Node f = Find(nameFrom);
            Node t = Find(nameTo);
            if (f != null && t != null)
            {
                Edge e = new Edge(f, t, cost);
                foreach (Edge x in f.Edges)
                {
                    if (x.Same(e))
                    {
                        return;
                    }
                }
                f.Edges.Add(e);
            }
        }
        public Node[] GetNeighbours(string from)
        {
            Node x = Find(from);
            if (x != null)
            {
                Node[] n = new Node[x.Edges.Count];
                int i = 0;
                foreach (Edge e in x.Edges)
                {
                    n[i++] = e.To;
                }
                return n;
            }
            return null;
        }
        public bool IsValidPath(ref Node[] nodes, params string[] path)
        {
            //iniciar a validação aqui
            Node a, b;
            for (int i = 0; i < path.Length - 1; i++)
            {
                a = this.Find(path[i]);
                b = this.Find(path[i + 1]);
                //Boolean[] bl= new Boolean[path.Length];
                Boolean flag = false;
                foreach (Edge e in a.Edges)
                {
                    if (e.To == b)
                    {
                        //bl[j++] = true;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return false;
                }
            }
            return true;
        }
        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }

        public List<Node> Nodes1 { get => nodes; set => nodes = value; }
        
        public void Clear_Visited(List<Node> Nodes)
        {
            if(this.Nodes != null)
            {
                foreach (Node n in Nodes)
                {
                    n.Visited = false;
                    n.Parent = null;
                }
            }
        }
        public List<Node> GetVizinhos(Node x)
        {
            List<Node> r = new List<Node>();
            foreach (Edge e in x.Edges)
            {
                r.Add(e.To);
            }
            return r;
        }
    }
}
