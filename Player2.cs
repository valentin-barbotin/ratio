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

    public GameObject flag;

    private int points;
    public int vague;

    public bool alive;

    private float endgameTimer;

    public int totalKills;
    private int totalKillsEnd;

    public bool gameEnded;

    public GameObject ak;
    public GameObject famas;

    public bool hasAK;
    public bool hasFAMAS;

    public string currentWeapon;

    void Start()
    {
        this.hasAK = false;
        this.hasFAMAS = false;

        this.gameEnded = false;
        this.totalKills = 0;
        this.endgameTimer = 0f;
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

        if (this.points >= 100) {
            if (this.endgameTimer == 0f) {
                this.endgameTimer = Time.unscaledTime;
            }
            this.flagsTxt.text = String.Format("Victoire : {0} en {1}s - Kills {2}", "Valentin", Math.Round(this.endgameTimer), this.totalKillsEnd);
            return;
        }
        this.flagsTxt.text = String.Format("Vague : {0} Points {1} - Zombie(s) : {2}", this.vague, this.points, this.currentEnnemy);
    }


    void UpdateTarget(GameObject target) {
        switch (target.tag)
        {
            case "ratio":
                this.crosshairTxt.text = "Prendre";
                break;
            
            case "famas":
                this.crosshairTxt.text = "Prendre Famas";
                break;

            case "ak":
                this.crosshairTxt.text = "Prendre AK";
                break;

            default:
                this.crosshairTxt.text = "X";
                break;
        }
    }

    void HandleFlags() {
        if (Vector3.Distance(this.flag.transform.position, gameObject.transform.position) > 15) {
            this.points = Math.Max(this.points -= 1, 0);
        } else {
            this.points = Math.Min(this.points += 1, 100);
            if ((this.points >= 100) && (this.vague > 5)) {
                this.gameEnded = true;
                this.totalKillsEnd = this.totalKills;
            }
        }
    }

    // void switchWeapon() {
    //     switch (true)
    //     {
    //         case Input.GetKeyUp(KeyCode.Alpha1):
    //         {
    //             this.ak.SetActive(true);
    //             this.famas.SetActive(false);
    //             break;
    //         }

    //         case Input.GetKeyUp(KeyCode.Alpha2):
    //         {
    //             this.famas.SetActive(false);
    //             this.ak.SetActive(true);
    //             break;
    //         }
    //         default:
    //     }
    // }

    void takeAK() {
        this.ak.SetActive(true);
        this.famas.SetActive(false);
        this.currentWeapon = "ak";
    }

    void takeFAMAS() {
        this.ak.SetActive(false);
        this.famas.SetActive(true);
        this.currentWeapon = "famas";
    }

    // Update is called once per framecollider.
    void Update()
    {
        // switchWeapon();
        bool Keypad1 = Input.GetKeyUp(KeyCode.Keypad1);
        bool Keypad2 = Input.GetKeyUp(KeyCode.Keypad2);

        if (Keypad1 && this.hasAK) {
            takeAK();
        } else if (Keypad2 && this.hasFAMAS) {
            takeFAMAS();
        }


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
                switch (obj.tag)
                {
                    case "ratio":
                        Destroy(obj);
                        this.inventory.add(IItems.BALLE, 200);
                        break;
                    
                    case "famas":
                        Destroy(obj);
                        this.hasFAMAS = true;
                        takeFAMAS();
                        break;

                    case "ak":
                        Destroy(obj);
                        this.hasAK = true;
                        takeAK();
                        break;

                    default:
                        this.crosshairTxt.text = "X";
                        break;
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
