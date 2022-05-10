using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI; // Pour utiliser les fonctionnalit�s du syst�me UI
using UnityEngine;
using static getObj;
using static Inventory;

public class Player2 : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public GameObject itemInHand = null;
    public Inventory inventory;

    public Text bulletsTxt;
    public Text flagsTxt;
    public Text crosshairTxt;

    public GameObject ennemy;

    public int ennemyAmount;
    public int currentEnnemy;

    public GameObject[] spawnPoints;

    private int points;
    public int vague;

    public bool alive;

    void Start()
    {
        this.health = 100;
        this.alive = true;
        this.points = 0;
        this.vague = 1;
        this.ennemyAmount = 5;
        this.itemInHand = null;
        this.inventory = new Inventory();
        StartCoroutine("handleCapture");
        this.spawnPoints = GameObject.FindGameObjectsWithTag("ennemySpawnPoint");

        for (int i = 0; i < this.ennemyAmount; i++)
        {
            GameObject spawnPoint = this.spawnPoints[UnityEngine.Random.Range(0, this.spawnPoints.Length)];
            GameObject z = Instantiate(ennemy, spawnPoint.transform.position, Quaternion.identity);
        }
        this.currentEnnemy = this.ennemyAmount;
    }

    void UpdateHud() {
        this.bulletsTxt.text = String.Format("Life : {0} - Bullets : {1}", Math.Max(0, this.health), this.inventory.get(IItems.BALLE));

        int vague = 2;
        int points = 3;
        this.flagsTxt.text = String.Format("Vague : {0} Points {1} - Zombie(s) : {2}", this.vague, this.points, this.currentEnnemy);
    }


    void UpdateTarget(GameObject target) {

        switch (target.tag)
        {
            case "ratio":
                this.crosshairTxt.text = "Prendre";
                break;

            default:
                this.crosshairTxt.text = "X";
                break;
        }
    }

    void HandleFlags() {
        GameObject[] flags = GameObject.FindGameObjectsWithTag("flag");
        foreach (var item in flags)
        {
            if (Vector3.Distance(item.transform.position, gameObject.transform.position) > 15) {
                return;
            }
            this.points = Math.Min(this.points += 1, 100);
        }
    }

    // Update is called once per framecollider.
    void Update()
    {
        if (this.health <= 0)
        {
            if (this.alive)
            {
                this.alive = false;
                print("You are dead");
                return;
            }
            return;
        }
        RaycastHit hit;
        int distance = 50;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GameObject obj = getObj.obj(ray, distance);
        this.UpdateHud();
        if (obj != null)
        {
            this.UpdateTarget(obj);
        }

        if (Input.GetKeyUp(KeyCode.E)) {

            if (Physics.Raycast(ray, out hit, distance)) {
                if (obj.tag == "ratio") {
                    Destroy(obj);
                    print("ajout inventaire");
                    this.inventory.add(IItems.BALLE, 200);
                }
            }
            return;
        }

        if (Input.GetKeyUp(KeyCode.F)) {

            if (itemInHand != null) {
                itemInHand.transform.parent = null;
                print("You have dropped " + itemInHand.name);
                itemInHand = null;
                return;
            }

            if (obj != null) {
                if (obj.tag == "ratio") {
                    itemInHand = obj;
                    obj.transform.SetParent(gameObject.transform, true);
                }
            }
        }
    }

    IEnumerator handleCapture()
    {
        while (true)
        {
            this.HandleFlags();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
