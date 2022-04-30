using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI; // Pour utiliser les fonctionnalit�s du syst�me UI
using UnityEngine;
using static getObj;
using static Inventory;

public class Flags : MonoBehaviour
{
    public int pointsUS = 0;
    public int pointsRU = 0;
    public string name = "";

    void Start()
    {
    }

    void Update()
    { 
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            print(item);
        }
    }
}
