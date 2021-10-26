using System;
using System.Linq;
using GameExtras.Astar;
using GameExtras.PriorityQueue;

namespace GameExtras
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test routine for priority queue
            PriorityQueueMin<string> pq = new PriorityQueueMin<string>();
            pq.insert("this");
            pq.insert("is");
            pq.insert("a");
            pq.insert("test");

            // Test of enumerable interface
            string[] ar = pq.ToArray();
            for (int i = 0; i < ar.Length; i++)
            {
                Console.WriteLine(ar[i]);
            }
            foreach (string str in pq)
            {
                Console.WriteLine(str);
            }

            while (!pq.IsEmpty())
                Console.WriteLine(pq.DelMin());

            // A Star test
            Console.WriteLine("Distance to goal: " + AStarPathfinding.TestMethod());

            Console.ReadKey();
        }
    }
}
