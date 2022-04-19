using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static getObj;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public GameObject itemInHand = null;
    void Start()
    {
        itemInHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        int distance = 50;

        if (Input.GetKeyUp(KeyCode.E)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, distance)) {
                print(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "ratio") {
                    Destroy(hit.collider.gameObject);

                    gameObject.GetComponent<Inventory>().bullets += 1;
                    print(gameObject.GetComponent<Inventory>().bullets);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.F)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject obj = getObj.obj(ray, distance);
            if (obj != null) {
                if (itemInHand != null) {
                    itemInHand.transform.parent = null;
                    print("You have dropped " + itemInHand.name);
                    itemInHand = null;
                    return;
                }
                if (obj.tag == "ratio") {
                    itemInHand = obj;
                    obj.transform.SetParent(gameObject.transform, true);
                }
            }
        }
    }
}
