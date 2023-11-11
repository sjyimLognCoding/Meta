using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    [SerializeField] GameObject target;
    public float radius = 5.0f;
    public float rotationSpeed = 30.0f; 
    private Vector3 center;  
    private float angle = 0.0f;

    void Start()
    {
        center = transform.position; 
    }

    void Update()
    {
        angle += rotationSpeed * Time.deltaTime; 
        float radianAngle = angle * Mathf.Deg2Rad;  

        Vector3 newPosition = new Vector3(
            center.x + radius * Mathf.Cos(radianAngle),
            center.y,
            center.z + radius * Mathf.Sin(radianAngle)
        );

        transform.position = newPosition; 
        transform.LookAt(target.transform);
    }
}
