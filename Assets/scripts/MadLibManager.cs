using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int promptsBeforeDemo = 3;
    public int promptsAfterDemo = 3;

    public Transform perspective1;
    public Transform perspective2;
    public Transform pongGame;

    public SoundSystem soundSystem;
    public DudeController dudeController;
    public DudeController audience;
    public DudeController audience2;

    public float sweatAccelRate = 1.0f;
    public float sweatLevel = 0;

    Phase phase = Phase.BeforeDemo;
    int promptCounter = 0;

	// Use this for initialization
	void Start () {
        DoIntroSequence();
        //TransitionToDemo(DOTween.Sequence());

        Debug.Assert(promptsBeforeDemo + promptsAfterDemo == madlibs.Length);
    }

    void Update() {
        sweatLevel += sweatAccelRate * Time.deltaTime;
        if (sweatLevel >= 100.0f) {
            sweatLevel = 100.0f;
        }
    }

    public void addSweatLevel(float sweatLevel) {
        this.sweatLevel += sweatLevel;
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
        float duration = seq.Duration();
        seq.Insert(duration, DOTween.To(()=> leftCurtain.x, x => leftCurtain.x = x, -curtainStart, 2.0f));
        seq.Insert(duration, DOTween.To(()=> rightCurtain.x, x => rightCurtain.x = x, curtainStart, 2.0f));
        seq.AppendCallback(()=> soundSystem.PlaySound("Curtain2"));
        seq.AppendInterval(2.0f);
		seq.AppendCallback(()=> perspective1.gameObject.SetActive(false));
        seq.AppendCallback(()=> perspective2.gameObject.SetActive(true));
        duration = seq.Duration();
        seq.Insert(duration, DOTween.To(()=> leftCurtain.x, x => leftCurtain.x = x, -curtainTarget, 2.0f));
        seq.Insert(duration, DOTween.To(()=> rightCurtain.x, x => rightCurtain.x = x, curtainTarget, 2.0f));
        seq.AppendCallback(StartPong);
        seq.AppendInterval(7.0f);
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
        ++currentLibIndex;
	}

    void EndMadlib(float finalGrade) {
        Sequence seq = DOTween.Sequence();

        Debug.Log(finalGrade);

        if (finalGrade >= -1.0f) {
            if (finalGrade <= 2.0f) {
                soundSystem.PlaySound("MadlibChoiceMedium");
            } else {
                soundSystem.PlaySound("MadlibChoiceGood");
                seq.AppendCallback(() => audience.StartPose(2));
                seq.AppendCallback(() => audience2.StartPose(2));
                seq.AppendInterval(4.0f);
                seq.AppendCallback(() => audience.StartResting());
                seq.AppendCallback(() => audience2.StartResting());
            }
        } else {
            soundSystem.PlaySound("MadlibChoiceBad");
            seq.AppendCallback(() => audience.StartPose(1));
            seq.AppendCallback(() => audience2.StartPose(1));
            seq.AppendInterval(4.0f);
            seq.AppendCallback(() => audience.StartResting());
            seq.AppendCallback(() => audience2.StartResting());
        }
        sweatLevel -= finalGrade;

        ClearCandidateChoices();
        
        ++promptCounter;
        if (phase == Phase.BeforeDemo && promptCounter >= promptsBeforeDemo) {
            // Go to playing the demo (pong)
            TransitionToDemo(seq);
            promptCounter = 0;
            phase = Phase.DuringDemo;
        } else if (phase == Phase.DuringDemo && promptCounter >= promptsAfterDemo) {
            seq.AppendCallback(() => {
                if (sweatLevel <= 40) {
                    SceneManager.LoadScene("GoodEnding");
                } else if (sweatLevel <= 70) {
                    SceneManager.LoadScene("BadEnding");
                } else {
                    SceneManager.LoadScene("WorstEnding");
                }
            });
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
		GameObject.Find("DisplayTextUI").GetComponent<Text>().text = "";
		GameObject.Find("UIDirArrows").GetComponent<Image>().enabled = false;
		GameObject.Find("MadlibPanel").GetComponent<CanvasGroup>().alpha = 0.0f;
	}
}
