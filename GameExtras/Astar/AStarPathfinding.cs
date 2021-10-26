using System;
using System.Collections.Generic;
using GameExtras.PriorityQueue;

namespace GameExtras.Astar
{
    public class AStarPathfinding
    {
        public static int Distance(bool[,] passable, int startX, int startY, int goalX, int goalY)
        {
            int[,] distanceMatrix = new int[passable.GetLength(0), passable.GetLength(1)];
            for (int i = 0; i < distanceMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < distanceMatrix.GetLength(1); j++)
                {
                    distanceMatrix[i, j] = -1;
                }
            }
            distanceMatrix[startX, startY] = 0;
            PriorityQueueMin<Node> openSet = new PriorityQueueMin<Node>();
            Node initial = new Node(startX, startY, 0, EuclideanDistance(startX, startY, goalX, goalY));
            openSet.insert(initial);
            while (true)
            {
                if (openSet.IsEmpty())
                {
                    // we failed to find the goal
                    return -1;
                }
                Node current = openSet.DelMin();
                if (current.x == goalX && current.y == goalY)
                {
                    // we found it!
                    return current.costToGetHere;
                }
                // search all the neighbours
                List<Node> neighbours = current.generateNeighbours(passable, distanceMatrix, goalX, goalY);
                openSet.insertRange(neighbours);
            }
        }

        public static int EuclideanDistance(int x1, int y1, int x2, int y2)
        {
            return (int)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public static bool exists(int x, int y, bool[,] passable)
        {
            return x >= 0 && y >= 0 && x < passable.GetLength(0) && y < passable.GetLength(1);
        }


        public static int TestMethod()
        {
            bool[,] testmap = {
                {true , true , false, true , true , true },
                {false, true , false, true , true , false},
                {true , true , false, false, true , true },
                {false, true , true , true , true , false},
                {false, false, false, false, true , false},
                {true , true , true , true , true , true },

        };
            return Distance(testmap, 0, 0, 5, 5);
        }
    }
}
