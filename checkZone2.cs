using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testzone : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        print("You have entered the zone");
        // print(collider.gameObject.name);
        // print(collider.gameObject.tag);
    }

    void OnTriggerExit(Collider collider)
    {
        print("You have left the zone");
        // print(collider.gameObject.name);
        // print(collider.gameObject.tag);
    }
}
