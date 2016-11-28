using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameExtras.PriorityQueue;

namespace GameExtras
{
  class Program
  {
    static void Main(string[] args)
    {
      /***************************************************************************
    * Test routine.
    ***************************************************************************/
      PriorityQueueMin<string> pq = new PriorityQueueMin<string>();
      pq.insert("this");
      pq.insert("is");
      pq.insert("a");
      pq.insert("test");
      
      string[] ar = pq.ToArray();
      for(int i = 0; i<ar.Length; i++)
      {
        Console.WriteLine(ar[i]);
      }
      foreach(string str in pq)
      {
        Console.WriteLine(str);
      }

      while (!pq.IsEmpty())
        Console.WriteLine(pq.DelMin());
      Console.ReadKey();
    }
  }
}
