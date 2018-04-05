using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMicTest : MonoBehaviour
{

    public AudioSource source;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = Microphone.Start("Headset Microphone (4- SteelSeries Arctis 7 Chat)", false, 10, 44100);
        source.Play();
        //foreach (string device in Microphone.devices)
        //{
        //    Debug.Log(device);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}