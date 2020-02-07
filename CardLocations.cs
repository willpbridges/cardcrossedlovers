using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLocations : MonoBehaviour
{
   public GameObject[] cards; //0  = past, 1 = present, 2 = future
   private static GameObject[] copy;

    public GameObject currCard;

    void Start()
    {
        copy = cards;
    }

    public void SwapCards(GameObject g, string whereItIs)
    {
        Debug.Log("Swap Cards");
        currCard = g;
        var c2 = cards[0];
        var temp = 0;

        if (whereItIs == "Present")
        {
            c2 = cards[1];
            temp = 1;
        }
        if (whereItIs == "Future")
        {
            c2 = cards[2];
            temp = 2;
        }

        Debug.Log("temp: " + temp);
        Debug.Log("OG list: " + cards[0].name + cards[1].name + cards[2].name);
        
        for (int i = 0; i < 3; i++)
        {
        
            if (currCard.name == cards[i].name)
            {
                Debug.Log("i: " + i);
                cards[temp] = currCard;
                cards[i] = c2;
                break;
            }
        }

        Debug.Log("After Swap list: " + cards[0].name + cards[1].name + cards[2].name);
        copy = cards;
    }

    public static string[] Endings(string character)
    {
        var cardTemp = copy;
        string[] endings = { "_Past", "_Present", "_Future" };
        

        for (int i = 0; i < 3; i++)
        {
            if (cardTemp[i].name == character + "_Fool")
            {
                endings[i] = character + "_Fool" + endings[i];
            }
            if (cardTemp[i].name == character + "_High")
            {
                endings[i] = character + "_High" + endings[i];
            }
            if (cardTemp[i].name == character + "_Wheel")
            {
                endings[i] = character + "_Wheel" + endings[i];
            }
        }

        return endings;
    }
}
