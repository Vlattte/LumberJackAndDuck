using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class PathFinder : MonoBehaviour
{
    PathManager pathManager;
    public Grid grid;
    Rigidbody2D rb;
    float speed;

    private void Awake()
    {
        //grid = GetComponent<Grid>();
        pathManager = GetComponent<PathManager>();
        rb = GetComponent<Rigidbody2D>();  
        speed = 2f;
    }


    public void StartFindingPath(Vector3 start, Vector3 end)
    {
        StartCoroutine(FindPath(start, end));
    }


    void MoveTo(Vector2 dist_vec, bool isDistStrg = false)
    {
        float dist = (rb.position - dist_vec).magnitude;
        transform.position = Vector2.MoveTowards(transform.position, dist_vec, speed * Time.deltaTime);
    }


    IEnumerator FindPath(Vector3 startPoint, Vector3 targetPoint)
    {
        bool path_success = false;
        Vector3[] waypoints = new Vector3[0];

        //transform world position to nodes
        Node startNode = grid.GetNodeFromPosition(startPoint);
        Node targetNode = grid.GetNodeFromPosition(targetPoint);

        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> OPEN_SET = new Heap<Node>(grid.Maxsize);
            HashSet<Node> CLOSED_SET = new HashSet<Node>();
            OPEN_SET.Add(startNode);

            while (OPEN_SET.Count > 0)
            {
                Node curNode = OPEN_SET.RemoveFirstItem();

                CLOSED_SET.Add(curNode);

                //if we are in target node
                if (curNode == targetNode)
                {
                    path_success = true;
                    break;
                }

                foreach (Node neib_node in grid.GetNeighbours(curNode))
                {
                    //if neighbour is not walkable or it's in close set
                    //skip to next neighbour
                    if (!neib_node.walkable || CLOSED_SET.Contains(neib_node))
                        continue;

                    int newDistToNeighbour = curNode.g_cost + GetDistance(curNode, neib_node);
                    if (newDistToNeighbour < neib_node.g_cost || !OPEN_SET.Contains(neib_node))
                    {
                        //reculculate F cost
                        neib_node.g_cost = newDistToNeighbour;
                        neib_node.h_cost = GetDistance(neib_node, targetNode);

                        //remember parent
                        neib_node.parentNode = curNode;

                        if (!OPEN_SET.Contains(neib_node))
                        {
                            OPEN_SET.Add(neib_node);
                        }
                    }

                }
            }
        }

            yield return null;
        if (path_success)
            waypoints = ReversedPath(startNode, targetNode);
        pathManager.FinishedProcessingPath(waypoints, path_success);
    }

    Vector3[] ReversedPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 dir = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 dirNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (dir != dirNew)
            {
                waypoints.Add(path[i].position);
            }
            dir = dirNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node fstNode, Node sndNode)
    {
        int distX = Mathf.Abs(fstNode.gridX - sndNode.gridX);
        int distY = Mathf.Abs(fstNode.gridY - sndNode.gridY);

        if (distX >= distY)
            return 14 * distY + 10 * (distX - distY);
        else
            return 14 * distX + 10 * (distY - distX);
    }
    
}
