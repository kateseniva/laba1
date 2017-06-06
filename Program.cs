using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1
{
   class Program
    {
        static int i, j, n, p, xn, xk;
        static bool[] flag = new bool[11];
        static uint[,] c = new uint[11, 11];
        static uint[] l = new uint[11];
        static string s = new string(new char[80]);
        static string path = "1";
        static int min(int n)
        {
            int i, result=0;
            for (i = 0; i < n; i++)
                if (!(flag[i])) result = i;
            for (i = 0; i < n; i++)
                if ((l[result] > l[i]) && (!flag[i])) result = i;
            return result;
        }
        static uint minim(uint x, uint y)
        {
            if (x < y) return x;
            return y;
        }
        static void Main(string[] args)
        {
            string s="";
           // string path="";
            Console.Write("Число точок : ");
 
 
            n = Convert.ToInt32(Console.ReadLine());

            Console.Write("Оберiть варiант заповнення матрицi: 1 - рандом, 2 - вручну");
            int choise;
            choise = Convert.ToInt32(Console.ReadLine());
            Random rnd = new Random();

            if (choise == 1) {
                for (i = 0; i < n; i++)
                    for (j = i + 1; j < n; j++)
                    {
                        c[i, j] = (uint) rnd.Next(1, 13);

                    }
                Console.Write("   ");
                for (i = 0; i < n; i++)
                {
                    Console.Write("    X");
                    Console.Write(i + 1);
                }
                Console.Write("\n");
                Console.Write("\n");

                for (i = 0; i < n; i++)
                {
                    Console.Write("X{0:D}", i + 1);

                    for (j = 0; j < n; j++)
                    {
                        Console.Write("{0,6:D}", c[i, j]);
                        c[j, i] = c[i, j];
                    }
                    Console.Write("\n\n");

                }
            }
            if(choise == 2) { 
            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++) c[i,j] = 0;
            for (i = 0; i < n; i++)
                for (j = i + 1; j < n; j++)
                {
                    Console.Write(" цiна дiлянки мережi  x");
                    Console.Write(i + 1);
                    Console.Write(" do x");
                    Console.Write(j + 1);
                    Console.Write(": ");
                    c[i, j] = Convert.ToUInt32(Console.ReadLine());
 
                }
            Console.Write("   ");
            for (i = 0; i < n; i++)
            {Console.Write("    X");
            Console.Write(i + 1);
        }
        Console.Write("\n");
        Console.Write("\n");
 
            for (i = 0; i < n; i++)
            {
                Console.Write("X{0:D}", i + 1);
 
                for (j = 0; j < n; j++)
                {
                    Console.Write("{0,6:D}", c[i, j]);
                    c[j, i] = c[i, j];
                }
                Console.Write("\n\n");
 
            }
            }
            for (i = 0; i < n; i++)
                for (j = 0; j < n; j++)
                    if (c[i,j] == 0) c[i,j] = 65535; //nekonecno
            xn = 1;
            xk = n;
            int rem = xk;
            xk--;
            xn--;
            if (xn == xk)
            {
               
                Console.WriteLine("Початкова i кiнцева точки спiвпадають");
                Console.ReadLine();
                return;
            }

            //------------------------
            dijstra dist = new dijstra(c, n);
            var item = dist.dist;
            for (int i = 0; i < item.Length; i++)
            {
                Console.Write("Node " + (i+1) + " Path price = " + item[i]);
                Console.Write("\n");
             }

            Queue<int> q = new Queue<int>();    //Черга, зберігає номери вершин
            string exit = "";
            int u;
            u = n-1;
            bool[] used = new bool[u + 1];  //масив, відмічає відвідані вершини
            int[][] g = new int[u + 1][];   //масив, мість записи суміжних вершин

            for (int i = 0; i < u+1; i++)
            {
                g[i] = new int[u + 1];
                Console.Write("\n({0}) вершина -->[", i + 1);
                for (int j = 0; j < u + 1; j++)
                {
                    g[i][j] = u+1;
                }
                g[i][i] = 0;
                foreach (var item1 in g[i])
                {
                    Console.Write(" {0}", item1);
                }
                Console.Write("]\n");
            }
            used[u] = true;     //масив, що зберігає стан вершини(відвідували її чи ні)

            q.Enqueue(u);
            Console.WriteLine("Починаємо обхiд з {0} вершини", u + 1);
            while (q.Count != 0)
            {
                u = q.Peek();
                q.Dequeue();
                Console.WriteLine("Перейшли до вузла {0}", u + 1);

                for (int i = 0; i < g.Length; i++)
                {
                    if (Convert.ToBoolean(g[u][i]))
                    {
                        if (!used[i])
                        {
                            used[i] = true;
                            q.Enqueue(i);
                            Console.WriteLine("Додали в чергу вузол {0}", i + 1);
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
    public class dijstra
    {
        public dijstra(uint[,] G, int s)
        {
            initial(0, s);
            while (queue.Count > 0)
            {
                int u = getNextVertex();
                for (int i = 0; i < s; i++)
                {
                    if (G[u, i] > 0)
                    {
                        if (dist[i] > dist[u] + G[u, i])
                        {
                            dist[i] = dist[u] + G[u, i];
                        }
                    }
                }
            }
        }

        public double[] dist { get; set; }
        int getNextVertex()
        {
            var min = double.PositiveInfinity;
            int vertex = -1;

            foreach (int val in queue)
            {
                if (dist[val] <= min)
                {
                    min = dist[val];
                    vertex = val;
                }
            }
            queue.Remove(vertex);
            return vertex;
        }
        List<int> queue = new List<int>();
        public void initial(int s, int len)
        {
            dist = new double[len];

            for (int i = 0; i < len; i++)
            {
                dist[i] = double.PositiveInfinity;
                queue.Add(i);
            }
            dist[0] = 0;
        }
    }
}
