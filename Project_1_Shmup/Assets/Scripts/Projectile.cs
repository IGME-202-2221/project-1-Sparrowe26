using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    GameObject vehicle;

    [SerializeField]
    float speed = 1f;

    Vector3 bulletPosition; 
    Vector3 direction = Vector3.right;
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        bulletPosition = transform.position;
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity = direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        bulletPosition += velocity;

        transform.position = bulletPosition;
    }
}
