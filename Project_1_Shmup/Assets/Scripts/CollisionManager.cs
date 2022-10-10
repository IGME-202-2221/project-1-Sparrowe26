using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    //Store all collidable objects
    [SerializeField]
    public List<GameObject> collidables = new List<GameObject>();

    [SerializeField]
    GameObject vehicle;

    public List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CircleDetection();
        HitDetection();

        BulletCleanUp();
    }

    private void CircleDetection()
    {
        //using vehicle, check for collisions with collidable objects
        for (int i = 0; i < collidables.Count; i++)
        {

            //if there's a collision, call collision methods
            if (GetComponent<CollisionDetection>().CircleCollision(vehicle, collidables[i]))
            {
                collidables[i].GetComponent<Enemy>().EnemyCollision();
                collidables.Remove(collidables[i]);
                i--;

                vehicle.GetComponent<Enemy>().EnemyCollision();
            }
        }
    }

    private void HitDetection()
    {
        //using bullet list, check for collisions with collidable objects
        for (int i = 0; i < collidables.Count; i++)
        {
            foreach (GameObject bullet in bullets)
            {
                //if there's a collision, call collision methods
                if (GetComponent<CollisionDetection>().BulletCollision(bullet, collidables[i]))
                {
                    collidables[i].GetComponent<Enemy>().EnemyCollision();
                    collidables.Remove(collidables[i]);
                    i--;
                }
            }
        }
    }

    public void BulletCleanUp()
    {
        //clean up any out of bounds bullets
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].transform.position.x > 10)
            {
                Destroy(bullets[i]);
                bullets.Remove(bullets[i]);
                i--;
            }
        }
    }
}
