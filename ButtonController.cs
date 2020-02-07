using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    
    public bool confirm = false;

    [SerializeField]
    private Narrative n;
    
    public void ContinuteStoryFrank()
    {
        Debug.Log("Continue Story");
        n.cardChooing = false;
        n.textBox.SetActive(true);
        n.contintueButton.gameObject.SetActive(false);

        n.ContinueFromStop();

    }
}
