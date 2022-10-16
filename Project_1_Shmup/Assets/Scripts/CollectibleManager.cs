using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> prefabs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTrash(Vector3 position)
    {
        //Instantiate new collectible into CollisionManager's trash list
        GameObject temp = Instantiate(PickRandomTrash(), position, Quaternion.identity);
        GameObject.Find("CollisionManager").GetComponent<CollisionManager>().trash.Add(temp);
    }

    private GameObject PickRandomTrash()
    {
        GameObject pickedTrash;

        //Pick random #
        float randVal = Random.Range(0f, 1f);

        //choose prefab based on percentages
        //popcorn 70%
        if (randVal < .7)
        {
            pickedTrash = prefabs[0];
        }
        //cookie 30%
        else
        {
            pickedTrash = prefabs[1];
        }

        return pickedTrash;
    }
}
