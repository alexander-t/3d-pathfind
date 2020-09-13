using UnityEngine;

public class TransformTranslationControler : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float duration = 5;

    private bool direction = true;
    private Vector3 start;
    private Vector3 end;
    private float timeElapsed = 0;


    void Start()
    {
        transform.position = start;
        start = startPosition;
        end = endPosition;
    }


    void Update()
    {
        if (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(start, end, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            if (direction)
            {
                start = transform.position;
                end = startPosition;
            }
            else
            {
                start = startPosition;
                end = endPosition;
            }
            direction = !direction;
            timeElapsed = 0;
        }
    }
}

