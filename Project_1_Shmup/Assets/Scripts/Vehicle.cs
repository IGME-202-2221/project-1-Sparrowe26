using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float speed = 1f;

    Vector3 vehiclePosition = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        vehiclePosition = transform.position;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity = direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        // add velocity to position
        vehiclePosition += velocity;

        // draw vehicle at that position
        transform.position = vehiclePosition;

        if(vehiclePosition.y >= mainCam.orthographicSize)
        {
            vehiclePosition.y = mainCam.orthographicSize;
        }
        else if (vehiclePosition.y <= -mainCam.orthographicSize)
        {
            vehiclePosition.y = -mainCam.orthographicSize;
        }
        if (vehiclePosition.x >= mainCam.orthographicSize * mainCam.aspect)
        {
            vehiclePosition.x = mainCam.orthographicSize * mainCam.aspect;
        }
        else if (vehiclePosition.x <= -mainCam.orthographicSize * mainCam.aspect)
        {
            vehiclePosition.x = -mainCam.orthographicSize * mainCam.aspect;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

        if(direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject temp;

            temp = Instantiate(bullet, vehiclePosition, Quaternion.identity, transform);
            GameObject.Find("CollisionManager").GetComponent<CollisionManager>().bullets.Add(temp);
        }
    }

    public void EnemyCollision()
    {
        //health depletion here
    }
}
