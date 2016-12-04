using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MadLibManager : MonoBehaviour {

    enum Phase {
        BeforeDemo,
        DuringDemo
    }

	public MadLib[] madlibs;
	private int currentLibIndex = 0;

    public _Mono leftCurtain;
    public _Mono rightCurtain;

    public float curtainStart = 0.0f;
    public float curtainTarget = 10.0f;
    public int promptsBeforeDemo = 1;
    public int promptsAfterDemo = 3;

    public Transform perspective1;
    public Transform perspective2;
    public Transform pongGame;

    public SoundSystem soundSystem;
    public DudeController dudeController;
    public DudeController audience;

    Phase phase = Phase.BeforeDemo;
    int promptCounter = 0;

	// Use this for initialization
	void Start () {
        DoIntroSequence();
        //TransitionToDemo(DOTween.Sequence());
    }

    void DoIntroSequence() {
        ClearCandidateChoices();

        soundSystem.PlaySound("Opening");

        Sequence seq = DOTween.Sequence();
        seq.Insert(0.5f, DOTween.To(()=> leftCurtain.x, x => leftCurtain.x = x, -curtainTarget, 3.0f));
        seq.Insert(0.5f, DOTween.To(()=> rightCurtain.x, x => rightCurtain.x = x, curtainTarget, 3.0f));
        seq.AppendInterval(0.4f);
		seq.AppendCallback(StartNextMadlib);
    }

    void TransitionToDemo(Sequence seq) {
        seq.Insert(4.0f, DOTween.To(()=> leftCurtain.x, x => leftCurtain.x = x, -curtainStart, 2.0f));
        seq.Insert(4.0f, DOTween.To(()=> rightCurtain.x, x => rightCurtain.x = x, curtainStart, 2.0f));
        seq.AppendInterval(2.0f);
		seq.AppendCallback(()=> perspective1.gameObject.SetActive(false));
        seq.AppendCallback(()=> perspective2.gameObject.SetActive(true));
        seq.Insert(8.0f, DOTween.To(()=> leftCurtain.x, x => leftCurtain.x = x, -curtainTarget, 2.0f));
        seq.Insert(8.0f, DOTween.To(()=> rightCurtain.x, x => rightCurtain.x = x, curtainTarget, 2.0f));
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

        ClearCandidateChoices();

        Sequence seq = DOTween.Sequence();
        //seq.AppendCallback(() => audience.switchToPose(poseIndex));
        seq.AppendInterval(4.0f);
        //seq.AppendCallback(() => audience.switchToRestPose());
        
        ++promptCounter;
        if (phase == Phase.BeforeDemo && promptCounter >= promptsBeforeDemo) {
            // Go to playing the demo (pong)
            TransitionToDemo(seq);
            promptCounter = 0;
            phase = Phase.DuringDemo;
        } else if (phase == Phase.DuringDemo && promptCounter >= promptsAfterDemo) {
            // TODO: End game
            promptCounter = 0;
        } else {
            // Go to new madlib
            seq.AppendCallback(StartNextMadlib);
        }
    }

    void OnSwitchSentence() {
        int randSound = Random.Range(1, 5);
        soundSystem.PlaySound("Murmur" + randSound);
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
