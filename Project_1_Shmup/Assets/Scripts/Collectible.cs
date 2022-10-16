using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickedUp()
    {
        isActive = false;

        GetComponent<SpriteRenderer>().sprite = null;
    }
}
