using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public AudioVisualizer micObj;
	public AudioVisualizer instruObj;
	public int playerscore = 0;
	public Text mText;

	// Use this for initialization
	void Start () {
		micObj = GameObject.Find ("Microphone").GetComponent("AudioVisualizer") as AudioVisualizer;
		instruObj = GameObject.Find ("Music").GetComponent("AudioVisualizer") as AudioVisualizer;
	}
	
	// Update is called once per frame
	void Update () {
		mText.text = "";
		if ((micObj.highestBarHit != 0 && instruObj.highestBarHit != 0) && micObj.highestBarHit == instruObj.highestBarHit) {
			playerscore++;
		}
		mText.text = playerscore+"";
	}
}
