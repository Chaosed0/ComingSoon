using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MadLibCandidates : MonoBehaviour {

	public string prompt;
	public string upCandidate;
	public string downCandidate;
	public string leftCandidate;
	public string rightCandidate;
	public string selectedCandidate { get; set; }

	public void ShowPrompt() {
		GameObject.Find("UpTextUI").GetComponent<Text>().text = upCandidate;
		GameObject.Find("DownTextUI").GetComponent<Text>().text = downCandidate;
		GameObject.Find("LeftTextUI").GetComponent<Text>().text = leftCandidate;
		GameObject.Find("RightTextUI").GetComponent<Text>().text = rightCandidate;
		GameObject.Find("PromptTextUI").GetComponent<Text>().text = prompt;
	}

	public void setSelectedCandidate(string s) {
		selectedCandidate = s;
	}
}
