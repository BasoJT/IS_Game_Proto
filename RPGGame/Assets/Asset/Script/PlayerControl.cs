using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject InteractIcon;
    // Start is called before the first frame update
    public float movespd;
    public float spdX;
    public float spdY;
    Rigidbody2D pl;
    private GameObject ActiveUI;

    private Vector2 boxsize = new Vector2(0.1f, 1f);
    void Start()
    {
        pl = GetComponent<Rigidbody2D>();//gravity
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            if(ActiveUI != null)//if ui is active
            {
                ActiveUI.SetActive(false);
            }
        }
        spdX = Input.GetAxisRaw("Horizontal") * movespd;
        spdY = Input.GetAxisRaw("Vertical") * movespd;
        pl.velocity = new Vector2(spdX, spdY);
    }

    public void OpenInteractableIcon()
    {
        InteractIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        InteractIcon.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position,boxsize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            print("hit");
            foreach(RaycastHit2D rc in hits)
            {
                if (rc.IsInteractable()) //did extension
                {
                    GameObject returnedUI;
                    returnedUI = rc.interacty();
                    if(returnedUI != null)//check to see if there is active UI
                    {
                        ActiveUI = returnedUI;
                    }
                    return;//storing taken actve UI 


                }

            }
        }
        
    }
}
