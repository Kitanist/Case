using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int Position;
    public float GCost; 
    public float HCost; 
    public float FCost => GCost + HCost;
    public Node Parent; 
}

public class Pathfinding : MonoBehaviour
{
    public GridManager gridManager;

    private Node[,] nodeGrid;
    private int width, height;

    private void Start()
    {
        if (gridManager == null)
        {
            Debug.LogError("GridManager referansý eksik!");
            return;
        }

        width = gridManager.width;
        height = gridManager.height;
        InitializeNodeGrid();
    }

    private void InitializeNodeGrid()
    {
        nodeGrid = new Node[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                nodeGrid[x, y] = new Node { Position = new Vector2Int(x, y) };
            }
        }
    }


    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        Debug.Log($"Yol bulunmaya çalýþýlýyor: {start} -> {end}");

        Node startNode = nodeGrid[start.x, start.y];
        Node endNode = nodeGrid[end.x, end.y];

        if (startNode == null || endNode == null)
        {
            Debug.LogError("Baþlangýç veya bitiþ nodlarý bulunamadý.");
            return null;
        }

        List<Node> openList = new List<Node> { startNode };
        HashSet<Node> closedList = new HashSet<Node>();

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];

            foreach (Node node in openList)
            {
                if (node.FCost < currentNode.FCost || node.FCost == currentNode.FCost && node.HCost < currentNode.HCost)
                {
                    currentNode = node;
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            Debug.Log($"Þu anki düðüm: {currentNode.Position}");

            if (currentNode == endNode)
            {
                Debug.Log("Yol bulundu.");
                return RetracePath(startNode, endNode);
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor) || !IsWalkable(neighbor))
                {
                    continue;
                }

                float newCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                if (newCostToNeighbor < neighbor.GCost || !openList.Contains(neighbor))
                {
                    neighbor.GCost = newCostToNeighbor;
                    neighbor.HCost = GetDistance(neighbor, endNode);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }

            // Debug listelerini kontrol et
            Debug.Log($"OpenList: {openList.Count}, ClosedList: {closedList.Count}");
        }

        Debug.Log("Yol bulunamadý.");
        return null;
    }




    private List<Vector2Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }



    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        int x = node.Position.x;
        int y = node.Position.y;

        if (x - 1 >= 0)
        {
            Node neighbor = nodeGrid[x - 1, y];
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }
        if (x + 1 < width)
        {
            Node neighbor = nodeGrid[x + 1, y];
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }
        if (y - 1 >= 0)
        {
            Node neighbor = nodeGrid[x, y - 1];
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }
        if (y + 1 < height)
        {
            Node neighbor = nodeGrid[x, y + 1];
            if (neighbor != null)
            {
                neighbors.Add(neighbor);
            }
        }

       
        Debug.Log($"Komþular ({x}, {y}): {neighbors.Count}");

        return neighbors;
    }



    private float GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.Position.x - nodeB.Position.x);
        int distY = Mathf.Abs(nodeA.Position.y - nodeB.Position.y);

        return distX + distY; 
    }

    private bool IsWalkable(Node node)
    {
        if (node.Position.x < 0 || node.Position.x >= width || node.Position.y < 0 || node.Position.y >= height)
        {
            Debug.LogError($"Geçersiz hücre konumu: {node.Position}");
            return false;
        }

        Cell cell = gridManager.grid[node.Position.x, node.Position.y];
        if (cell == null)
        {
            Debug.LogError($"Cell bulunamadý: {node.Position}");
            return false;
        }

        bool walkable = !cell.isPath; 
        Debug.Log($"Hücre ({node.Position}): Geçiþ {(walkable ? "izin verilen" : "engellenmiþ")}");
        return walkable;
    }




}
