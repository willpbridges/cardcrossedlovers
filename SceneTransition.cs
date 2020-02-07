using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// we are using this for simple way to transition between scenes :)
public class SceneTransition : MonoBehaviour
{
    public void toFrank()
    {
        SceneManager.LoadScene("Cards - Frank");
    }

    public void toKaren()
    {
        SceneManager.LoadScene("Cards - Karen");
    }

    public void toFrankChair()
    {
        SceneManager.LoadScene("ChairFrank");

    }

    public void toKarenChair()
    {
        SceneManager.LoadScene("ChairKaren");

    }

    public void toCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void toTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
