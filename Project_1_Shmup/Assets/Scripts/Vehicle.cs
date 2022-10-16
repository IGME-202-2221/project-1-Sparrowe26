using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float speed = 1f;

    [SerializeField]
    Text lives;

    [SerializeField]
    Text score;

    [SerializeField]
    Text endScreen;

    Vector3 vehiclePosition = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    Camera mainCam;

    //score information
    public int health;
    public int trashCollected;

    public float period = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        trashCollected = 0;
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

        lives.text = "Lives: " + health;
        score.text = "Trash Collected: " + trashCollected;

        //game ends when lives hit 0
        if (health == 0)
        {
            endScreen.text = "POWERING DOWN...\nFinal Score: " + trashCollected;
            Invoke("EndGame", 2);
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

            //Instantiate bullet at vehicle location
            temp = Instantiate(bullet, vehiclePosition, Quaternion.identity, transform);

            //ass to CollisionManager's bullet list
            GameObject.Find("CollisionManager").GetComponent<CollisionManager>().bullets.Add(temp);
        }
    }

    public void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void EnemyCollision()
    {
        //turn the roomba red for 1sec
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ResetColor", 1f);

        //health depletion here
        if (health > 0)
        {
            health--;
        }
    }

    public void PickUp()
    {
        trashCollected++;
    }

    public void EndGame()
    {
        Time.timeScale = 0;
    }
}
