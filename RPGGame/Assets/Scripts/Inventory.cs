using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Interactables
{
   [SerializeField] public GameObject Clue;
 
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override GameObject Interact()
    { //problem: how to tell if clue has been added to inventory
        Dictionary<int, GameObject> cluecount = new Dictionary<int, GameObject>();
        if (Clue == isActiveAndEnabled)
        {
            cluecount.Add(1, Clue);//yoinked into dictionary



            Destroy(Clue);//destroys sprite and collision
            
        }
        return null;
    }
    
}
