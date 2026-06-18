using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tractormove : MonoBehaviour
{
  public Transform pointA;
    public Transform pointB;
    public float speed = 30f;

    private Transform target;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }
}
