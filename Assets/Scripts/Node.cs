using UnityEngine;

public class Node
{
    public int x, y, z;
    private Vector3 worldPoint;
    public bool traversable;
    public int gCost = 0;
    public int hCost = 0;
    public Node parentNode;

    public int FCost
    {
        get => gCost + hCost;
    }

    public Vector3 WorldPoint
    {
        get => worldPoint;
    }

    public Node(int x, int y, int z, Vector3 worldPoint, bool traversable) 
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.worldPoint = worldPoint;
        this.traversable = traversable;
    }

    public Node(Node src) : this(src.x, src.y, src.x, src.WorldPoint, src.traversable) {
    }
}