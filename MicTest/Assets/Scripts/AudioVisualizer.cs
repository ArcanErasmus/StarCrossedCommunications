using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{

    public Transform[] audioSpectrumObjects;
    public float heightMultiplier;
    public int numSamples = 1024; // has to be power of 2
    public FFTWindow fftWindow;
    public float lerpTime = 1;

    // Update is called once per frame
    void Update()
    {
        // initialize float arr
        float[] spectrum = new float[numSamples];

        // populate arr with freq spectrum data
        GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, fftWindow);

        // loop over audioSpectrumObjects and modify according to frequency spectrum data
        for (int i = 0; i < audioSpectrumObjects.Length; i++)
        {
            // apply height mult to intensity
            float intensity = spectrum[i] * heightMultiplier;

            // calc obj scale
            float lerpY = Mathf.Lerp(audioSpectrumObjects[i].localScale.y, intensity, lerpTime);// smoothes out intensity change

            // apply scale to obj
            audioSpectrumObjects[i].localScale = new Vector3(audioSpectrumObjects[i].localScale.x, lerpY, audioSpectrumObjects[i].localScale.z);
        }
    }
}