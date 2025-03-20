using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactables : MonoBehaviour
{
    private void Reset()
    {
        GetComponent <BoxCollider2D>().isTrigger = true;
    }
    public abstract GameObject Interact();//interact funciton will return game object that is UI

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerControl>().OpenInteractableIcon();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<PlayerControl>().CloseInteractableIcon();
    }
}
