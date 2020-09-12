using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    public int width;
    public int height;
    public int depth;
    public float gridSize;
    private float nodeRadius;

    private Node[,,] nodes;
    private Node startNode;
    private Node goalNode;
    private HashSet<Node> vistedNodes = new HashSet<Node>();

    float timer;

    private GridController gridController;


    void Awake()
    {
        gridController = GetComponent<GridController>();

        nodes = new Node[depth, height, width];
        nodeRadius = gridSize / 2.0f;

        for (int z = 0; z < depth; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3 worldPoint = new Vector3(transform.position.x + x * gridSize + nodeRadius, transform.position.y + y * gridSize + nodeRadius, transform.position.z + z * gridSize + nodeRadius);
                    nodes[z, y, x] = new Node(x, y, z, worldPoint, !Physics.CheckSphere(worldPoint, nodeRadius));
                }
            }
        }
    }

    void Start()
    {
        startNode = nodes[0, 0, 0];
        goalNode = nodes[9, 9, 9];
        ResetNodes();
        ComputePath();
    }

    void ResetNodes()
    {
        if (nodes != null)
        {
            vistedNodes.Clear();
            foreach (Node node in nodes)
            {
                node.traversable = !Physics.CheckSphere(node.WorldPoint, nodeRadius);
                node.gCost = 0;
                node.hCost = 0;
                node.parentNode = null;
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5)
        {
            ResetNodes();
            ComputePath();
            timer = 0;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 nodeSize = new Vector3(gridSize, gridSize, gridSize);

        if (nodes != null)
        {
            foreach (Node node in nodes)
            {
                if (node.traversable)
                {
                    if (gridController.showGrid)
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawWireCube(node.WorldPoint, nodeSize);
                    }
                }
                else
                {
                    if (gridController.showCollisonBox)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(node.WorldPoint, nodeSize);
                    }
                }
            }

            Node n = goalNode;
            if (n != null)
            {
                Gizmos.color = Color.blue;
                while (n != startNode && n != null)
                {
                    Gizmos.DrawCube(n.WorldPoint, nodeSize);
                    n = n.parentNode;
                }
                Gizmos.DrawCube(n.WorldPoint, nodeSize);
            }
        }
    }

    public Node ComputePath()
    {
        if (startNode == null || goalNode == null || nodes == null)
        {
            return null;
        }

        HashSet<Node> nodesToVisit = new HashSet<Node>();
        Node currentNode = startNode;
        while (currentNode != goalNode)
        {
            vistedNodes.Add(currentNode);
            ISet<Node> adjacentNodes = CalculateNeighborhood(currentNode);
            nodesToVisit.UnionWith(adjacentNodes);
            currentNode = nodesToVisit.Aggregate((n1, n2) => n2.FCost > n1.FCost ? n1 : n2);
            nodesToVisit.Remove(currentNode);
        }
        return goalNode;
    }

    public static int CalculateDistance(int startX, int startY, int startZ, int endX, int endY, int endZ)
    {
        int deltaX = Math.Abs(endX - startX);
        int deltaY = Math.Abs(endY - startY);
        int deltaZ = Math.Abs(endZ - startZ);
        int diagonalCost = 0;
        if (deltaX > 0 && deltaY > 0 && deltaZ == 0)
            diagonalCost = Math.Min(deltaX, deltaY) * -6;
        else if (deltaX > 0 && deltaY == 0 && deltaZ > 0)
            diagonalCost = Math.Min(deltaX, deltaZ) * -6;
        else if (deltaX == 0 && deltaY > 0 && deltaZ > 0)
            diagonalCost = Math.Min(deltaY, deltaZ) * -6;
        else if (deltaX > 0 && deltaY > 0 && deltaZ > 0)
            diagonalCost = Math.Min(deltaX, Math.Min(deltaY, deltaZ)) * -6;
        return deltaX * 10 + deltaY * 10 + +deltaZ * 10 + diagonalCost;
    }

    private ISet<Node> CalculateNeighborhood(Node node)
    {
        var adjacentNodes = new HashSet<Node>();
        int sz = node.z == 0 ? 0 : node.z - 1;
        int sy = node.y == 0 ? 0 : node.y - 1;
        int sx = node.x == 0 ? 0 : node.x - 1;
        int ez = node.z == nodes.GetLength(0) - 1 ? nodes.GetLength(0) - 1 : node.z + 1;
        int ey = node.y == nodes.GetLength(1) - 1 ? nodes.GetLength(1) - 1 : node.y + 1;
        int ex = node.x == nodes.GetLength(2) - 1 ? nodes.GetLength(2) - 1 : node.x + 1;

        for (int z = sz; z <= ez; ++z)
        {
            for (int y = sy; y <= ey; ++y)
            {
                for (int x = sx; x <= ex; ++x)
                {
                    if (!(y == node.y && x == node.x && z == node.z) && nodes[z, y, x].traversable && !vistedNodes.Contains(nodes[z, y, x]))
                    {
                        int gCost = CalculateDistance(x, y, z, node.x, node.y, node.z);
                        if (nodes[z, y, x].gCost == 0 || node.gCost + gCost < nodes[z, y, x].gCost)
                        {
                            nodes[z, y, x].gCost = node.gCost + gCost;
                            nodes[z, y, x].parentNode = node;
                        }
                        nodes[z, y, x].hCost = CalculateDistance(x, y, z, goalNode.x, goalNode.y, goalNode.z);
                        adjacentNodes.Add(nodes[z, y, x]);
                    }
                }
            }
        }
        return adjacentNodes;
    }

}
