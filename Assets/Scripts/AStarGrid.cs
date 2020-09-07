using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    public float width;
    public float height;
    public float depth;
    public float gridSize;

    public void Start()
    {

    }
    private void OnDrawGizmos()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Obstacle"))
        {

        }

        for (int z = 0; z < depth / gridSize; z++)
        {
            for (int y = 0; y < height / gridSize; y++)
            {
                for (int x = 0; x < width / gridSize; x++)
                {
                    if (Physics.CheckSphere(new Vector3(transform.position.x - (width / 2) + (x * gridSize) + (gridSize / 2),
                        transform.position.z - (depth / 2) + (z * gridSize) + (gridSize / 2),
                        transform.position.y - (height / 2) + (y * gridSize) + (gridSize / 2)), gridSize / 2))
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3(transform.position.x - (width / 2) + (x * gridSize) + (gridSize / 2),
                         transform.position.z - (depth / 2) + (z * gridSize) + (gridSize / 2),
                         transform.position.y - (height / 2) + (y * gridSize) + (gridSize / 2)), new Vector3(gridSize, gridSize, gridSize));
                    }
                    else
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawWireCube(new Vector3(transform.position.x - (width / 2) + (x * gridSize) + (gridSize / 2),
                            transform.position.z - (depth / 2) + (z * gridSize) + (gridSize / 2),
                            transform.position.y - (height / 2) + (y * gridSize) + (gridSize / 2)), new Vector3(gridSize, gridSize, gridSize));
                    }
                }
            }
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, depth, height));
    }
}
