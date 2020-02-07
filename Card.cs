using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public string whereItIs;
    public Vector3 initialPos;
    public Vector3 swapPos;
    private GameObject card;

    public CardLocations loc;


    void Start()
    {
        card = this.gameObject;
        initialPos = this.transform.position;
        swapPos = initialPos;
        GetComponent<Shadow>().enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Shadow>().enabled = true;
        initialPos = this.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 100f; //distance of the plane from the camera
        this.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Shadow>().enabled = false;
        this.transform.position = swapPos;
        card.transform.position = this.initialPos;
        card = this.gameObject;
        loc.SwapCards(card, whereItIs);
        Debug.Log(this.gameObject.name + " " + whereItIs);
    }

    // Using this to detect colision for where the player places the card.
    // The if statment is there so you dont get the name of any other collider. 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Past" ||
            collision.gameObject.name == "Present" ||
            collision.gameObject.name == "Future")
        {
            whereItIs = collision.gameObject.name;
        }
        else
        { 
            card = collision.gameObject;
            swapPos = collision.gameObject.transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        card = this.gameObject;
        swapPos = this.initialPos;
    }
}
