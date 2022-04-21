using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    IDictionary<IItems, int> items = Enum.GetValues(typeof(IItems))
               .Cast<IItems>()
               .ToDictionary(t => t, t => 10 );

    public enum IItems
    {
        BANANE,    
        EAU,    
        BALLE,    
    }

    void Start()
    {
        this.list();
    }

    void list() {
        items.All(item => {
            print(item.Key + " : " + item.Value);
            return true;
        });
    }

    public void add(IItems item, int quantity = 1) {
        items[item] += quantity;
    }

    public void remove(IItems item) {
        items[item] -= 1;
    }

    public int get(IItems item) {
        return items[item];
    }

    public void use(IItems item) {
        items[item] -= 1;
    }
}
