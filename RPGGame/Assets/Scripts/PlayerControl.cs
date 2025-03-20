using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private GameObject InteractIcon;//used for clue interaction
    // Start is called before the first frame update
    public float movespd;
    public float jump;
    private float spdX;//movex
    public Rigidbody2D pl;
    private GameObject ActiveUI;
    public bool IsJumping;

    private Vector2 boxsize = new Vector2(0.1f, 1f);
    void Start()
    {
        //pl = GetComponent<Rigidbody2D>();//gravity
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))//interact button
        {
            CheckInteraction();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        { 
            if(ActiveUI != null)//checks if ui is active
            {
                ActiveUI.SetActive(false);
            }
        }
        //spdX = Input.GetAxisRaw("Horizontal") * movespd;
        //spdY = Input.GetAxisRaw("Vertical") * movespd;
        //  pl.velocity = new Vector2(spdX, spdY);

        spdX = Input.GetAxis("Horizontal");
        pl.velocity = new Vector2(movespd * spdX, pl.velocity.y);

        if (Input.GetButtonDown("Jump") && IsJumping == false)
        {
            pl.AddForce(new Vector2(pl.velocity.x, jump));

            
        }

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

    private void OnCollisionEnter2D(Collision2D other)
    {
     
        if (other.gameObject.CompareTag("Floor"))
        {
            IsJumping = false;
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Floor"))
        {
            IsJumping = true;
        }

    }

}
