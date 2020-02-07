using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource audSource;

    public AudioClip titleAud;

    public AudioClip frankAud;

    public AudioClip karenAud;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }



}
