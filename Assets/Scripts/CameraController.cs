using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float angularVelocity = 10;


    private Vector3 direction = Vector3.zero;
        
    void Update()
    {
        direction = Vector3.zero;
        int x = 0;
        int z = 0;
        if (Input.GetKey(KeyCode.W))
        {
            z += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += 1;
        }
   
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction.x = vertical;
        direction.y = horizontal;


        if (Input.GetMouseButton(1))
        {
            transform.Rotate(direction * Time.deltaTime * angularVelocity);
        }
        transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * angularVelocity);
    }
}
