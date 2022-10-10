using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CircleCollision(GameObject vehicle, GameObject collidable)
    {
        //find the radii of the bounding circles using sprite dimensions
        float vehicleRadius = 75f * 1.5f / 100f;
        float collidableRadius = 40f * 1.5f / 100f;

        //calculate the safe distance between objects squared
        float safeDistance = (vehicleRadius + collidableRadius);

        //calculate the current distance between objects squared
        float distance =
           Mathf.Pow(vehicle.GetComponent<SpriteRenderer>().bounds.center.x
           - collidable.GetComponent<SpriteRenderer>().bounds.center.x, 2)
           +
           Mathf.Pow(vehicle.GetComponent<SpriteRenderer>().bounds.center.y
           - collidable.GetComponent<SpriteRenderer>().bounds.center.y, 2);

        //compare the two distances
        if(Mathf.Sqrt(distance) < safeDistance)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    public bool BulletCollision(GameObject bullet, GameObject collidable)
    {
        //find the radii of the bounding circles using sprite dimensions
        float bulletRadius = 75f / 100f;
        float collidableRadius = 40f * 1.5f / 100f;

        //calculate the safe distance between objects squared
        float safeDistance = (bulletRadius + collidableRadius);

        //calculate the current distance between objects squared
        float distance =
           Mathf.Pow(bullet.GetComponent<SpriteRenderer>().bounds.center.x
           - collidable.GetComponent<SpriteRenderer>().bounds.center.x, 2)
           +
           Mathf.Pow(bullet.GetComponent<SpriteRenderer>().bounds.center.y
           - collidable.GetComponent<SpriteRenderer>().bounds.center.y, 2);

        //compare the two distances
        if (Mathf.Sqrt(distance) < safeDistance)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
}
