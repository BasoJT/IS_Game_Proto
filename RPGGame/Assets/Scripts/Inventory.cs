using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : Interactables
{
    [SerializeReference]public GameObject Clue;
    public TMP_Text InventoryDisplay;
    Dictionary<GameObject, int> PlayerInventory = new Dictionary<GameObject, int>();

    void Start()
    {
       // PlayerInventory.Add(Clue, 0);//Clue Counter added to dictionary
        DisplayInventory();//display Inventory
        Interact();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DisplayInventory()
    {
        InventoryDisplay.text = "";
        foreach (var item in PlayerInventory)
        {
            InventoryDisplay.text += "Item: " + item.Key + " Quantity: " + item.Value + "\n";
        }
    }

    public void addclue()
    {
        if (PlayerInventory.ContainsKey(Clue))
        {
            PlayerInventory[Clue]++;

        }
        else
        {
            PlayerInventory.Add(Clue, 0);
        }
        DisplayInventory();
    }
    public override GameObject Interact()
    { 
        
        
        if (Clue == isActiveAndEnabled)
        {
            addclue();


            
            
        }
        return null;
    }
    
}
