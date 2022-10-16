using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    GameObject target;

    Vector3 enemyPosition = Vector3.zero;
    Vector3 direction = Vector3.left;
    Vector3 velocity = Vector3.zero;

    private bool moveUp;
    private bool runAway;
    private bool fleeUp;


    Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        runAway = false;
        enemyPosition = transform.position;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Velocity = direction * speed * deltaTime
        velocity = direction * speed * Time.deltaTime;

        float randNum = Random.Range(0f, 1f);

        //.5% chance to swap bool
        if (randNum < .005)
        {
            moveUp = !moveUp;
        }

        if (!runAway)
        {
            // move the enemy up or down depending on moveUp bool
            if (moveUp)
            {
                enemyPosition.y += velocity.x;
            }
            else
            {
                enemyPosition.y -= velocity.x;
            }

            enemyPosition.x += velocity.x;

            if (enemyPosition.y >= mainCam.orthographicSize)
            {
                enemyPosition.y = mainCam.orthographicSize;
            }
            else if (enemyPosition.y <= -mainCam.orthographicSize)
            {
                enemyPosition.y = -mainCam.orthographicSize;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.left);
        }
        else
        {
            speed = 8f;
            if (fleeUp)
            {
                enemyPosition.y -= velocity.x;
                enemyPosition.x -= velocity.x;
            }
            else
            {
                enemyPosition.y += velocity.x;
                enemyPosition.x -= velocity.x;
            }
            transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.right);
        }

        // draw enemy at that position
        transform.position = enemyPosition;
    }

    public void EnemyCollision()
    {
        float randNum = Random.Range(0f, 1f);

        //determine which direction the rat scurries away
        if (randNum < .5)
        {
            fleeUp = true;
        }
        else
        {
            fleeUp = false;
        }

        if (transform.position.x <= 9)
        {
            GameObject.Find("CollectibleManager").GetComponent<CollectibleManager>().SpawnTrash(transform.position);
        }

        runAway = true;
    }
}
