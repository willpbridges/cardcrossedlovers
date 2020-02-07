using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTween : MonoBehaviour
{
    public GameObject back1;
    public GameObject back2;
    public GameObject back3;

    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alpha(back1.GetComponent<RectTransform>(), 0f, 1f).setDelay(2f);
        LeanTween.alpha(back2.GetComponent<RectTransform>(), 0f, 1f).setDelay(1.5f);
        LeanTween.alpha(back3.GetComponent<RectTransform>(), 0f, 1f).setDelay(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (back1.GetComponent<Image>().color.a <= 0) 
        {
            back1.SetActive(false);
            back2.SetActive(false);
            back3.SetActive(false);
        }
    }
}
