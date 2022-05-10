using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI; // Pour utiliser les fonctionnalit�s du syst�me UI
using UnityEngine.AI;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public GameObject[] points; // Points de patrouille
    public Transform player; // Joueur à attaquer

    public GameObject FPSController;

    NavMeshAgent agent; // Nav Mesh Agent du robot

    Animator anim;

    Zombie zombie;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    int destPointIndex = 0; // Point de destination

    // Start is called before the first frame update
    void Start()
    {
        // On récupère le composant agent
        print("Start nav");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        zombie = GetComponent<Zombie>();
        // Don’t update position automatically
        agent.updatePosition = false;
        GotoNextPoint();
        StartCoroutine("mordre");
    }

    void GotoNextPoint() // récupère une destination au hasard
    {
        this.destPointIndex = UnityEngine.Random.Range(0, points.Length - 1);
    }

    void UpdatePointPos() {
        agent.destination = points[this.destPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (zombie.health <= 0) {
            agent.isStopped = true;
            return;
        }

        UpdatePointPos();

        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot (transform.right, worldDeltaPosition);
        float dy = Vector3.Dot (transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2 (dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp (smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f) {
            velocity = smoothDeltaPosition / Time.deltaTime;
        }

        // bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        anim.SetBool("move", true);
        anim.SetFloat("velx", velocity.x);
        anim.SetFloat("vely", velocity.y);

        // print("velocity : " + velocity);
        // print("should move : " + shouldMove);

        float distance = Vector3.Distance(transform.position, player.position);
        // print(distance);
        if (distance > 3.0)
        {
            agent.isStopped = false;
        //     // On patrouille
        //     // Si on n'a pas de chemin en attente et si on a atteint le point
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                // this.destPointIndex = UnityEngine.Random.Range(0, points.Length - 1);
                print("go next point");
                GotoNextPoint();  // On va au point suivant
            }
        }
        else
        {
            // agent.isStopped = true;
            if (!agent.pathPending && distance < 0.5f)
            {
                agent.isStopped = true;
                print("MORDRE");
            } else {
                agent.isStopped = false;
                agent.destination = player.position;
            }
        }
    }

    IEnumerator mordre()
    {
        while (true)
        {
            this.checkMordre();
            yield return new WaitForSeconds(1f);
        }
    }

    void checkMordre() {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 3.0)
        {
            this.FPSController.GetComponent<Player2>().health -= 5;
        }
    }

    void OnAnimatorMove ()
    {
        // Update position to agent position
        try
        {
            transform.position = agent.nextPosition;
        }
        catch (System.Exception)
        {
            // throw;
        }
    }
}
