using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    Transform playerTransform = null;
    public LayerMask players;
    public BoxCollider2D bc2D;
    Vector2 topRight;
    Vector2 bottomLeft;
    public bool isPickedUp = false;

    public virtual void PickUp(string player)
    {
        //Ezt írogatom felül
    }
    private void Start()
    {
        bc2D = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        topRight = new Vector2(bc2D.bounds.center.x + bc2D.bounds.extents.x, bc2D.bounds.center.y + bc2D.bounds.extents.y);
        bottomLeft = new Vector2(bc2D.bounds.center.x - bc2D.bounds.extents.x, bc2D.bounds.center.y - bc2D.bounds.extents.y);
        Collider2D hit = Physics2D.OverlapArea(bottomLeft, topRight, players);
        if (hit != null)
        {
            playerTransform = hit.transform;
            if (playerTransform != null)
            {
                if (Input.GetButtonDown("P1_Pickup") && hit.gameObject.name == "Player1")
                {
                    isPickedUp = true;
                    PickUp("Player1");
                }
                else if (Input.GetButtonDown("P2_Pickup") && hit.gameObject.name == "Player2")
                {
                    isPickedUp = true;
                    PickUp("Player2");
                }
            }
        }
        else
            playerTransform = null;
    }
}
