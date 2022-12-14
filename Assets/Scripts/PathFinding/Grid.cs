using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask obstacle_mask;
    public float nodeRadius;
    private float nodeDiameter;
    public Vector2 gridSize;
    private int gridCellsX, gridCellsY;
    private Node[,] grid;

    public bool isDrawGizmos;


    private void Awake()
    {
        isDrawGizmos = false;
        nodeDiameter = nodeRadius * 2;
        gridCellsX = Mathf.RoundToInt(gridSize.x / nodeDiameter);
        gridCellsY = Mathf.RoundToInt(gridSize.y / nodeDiameter);
        CreateGrid();
    }

    public int Maxsize
    {
        get { return gridCellsX * gridCellsY; }
    }

    private void CreateGrid()
    {
        grid = new Node[gridCellsX, gridCellsY];
        Vector3 gridLeftBottomCorner = transform.position - Vector3.right * gridSize.x/2 - Vector3.up * gridSize.y/2;

        for (int x = 0; x < gridCellsX; x++)
        {
            for (int y = 0; y < gridCellsY; y++)
            {
                Vector3 curNodePos = gridLeftBottomCorner + (x * nodeDiameter + nodeRadius) * Vector3.right
                                                          + (y * nodeDiameter + nodeRadius) * Vector3.up;
                bool walkable = !(Physics2D.Raycast(curNodePos, Vector3.down, nodeRadius, obstacle_mask));
                grid[x, y] = new Node(walkable, curNodePos, x, y);
            }
        }
    }

    public Node GetNodeFromPosition(Vector3 position)
    {
        float percentX = Mathf.Clamp01((position.x + gridSize.x/2) / gridSize.x);
        float percentY = Mathf.Clamp01((position.y + gridSize.y/2) / gridSize.y);

        int x = Mathf.RoundToInt((gridCellsX-1) * percentX);
        int y = Mathf.RoundToInt((gridCellsY-1) * percentY);

        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (y == 0 && x == 0)
                    continue;

                int nodeX = node.gridX + x;
                int nodeY = node.gridY + y;
                if (nodeX >= 0 && nodeX < gridCellsX && nodeY >= 0 && nodeY < gridCellsY)
                    neighbours.Add(grid[nodeX, nodeY]);
            }
        }

        return neighbours;
    }

    void OnDrawGizmos()
    {
        //draw grid contour
        Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 1));
        {
            //draw grid
            if (grid != null && isDrawGizmos)
            {
                foreach (Node node in grid)
                {
                    Gizmos.color = (node.walkable) ? Color.white : Color.red;
                    Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - 0.3f));
                }
            }
        }
    }

}
