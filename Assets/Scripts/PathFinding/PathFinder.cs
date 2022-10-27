using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PathFinder : MonoBehaviour
{
    public Grid grid;
    public Transform BeginingNode, EndNode;
    Rigidbody2D rb;
    float speed;

    private void Awake()
    {
        //grid = GetComponent<Grid>();
        rb = GetComponent<Rigidbody2D>();  
        speed = 2f;
    }


    void Update()
    {
        if(Input.GetButton("Jump"))
        {
            FindPath(BeginingNode.position, EndNode.position);
        }
        
    }

    void MoveTo(Vector2 dist_vec, bool isDistStrg = false)
    {
        float dist = (rb.position - dist_vec).magnitude;
        transform.position = Vector2.MoveTowards(transform.position, dist_vec, speed * Time.deltaTime);

    }


    void FindPath(Vector3 startPoint, Vector3 targetPoint)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        //transform world position to nodes
        Node startNode = grid.GetNodeFromPosition(startPoint);
        Node targetNode = grid.GetNodeFromPosition(targetPoint);

        List<Node> OPEN_SET = new List<Node>();
        HashSet<Node> CLOSED_SET = new HashSet<Node>();
        OPEN_SET.Add(startNode);

        while (OPEN_SET.Count > 0)
        {
            Node curNode = OPEN_SET[0];
                       
            for (int i = 1; i < OPEN_SET.Count; i++)
            {
                if (curNode.f_cost > OPEN_SET[i].f_cost ||
                   curNode.f_cost == OPEN_SET[i].f_cost && curNode.h_cost > OPEN_SET[i].h_cost)
                    curNode = OPEN_SET[i];
            }

            OPEN_SET.Remove(curNode);
            CLOSED_SET.Add(curNode);

            //if we are in target node
            if (curNode == targetNode)
            {
                ReversedPath(startNode, targetNode);
                sw.Stop();
                UnityEngine.Debug.Log("Path_finding " + sw.ElapsedMilliseconds);
                return;
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

                    if(!OPEN_SET.Contains(neib_node))
                    {
                        OPEN_SET.Add(neib_node);
                    }
                }
                
            }
        }
    }

    void ReversedPath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Add(currentNode);
        path.Reverse();
        grid.path = path;
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
