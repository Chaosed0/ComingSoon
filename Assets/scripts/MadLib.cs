using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class MadLib : MonoBehaviour {
	public string[] story;
	public MadLibCandidates[] candidates;
	private bool selectionActive = false;
	//private bool sentenceActive = false;
	private int currentCandidate = 0;
	private string[] selectedCandidates = new string[0];
	private float[] selectedCandidateGrades = new float[0];
	private const float TIME_BETWEEN = 3.5f;
	private const float TIME_BETWEEN_LONG = 5.0f;
	private int currentDisplayingStory = 0;

	public delegate void FinishCallback(float finalGrade);
	public FinishCallback finishCallback;

    public delegate void SwitchSentence();
    public SwitchSentence onSwitchSentence;

    public SoundSystem soundSystem;
	public DudeController dudeController;

	public void StartSelections() {
		if (candidates.Length > 0) {
			selectedCandidates = new string[candidates.Length];
			selectedCandidateGrades = new float[candidates.Length];
			selectionActive = true;
			candidates[currentCandidate].ShowPrompt();
		} else {
			//Go to dialogue directly.
			ClearCandidateChoices();
			StartSentences();
		}
	}

	private void NextSelection() {
		currentCandidate++;
		if (currentCandidate < candidates.Length) {
			candidates[currentCandidate].ShowPrompt();
		} else {
			selectionActive = false;
			currentCandidate = 0;
			//sentenceActive = true;
			ClearCandidateChoices();
			StartSentences();
		}
	}

	public void StartSentences() {
		for (int i = 0; i < story.Length; i++) {
            story[i] = story[i].Replace("{", "<color=yellow>{");
            story[i] = story[i].Replace("}", "}</color>");
			story[i] = string.Format(story[i], selectedCandidates);
		}

		dudeController.StartCharacterTransformSequence();
		Sequence s = DOTween.Sequence();
		s.AppendInterval(dudeController.poseSwitchTime);
		s.AppendCallback(() => {
            string story = this.story[currentDisplayingStory];
            GameObject.Find("MadlibPanel").GetComponent<CanvasGroup>().alpha = 1.0f;
            GameObject.Find("DisplayTextUI").GetComponent<Text>().text = story;

            bool isLong = false;
            if (story.Length >= 100) {
                isLong = true;
            }

            if (onSwitchSentence != null) {
                onSwitchSentence();
            }

			WaitAndDisplayNextSentence(isLong);
		});
	}

	private void NextSentence() {
		currentDisplayingStory++;
		if (currentDisplayingStory < story.Length) {
			dudeController.StartCharacterTransformSequence();
			Sequence s = DOTween.Sequence();
			s.AppendInterval(dudeController.poseSwitchTime);
			s.AppendCallback(() => {
                string story = this.story[currentDisplayingStory];
                GameObject.Find("DisplayTextUI").GetComponent<Text>().text = story;

                bool isLong = false;
                if (story.Length >= 100) {
                    isLong = true;
                }

                if (onSwitchSentence != null) {
                    onSwitchSentence();
                }

                WaitAndDisplayNextSentence(isLong);
			});
		} else {
			dudeController.StartResting();
			Sequence s = DOTween.Sequence();
			s.AppendInterval(dudeController.poseSwitchTime);
			s.AppendCallback(() => {
				currentDisplayingStory = 0;

				float sum = 0.0f;
				for (int i = 0; i < selectedCandidateGrades.Length; i++) {
					sum += selectedCandidateGrades[i];
				}

				if (finishCallback != null) {
					finishCallback(sum);
				}
			});
        }
	}

	private void WaitAndDisplayNextSentence(bool isLong) {
		Sequence s = DOTween.Sequence();
		s.AppendInterval(isLong ? TIME_BETWEEN_LONG : TIME_BETWEEN);
		s.AppendCallback(NextSentence);
	}

	public void ClearCandidateChoices() {
		GameObject.Find("UpTextUI").GetComponent<Text>().text = "";
		GameObject.Find("DownTextUI").GetComponent<Text>().text = "";
		GameObject.Find("LeftTextUI").GetComponent<Text>().text = "";
		GameObject.Find("RightTextUI").GetComponent<Text>().text = "";
		GameObject.Find("PromptTextUI").GetComponent<Text>().text = "";
		GameObject.Find("DisplayTextUI").GetComponent<Text>().text = "";
		GameObject.Find("UIDirArrows").GetComponent<Image>().enabled = false;
	}

	public void Update() {
		if (selectionActive)
		if (Input.GetKeyDown(KeyCode.UpArrow) || 
			Input.GetKeyDown(KeyCode.DownArrow) || 
			Input.GetKeyDown(KeyCode.LeftArrow) || 
			Input.GetKeyDown(KeyCode.RightArrow)) {
            
			if (Input.GetKeyDown(KeyCode.UpArrow)) {
				selectedCandidates[currentCandidate] = candidates[currentCandidate].upCandidate;
                selectedCandidateGrades[currentCandidate] = candidates[currentCandidate].upGrade;
			} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
				selectedCandidates[currentCandidate] = candidates[currentCandidate].downCandidate;
                selectedCandidateGrades[currentCandidate] = candidates[currentCandidate].downGrade;
			} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
				selectedCandidates[currentCandidate] = candidates[currentCandidate].leftCandidate;
                selectedCandidateGrades[currentCandidate] = candidates[currentCandidate].leftGrade;
			} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
				selectedCandidates[currentCandidate] = candidates[currentCandidate].rightCandidate;
                selectedCandidateGrades[currentCandidate] = candidates[currentCandidate].rightGrade;
			}

			//Util.Log("Text chosen " + selectedCandidates[currentCandidate]);
			NextSelection();
		}
	}
}
