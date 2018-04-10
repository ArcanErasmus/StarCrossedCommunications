using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour
{

    public Dropdown microphone;
    public Slider sensitivitySlider, thresholdSlider;
    public GameObject settingsPanel;
    public GameObject openButton;
    public GameObject music;

    private bool panelActive = false;
    private bool playingMusic = false;
    private AudioSource musicSource;

    // Use this for initialization
    void Start()
    {
        SetDefaults();

        microphone.value = PlayerPrefsManager.GetMicrophone();
        sensitivitySlider.value = PlayerPrefsManager.GetSensitivity();
        thresholdSlider.value = PlayerPrefsManager.GetThreshold();

        // show settings panel on start
        panelActive = true;
        settingsPanel.SetActive(true);

        // not playing music on start
        playingMusic = false;

        // get music source reference
        musicSource = music.GetComponent<AudioSource>();
    }

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMicrophone(microphone.value);
        PlayerPrefsManager.SetSensitivity(sensitivitySlider.value);
        PlayerPrefsManager.SetThreshold(thresholdSlider.value);

        panelActive = !panelActive;
        //settingsPanel.GetComponent<Animator>().SetBool("PanelActive", panelActive);
        settingsPanel.SetActive(false);
    }

    public void SetDefaults()
    {
        microphone.value = 0;
        sensitivitySlider.value = 100f;
        thresholdSlider.value = 0.001f;
    }

    public void OpenSettings()
    {
        panelActive = !panelActive;
        //settingsPanel.GetComponent<Animator>().SetBool("PanelActive", panelActive);
        settingsPanel.SetActive(true);
    }

    public void TogglePanel()
    {
        if (!panelActive)
        {
            OpenSettings();
        }
        else
        {
            SaveAndExit();
        }
    }

    public void PlayPauseMusic()
    {
        if (!playingMusic)
        {
            musicSource.Play();
            playingMusic = !playingMusic;
        }
        else
        {
            musicSource.Stop();
            playingMusic = !playingMusic;
        }
    }
}