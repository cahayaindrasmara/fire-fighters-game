using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoVolume : MonoBehaviour
{
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("volume", 1f);
        Debug.Log("Volume (AudioListener) saat Start: " + volume);
        AudioListener.volume = volume;
    }
}

