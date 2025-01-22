using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    public int gridWidth;
    public int gridHeight;
    public float nodeSize;
    public LayerMask obstacleLayer;
    private Node[,] grid;

    private void Start()
    {
        if (gridWidth <= 0 || gridHeight <= 0 || nodeSize <= 0)
        {
            Debug.LogError("Invalid grid dimensions or node size.");
            return;
        }
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridWidth, gridHeight];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWidth / 2 - Vector3.up * gridHeight / 2;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeSize + nodeSize / 2) + Vector3.up * (y * nodeSize + nodeSize / 2);
                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeSize / 2, obstacleLayer);
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = Mathf.Clamp01((worldPosition.x - transform.position.x + gridWidth / 2) / gridWidth);
        float percentY = Mathf.Clamp01((worldPosition.y - transform.position.y + gridHeight / 2) / gridHeight);

        int x = Mathf.RoundToInt((gridWidth - 1) * percentX);
        int y = Mathf.RoundToInt((gridHeight - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridWidth && checkY >= 0 && checkY < gridHeight)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach (var node in grid)
            {
                //Gizmos.color = node.walkable ? Color.white : Color.red;
                //Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeSize - 0.1f));
            }
        }
    }
}

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost => gCost + hCost;

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }
}
