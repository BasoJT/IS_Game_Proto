using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//extend raycast 2d
//get if the thing is interactable
public static class Extensions
{
    public static bool IsInteractable(this RaycastHit2D hit)
    {
        return hit.transform.GetComponent<Interactables>();
    }
    public static GameObject interacty(this RaycastHit2D hit)//returns game object for UI
    {
         return hit.transform.GetComponent<Interactables>().Interact();
    }
}
