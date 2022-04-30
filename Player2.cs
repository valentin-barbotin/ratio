using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.UI; // Pour utiliser les fonctionnalit�s du syst�me UI
using UnityEngine;
using static getObj;
using static Inventory;

enum Faction
{
    US,
    RU
}

public class Player2 : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public GameObject itemInHand = null;
    public Inventory inventory;

    public Text bulletsTxt;
    public Text flagsTxt;

    public string faction = "US";

    void Start()
    {
        this.itemInHand = null;
        this.inventory = new Inventory();
        StartCoroutine("handleCapture");
    }

    void UpdateHud() {
        this.bulletsTxt.text = "Bullets : " + this.inventory.get(IItems.BALLE);

        GameObject flag = GameObject.FindGameObjectWithTag("flag");
        this.flagsTxt.text = "";
        // foreach (var item in flags)
        // {
            Flags comp = flag.GetComponent<Flags>();
            
            this.flagsTxt.text += String.Format("{0} {2} | {3} {1}", "US", "RU", comp.pointsUS, comp.pointsRU);
            if (comp.pointsUS >= 100)
            {
                this.flagsTxt.text = "US WIN";
            }
            else if (comp.pointsUS >= 100)
            {
                this.flagsTxt.text = "RU WIN";
            }
        // }
    }

    void HandleFlags() {
        GameObject[] flags = GameObject.FindGameObjectsWithTag("flag");
        foreach (var item in flags)
        {
            if (Vector3.Distance(item.transform.position, gameObject.transform.position) > 15) {
                return;
            }
            Flags comp = item.GetComponent<Flags>();
            // comp.pointsUS += 1;
            switch (this.faction)
            {
                case "US":
                    comp.pointsUS = Math.Min(comp.pointsUS += 1, 100);
                    break;

                case "RU":
                    comp.pointsRU = Math.Min(comp.pointsRU += 1, 100);
                    break;

                default:
                    break;
            }
            // switch (switch_on_faction(this.faction))
            // {
            //     case Faction.US:
            //         if (comp.pointsUS > comp.pointsRU)
            //         {
            //             comp.pointsUS -= 1;
            //             this.HandleVictory();
            //         }
            //         break;
            //     case Faction.RU:
            //         if (comp.pointsRU > comp.pointsUS)
            //         {
            //             comp.pointsRU -= 1;
            //             this.HandleVictory();
            //         }
            //         break;
            // }
            // {
                
            //     default:
            // }
        }
    }

    // Update is called once per framecollider.
    void Update()
    {
        this.UpdateHud();
        RaycastHit hit;
        int distance = 50;

        if (Input.GetKeyUp(KeyCode.E)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject obj = getObj.obj(ray, distance);

            if (Physics.Raycast(ray, out hit, distance)) {
                print(obj.tag);
                if (obj.tag == "ratio") {
                    Destroy(obj);
                    print("ajout inventaire");
                    this.inventory.add(IItems.BALLE, 15);
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

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject obj = getObj.obj(ray, distance);

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
