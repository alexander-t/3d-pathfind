using UnityEngine;

public class TransformRotationController : MonoBehaviour
{
    private float timer;
        
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.05) {
            gameObject.transform.Rotate(Vector3.one, 2);
            timer = 0;
        }
    }
}
