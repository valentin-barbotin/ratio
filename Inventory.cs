using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public int bullets = 5;
    IDictionary<IItems, int> items;

    public enum IItems
    {
        BANANE,    
        EAU,    
        BALLE,    
    }

    void Start()
    {
        // string[] _items = new string[] { "banane", "ratio", "fusil", "potion", "eau" };
        items = new Dictionary<IItems, int>();
        foreach (int item in Enum.GetValues(typeof(IItems)))
        {
            // items.Add(item, 0);
            print(item);
        }
    }

    void list() {
        foreach (var item in items)
        {
            print(item.Key + " : " + item.Value);
        }
    }

    public void add(IItems item) {
        if (items.ContainsKey(item)) {
            items[item] += 1;
        }
    }

    public void remove(IItems item) {
        if (items.ContainsKey(item)) {
            items[item] -= 1;
        }
    }

    public int get(IItems item) {
        if (items.ContainsKey(item)) {
            return items[item];
        }
        return 0;
    }

    public void use(IItems item) {
        if (items.ContainsKey(item)) {
            items[item] -= 1;
        }
    }
}
