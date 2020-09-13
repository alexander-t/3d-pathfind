using UnityEngine;

namespace Environment
{
    /**
     * Spawns objects in an area bounded by a sphere.
     **/
    public class SphereSpawner : MonoBehaviour
    {
        public GameObject[] spawnPrefabs;
        public float radius = 10;
        public int asteroids = 10;
        public float minScale = 1;
        public float maxScale = 1;
        public bool drawGizmos = false;

        void Start()
        {
            for (int i = 0; i < asteroids; i++)
            {
                Vector3 spawnPosition = transform.position + Random.insideUnitSphere * radius;
                int prefabIndex = Random.Range(0, spawnPrefabs.Length);

                GameObject newObject = Instantiate(spawnPrefabs[prefabIndex], spawnPosition, Quaternion.identity, this.transform) as GameObject;
                newObject.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
                newObject.transform.Rotate(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            }
        }

        
        void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.DrawWireSphere(transform.position, radius);
                for (int i = 0; i < asteroids; i++)
                {
                    Vector3 position = transform.position + Random.insideUnitSphere * radius;
                    Gizmos.DrawSphere(position, Random.Range(minScale, maxScale));
                }
            }
        }
    }
}