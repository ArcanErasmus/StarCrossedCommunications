using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public GameObject microphone; // Microphone GameObject
    public Text instructionsText; // Instructions Text Object
    public Text scoreText; // Score Text Object
    public int playerscore = 0; // Player Score
    public float roundingError; // Margin Of Error when playing notes

    private MicrophoneInput input; // Microphone Input Script that also contains fundamental frequency finding functionality
    private Dictionary<string, float> notes; // Dictionary contating notes and their corresponding fundamental frequency values
    private string noteToPlay; // The Note To Play

    // Use this for initialization
    void Start () {
        // Get microphone input
        input = microphone.GetComponent<MicrophoneInput>();

        // Set up initial note dictionary
        notes = new Dictionary<string, float>
        {
            { "G", 725 },
            { "C", 955 },
            { "E", 1180 },
            { "A", 1600 }
        };

        // Generate an initial random note to play
        GenerateNewRandomNoteToPlay();

        // Set the rounding error
        roundingError = 50;
    }
	
	// Update is called once per frame
	void Update () {
        // Get the current fundamental frequency of the mic input
        float frequency = input.GetFundamentalFrequency();

        // Compare the frequency to the note to play fundamental frequency value, taking possible rounding error in to account
        if (frequency >= (notes[noteToPlay] - roundingError) && frequency <= (notes[noteToPlay] + roundingError))
        {
            // Generate a new random note to play
            GenerateNewRandomNoteToPlay();

            // Update the player score and instruction text
            playerscore++;
            scoreText.text = "Score: " + playerscore;
        }
	}

    void GenerateNewRandomNoteToPlay()
    {
        // Generate a new random note to play
        string nextNote = noteToPlay;
        while (nextNote == noteToPlay)
        {
            nextNote = notes.ElementAtOrDefault(Random.Range(0, notes.Count)).Key;
        }
        noteToPlay = nextNote;

        // Update the instructions text
        instructionsText.text = "Play the note: " + noteToPlay;
    }
}
