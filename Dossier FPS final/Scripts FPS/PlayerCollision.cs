using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Pour utiliser les fonctionnalit�s du syst�me UI
using UnityEngine.SceneManagement; // Pour charger des sc�nes, relancer le niveau (par ex)

public class PlayerCollision : MonoBehaviour
{
    public Text healthTxt;
    public int health = 100;

    // D�tecter les collisions avec le tir 
    void OnCollisionEnter(Collision col)
    {
        // Si l'objet qui touche le joueur a le Tag Tourelle
        if (col.gameObject.tag == "Tourelle")
        {
            LooseHealth(20); // Perte de 20 points de vie
            Destroy(col.gameObject); // Destruction du projectile
        }
    }

    // Si le joueur touche un Trigger
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Pic") // Si le joueur touche un pi�ge 
        {
            LooseHealth(100); // Perte de 100 pts de vie
        }

        if (collider.gameObject.tag == "Vie") // Si le joueur touche un Kit de soin 
        {
            LooseHealth(-50); // gain de 50pts de vie
            Destroy(collider.gameObject); // Destruction du projectile
        }
    }


    // Perte de vie
    void LooseHealth(int val) // val: valeur de perte
    {
        health = health - val; // Perte de val% de vie
        // healthTxt.text = health + "%ratio"; // Mise � jour du texte
        if (health < 0)
        {
            health = 0; // on bloque la vie � 0
            // Le perso n'a plus de vie, on lancera le Game Over ici
            SceneManager.LoadScene(0); // On relance la scene d'index 0 (la seule que l'on ait pour l'instant)
        }
    }

    void Update() // val: valeur de perte
    {
        // health = health - val; // Perte de val% de vie
        healthTxt.text = health + "%ratioz"; // Mise � jour du texte
    }
    
}
