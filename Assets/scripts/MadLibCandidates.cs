using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MadLibCandidates : MonoBehaviour {

    [System.Serializable]
    public enum ChoiceGrade {
        Good,
        Bad
    };

	public string prompt;
	public string upCandidate;
    public ChoiceGrade upGrade = ChoiceGrade.Good;
	public string downCandidate;
    public ChoiceGrade downGrade = ChoiceGrade.Good;
	public string leftCandidate;
    public ChoiceGrade leftGrade = ChoiceGrade.Bad;
	public string rightCandidate;
    public ChoiceGrade rightGrade = ChoiceGrade.Bad;
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
