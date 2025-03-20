using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : Interactables
{
    public Sprite Open;
    public Sprite Close;

    private SpriteRenderer sr;
    private bool isOpen;

    public override GameObject Interact()
    {
        if (isOpen)
        {
            sr.sprite = Close;
        }
        else
        {
            Destroy(gameObject);//deletes clues on field and it presumably get into inventory
        }
        isOpen = !isOpen;
        return null;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Close;

    }

}
