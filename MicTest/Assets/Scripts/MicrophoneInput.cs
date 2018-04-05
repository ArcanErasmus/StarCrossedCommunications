using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneInput : MonoBehaviour
{

    public int audioSampleRate = 44100;
    public string microphone;
    public FFTWindow fftWindow;
    public Dropdown micDropdown;
    public Slider thresholdSlider;

    private List<string> options = new List<string>();
    private int samples = 8192;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        // get components
        audioSource = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
