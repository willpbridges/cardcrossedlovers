using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShowDescriptionText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI text;
    public Image panel;

    public bool isDisabled = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log("Mouse is over GameObject.");

        if (!isDisabled)
        {
            panel.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      //  Debug.Log("Mouse is no longer on GameObject.");

        panel.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
