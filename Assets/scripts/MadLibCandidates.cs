using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MadLibCandidates : MonoBehaviour {

	public string prompt;
	public string upCandidate;
    public float upGrade = 0;
	public string downCandidate;
    public float downGrade = 1;
	public string leftCandidate;
    public float leftGrade = 2;
	public string rightCandidate;
    public float rightGrade = 3;
	public string selectedCandidate { get; set; }

	public void ShowPrompt() {
		GameObject.Find("UpTextUI").GetComponent<Text>().text = upCandidate;
		GameObject.Find("DownTextUI").GetComponent<Text>().text = downCandidate;
		GameObject.Find("LeftTextUI").GetComponent<Text>().text = leftCandidate;
		GameObject.Find("RightTextUI").GetComponent<Text>().text = rightCandidate;
		GameObject.Find("PromptTextUI").GetComponent<Text>().text = prompt;
		GameObject.Find("MadlibPanel").GetComponent<CanvasGroup>().alpha = 1.0f;
	}

	public void setSelectedCandidate(string s) {
		selectedCandidate = s;
	}
}
