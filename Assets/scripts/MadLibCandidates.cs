using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadLibCandidates : MonoBehaviour {

	public string prompt;
	public string upCandidate;
	public string downCandidate;
	public string leftCandidate;
	public string rightCandidate;
	public string selectedCandidate { get; set; }

	public void ShowPrompt() {

	}

	public void setSelectedCandidate(string s) {
		selectedCandidate = s;
	}
}
