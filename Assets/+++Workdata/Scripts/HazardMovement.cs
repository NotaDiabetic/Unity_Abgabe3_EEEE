using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class HazardMovement : MonoBehaviour
{
    public float speed = 3;
    public int startingPoint;
    public Transform[] MovementPoints;
    private int i;

    void Start()
    {
        transform.position = MovementPoints[startingPoint].position;
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, MovementPoints[i].position) < 0.02)
        {
            i++;
            if (i == MovementPoints.Length)
            {
                i = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, MovementPoints[i].position, speed * Time.deltaTime);
    }
}
