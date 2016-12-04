using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MadLibManager : MonoBehaviour {

	public MadLib[] madlibs;
	private int currentLibIndex = 0;

    public float delayBetweenMadlibs = 1.0f;
    public _Mono leftCurtain;
    public _Mono rightCurtain;

    public float curtainStart = 0.0f;
    public float curtainTarget = 10.0f;

    public Transform perspective1;
    public Transform perspective2;
    public Transform pongGame;

    public SoundSystem soundSystem;
    public DudeController dudeController;
    public DudeController audience;

	// Use this for initialization
	void Start () {
        DoIntroSequence();
    }

    void DoIntroSequence() {
        ClearCandidateChoices();

        soundSystem.PlaySound("Opening");

        Sequence seq = DOTween.Sequence();
        seq.Insert(3.0f, DOTween.To(()=> leftCurtain.x, x => leftCurtain.x = x, -curtainTarget, 3.0f));
        seq.Insert(3.0f, DOTween.To(()=> rightCurtain.x, x => rightCurtain.x = x, curtainTarget, 3.0f));
        seq.AppendInterval(2.0f);
        seq.AppendCallback(StartNextMadlib);
    }

    void TransitionToDemo() {
        ClearCandidateChoices();

        Sequence seq = DOTween.Sequence();
        seq.Insert(0.0f, DOTween.To(()=> leftCurtain.x, x => leftCurtain.x = x, -curtainStart, 2.0f));
        seq.Insert(0.0f, DOTween.To(()=> rightCurtain.x, x => rightCurtain.x = x, curtainStart, 2.0f));
        seq.AppendInterval(2.0f);
        seq.AppendCallback(()=> perspective1.gameObject.SetActive(false));
        seq.AppendCallback(()=> perspective2.gameObject.SetActive(true));
        seq.Insert(4.0f, DOTween.To(()=> leftCurtain.x, x => leftCurtain.x = x, -curtainTarget, 2.0f));
        seq.Insert(4.0f, DOTween.To(()=> rightCurtain.x, x => rightCurtain.x = x, curtainTarget, 2.0f));
        seq.AppendCallback(StartPong);
        seq.AppendCallback(StartNextMadlib);
	}

    void StartPong() {
        soundSystem.PlayBackgroundMusic("Pong");
        pongGame.gameObject.SetActive(true);
    }

	void StartNextMadlib() {
		madlibs[currentLibIndex].StartSelections();
        madlibs[currentLibIndex].finishCallback += EndMadlib;
        madlibs[currentLibIndex].onSwitchSentence += OnSwitchSentence;
	}

    void EndMadlib(MadLibCandidates.ChoiceGrade finalGrade) {
        int poseIndex = 0;
        if (finalGrade == MadLibCandidates.ChoiceGrade.Good) {
            soundSystem.PlaySound("MadlibChoiceGood");
            poseIndex = 2;
        } else {
            soundSystem.PlaySound("MadlibChoiceBad");
            poseIndex = 1;
        }

        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => audience.switchToPose(poseIndex));
        seq.AppendInterval(4.0f);
        seq.AppendCallback(() => audience.switchToRestPose());

        dudeController.switchToRestPose();

        // Temporary - we should do several madlibs first
        TransitionToDemo();
    }

    void OnSwitchSentence() {
        Debug.Log("switch");
        dudeController.switchToRandomActionPose();
    }
    
	public void ClearCandidateChoices() {
		GameObject.Find("UpTextUI").GetComponent<Text>().text = "";
		GameObject.Find("DownTextUI").GetComponent<Text>().text = "";
		GameObject.Find("LeftTextUI").GetComponent<Text>().text = "";
		GameObject.Find("RightTextUI").GetComponent<Text>().text = "";
		GameObject.Find("PromptTextUI").GetComponent<Text>().text = "";
		GameObject.Find("PromptTextUI").GetComponent<Text>().text = "";
		GameObject.Find("MadlibPanel").GetComponent<CanvasGroup>().alpha = 0.0f;
	}
}
