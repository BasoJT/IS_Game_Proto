using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnabler : Interactables
{
    public GameObject EnabledUI;
    // Start is called before the first frame update

    public override GameObject Interact()
    {

        if (!EnabledUI.activeSelf)
        {
            EnabledUI.SetActive(true);
            return EnabledUI;
        }
        return null;
    }
}
