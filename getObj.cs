using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Pour utiliser les fonctionnalit�s du syst�me UI
using UnityEngine.SceneManagement; // Pour charger des sc�nes, relancer le niveau (par ex)

public class getObj : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "FPSController") // Si le joueur touche un pi�ge 
        {
            print("You have been hit by a trap");
            print("You have " + collider.gameObject.GetComponent<Player2>().health + " health left");
        }
    }

    public static GameObject obj(Ray ray, int distance = 50) {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance)) {
            return hit.collider.gameObject;
        } else {
            return null;
        }
    }
}