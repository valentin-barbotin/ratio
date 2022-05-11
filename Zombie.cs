using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI; // Pour utiliser les fonctionnalit�s du syst�me UI
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int health;
    Animator anim;
    public AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        this.health = 100;
        this.anim = GetComponent<Animator> ();
        this.anim.SetBool("alive", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.health <= 0)
        {
            anim.SetBool("alive", false);
            // Destroy(this.gameObject);
        }
    }
}
