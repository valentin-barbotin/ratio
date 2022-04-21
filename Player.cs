using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI; // Pour utiliser les fonctionnalit�s du syst�me UI
using UnityEngine;
using static getObj;
using static Inventory;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public GameObject itemInHand = null;
    public Inventory inventory;

    public Text bulletsTxt;

    void Start()
    {
        this.itemInHand = null;
        this.inventory = new Inventory();
    }

    // Update is called once per frame
    void Update()
    {
        bulletsTxt.text = "Bullets : " + this.inventory.get(IItems.BALLE);
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
                    // inventory.add(IItems.RATIO);
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
}
