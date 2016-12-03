using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MadLibManager : MonoBehaviour {

	public MadLib[] madlibs;
	private int currentLibIndex = 0;

	// Use this for initialization
	void Start () {
		//StartNextMadlib();
        ClearCandidateChoices();
	}

	void StartNextMadlib() {
		madlibs[currentLibIndex].StartSelections();
	}
    
	public void ClearCandidateChoices() {
		GameObject.Find("UpTextUI").GetComponent<Text>().text = "";
		GameObject.Find("DownTextUI").GetComponent<Text>().text = "";
		GameObject.Find("LeftTextUI").GetComponent<Text>().text = "";
		GameObject.Find("RightTextUI").GetComponent<Text>().text = "";
		GameObject.Find("PromptTextUI").GetComponent<Text>().text = "";
	}
}
