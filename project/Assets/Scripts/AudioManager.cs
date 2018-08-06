using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManager : MonoBehaviour {
    public AudioSource[] audioSources;
    public AudioClip applause;
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start () {
        audioSources = GetComponents<AudioSource>();
        audioSources[1].Play();
    }
}
