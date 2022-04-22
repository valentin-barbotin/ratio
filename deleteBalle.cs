using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteBalle : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        print("ratio");
        Destroy(collider.gameObject);
    }
}
