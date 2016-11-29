using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameExtras.Astar
{

  public class Node : IComparable<Node>
  {
    public int x;
    public int y;
    public int costToGetHere;
    public int estimatedCostToGoal;

    public Node(int x, int y, int costToGetHere, int estimatedCostToGoal)
    {
      this.x = x;
      this.y = y;
      this.costToGetHere = costToGetHere;
      this.estimatedCostToGoal = estimatedCostToGoal;
    }

    public int total()
    {
      return costToGetHere + estimatedCostToGoal;
    }
    public int CompareTo(Node node)
    {
      Node other = node;
      if (this.total() == other.total())
      {
        return 0;
      }
      else return (this.total() < other.total() ? -1 : 1);
    }

    public List<Node> generateNeighbours(bool[,] passable, int[,] distanceMatrix, int goalX, int goalY)
    {
      List<Node> list = new List<Node>();
      createAndAdd(x + 1, y, goalX, goalY, passable, distanceMatrix, list);
      createAndAdd(x - 1, y, goalX, goalY, passable, distanceMatrix, list);
      createAndAdd(x, y + 1, goalX, goalY, passable, distanceMatrix, list);
      createAndAdd(x, y - 1, goalX, goalY, passable, distanceMatrix, list);
      return list;
    }

    private void createAndAdd(int newX, int newY, int goalX, int goalY, bool[,] passable,
                                int[,] distanceMatrix, List<Node> list)
    {
      if (AStarPathfinding.exists(newX, newY, passable) && passable[newX, newY])
      {
        int newCost = this.costToGetHere + 1;
        int newEstimate = AStarPathfinding.euclideanDistance(newX, newY, goalX, goalY);
        Node newNode = new Node(newX, newY, newCost, newEstimate);
        if (distanceMatrix[newX, newY] < 0 || newCost < distanceMatrix[newX, newY])
        {
          // if (newX == goalX && newY == goalY)
          //     System.out.println("adding goal " + newX + " " + newY + " " + newCost);
          list.Add(newNode);
          distanceMatrix[newX, newY] = newCost;
        }
      }
    }
  }
}

