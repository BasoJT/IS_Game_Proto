using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Interactables
{
    public int sceneBuildIndex; //Select which scene it loads

    // private void OnTriggerEnter2D(Collider2D Other)
    // {
    //         if (Other.tag == "Player")//checks if it is the player or a random collider2D
    //        {
    //This loads 1 scene and selected in public int above
    //           SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);


    //       }

    //  }

    public override GameObject Interact()
    {

        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        return null;


    }
}
