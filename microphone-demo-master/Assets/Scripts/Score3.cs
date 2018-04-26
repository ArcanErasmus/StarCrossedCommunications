using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score3 : MonoBehaviour
{

    public GameObject microphone; // Microphone GameObject
    public Text instructionsText; // Instructions Text Object
    public Text scoreText; // Score Text Object
    public Text freqText;
    public int playerscore = 0; // Player Score
    public float roundingError; // Margin Of Error when playing notes
	public Text percentageText;
	public int totalScore = 25;
	public float percentage = 0.0f;
	public Image progress;

    private MicrophoneInput input; // Microphone Input Script that also contains fundamental frequency finding functionality
    private Dictionary<string, float> notes; // Dictionary contating notes and their corresponding fundamental frequency values
    private string noteToPlay; // The Note To Play

    private List<float> noteFrequencyLogs = new List<float>(new float[] { 71.24374f, 74.18611f, 77.13511f, 78.61377f, 81.55579f, 84.50099f, 87.44839f, 88.91675f, 91.86607f, 94.80811f, 96.28093f, 99.22879f, 102.174f, 105.1214f, 106.5937f, 109.5391f, 112.4842f, 113.9569f, 116.9018f, 119.847f, 122.7923f, 124.2647f, 127.2103f, 130.1557f, 131.6284f, 134.5748f, 137.52f, 140.4653f, 141.9387f, 144.8833f, 147.8294f, 149.3021f, 152.2478f, 155.193f, 158.1383f, 159.6112f, 162.5568f, 165.5021f, 166.9751f, 169.9205f, 172.866f, 175.8116f, 177.2842f, 180.2298f, 183.1753f, 184.648f, 187.5935f, 190.539f, 193.4845f, 194.9572f, 197.9028f, 200.8483f, 202.3211f, 205.2665f, 208.212f, 211.1575f, 212.6303f, 215.5757f, 218.5213f, 219.994f, 222.9395f, 225.885f, 228.8305f });
    private string[] noteLetters = new string[] { "C", "D", "E", "F", "G", "A", "B" };

    // Use this for initialization
    void Start()
    {
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

        // Reset player score
        playerscore = 0;

        // Set the total score to hit to progress to the next level
        totalScore = 25;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current fundamental frequency of the mic input
        float frequency = input.GetFundamentalFrequency();

        // Compare the frequency to the note to play fundamental frequency value, taking possible rounding error in to account
        //if (frequency >= (notes[noteToPlay] - roundingError) && frequency <= (notes[noteToPlay] + roundingError))
        if (MatchNote(frequency) == noteToPlay)
        {
            // Generate a new random note to play
            GenerateNewRandomNoteToPlay();

            // Update the player score and instruction text
            playerscore++;
            scoreText.text = "Score: " + playerscore;
			percentage = ((float)playerscore / (float)totalScore) * 100.0f;
			progress.fillAmount = percentage/100.0f;
			percentageText.text = Mathf.RoundToInt(percentage) + "%";

            // If the perecentage hits 100% progress to the win screen
            if (percentage >= 100)
            {
                SceneManager.LoadScene(7); // Load next scene
            }
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

    string MatchNote(float inputFreq)
    {
        if (inputFreq == 0) // no input or not above threshold
        {
            freqText.text = "Current Note: Too quiet";
            return "X";
        }

        float frequency = Mathf.Log(inputFreq, 1.04f);// 1.04f is very important, it is the base used to get the logs list

        // this gets us index of note in list, or if not found, negative of index where it would be inserted in list 
        int closest = noteFrequencyLogs.BinarySearch(frequency);

        string closestNote = "X"; // X == out of range
        if (frequency < 70f)
        {
            freqText.text = "Current Note: Too low";
        }
        else if (frequency > 230f)
        {
            freqText.text = "Current Note: Too high";
        }
        else
        {
            if (closest > -1) // rare, exact note match
            {
                closestNote = noteLetters[closest % 7];
            }
            else // have match to next note
            {
                closest = ~closest; // bitwise negation
                // we have next closest note, test whether closer to that or to previous note
                if (closest == 0) // just below lowest note
                {
                    closestNote = noteLetters[0];
                }
                else if (closest == 63) // just above highest note
                {
                    closestNote = noteLetters[6];
                }
                else // between two notes in list
                {
                    // determine what note it is closest to
                    float range = noteFrequencyLogs[closest] - noteFrequencyLogs[closest - 1];
                    if (range / (noteFrequencyLogs[closest] - frequency) > 0.5f) // closest to higher note
                    {
                        closestNote = noteLetters[closest % 7];
                    }
                    else // closest to lower note
                    {
                        closestNote = noteLetters[closest - 1 % 7];
                    }
                }
            }

            freqText.text = "Current Note: " + closestNote;
        }

        return closestNote;
    }
}
