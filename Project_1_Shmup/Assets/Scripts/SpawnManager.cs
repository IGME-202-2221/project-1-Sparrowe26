using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> prefabs = new List<GameObject>();

    public Vector2 minSpawnPoint, maxSpawnPoint;

    public List<GameObject> spawnedCreatures = new List<GameObject> ();

    public float period = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (period > 4f)
        {
            Spawn();
            period = 0;
        }
        period += Time.deltaTime;
    }

    public void Spawn()
    {
        CleanUp();

        int randNum = Random.Range(1, 4);

        for (int i = 0; i < randNum; i++)
        {
            GameObject newCreature = SpawnCreature();

            //add spawned creatures to spawned & collision lists
            spawnedCreatures.Add(newCreature);
            GameObject.Find("CollisionManager").GetComponent<CollisionManager>().collidables.Add(newCreature);
        }
    }

    private GameObject SpawnCreature()
    {
        GameObject newCreature;

        Vector3 spawnPoint = Vector3.zero;
        spawnPoint.x = Random.Range(minSpawnPoint.x, maxSpawnPoint.x);
        spawnPoint.y = Random.Range(minSpawnPoint.y, maxSpawnPoint.y);

        newCreature = Instantiate(PickRandomCreature(), spawnPoint, Quaternion.identity);

        return newCreature;
    }

    void CleanUp()
    {
        //destroy any gameobjects if out-of-bounds
        for (int i = 0; i < spawnedCreatures.Count; i++)
        {
            if (spawnedCreatures[i].transform.position.y > 5 || spawnedCreatures[i].transform.position.y < -5)
            {
                Destroy(spawnedCreatures[i]);

                spawnedCreatures.Remove(spawnedCreatures[i]);
                i--;
            }
            else if (spawnedCreatures[i].transform.position.x < -10)
            {
                Destroy(spawnedCreatures[i]);
                GameObject.Find("CollisionManager").GetComponent<CollisionManager>().collidables.Remove(spawnedCreatures[i]);

                spawnedCreatures.Remove(spawnedCreatures[i]);
                i--;
            }
        }
    }

    GameObject PickRandomCreature()
    {
        GameObject pickedCreature;

        //Pick random #
        float randVal = Random.Range(0f, 1f);

        //spawn based on % likely
        //brown 50%
        if (randVal < .5)
        {
            pickedCreature = prefabs[0];
        }
        //gray 30%
        else if (randVal < .80)
        {
            pickedCreature = prefabs[1];
        }
        //black 20% (these ones are faster)
        else
        {
            pickedCreature = prefabs[2];
        }

        return pickedCreature;
    }
}
