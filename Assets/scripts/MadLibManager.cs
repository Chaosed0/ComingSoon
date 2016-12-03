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

    public float curtainTarget = 20.0f;
    public float curtainTime = 2.0f;

	// Use this for initialization
	void Start () {
        ClearCandidateChoices();

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(1.0f);
        seq.Insert(1.0f, DOTween.To(()=> leftCurtain.x, x => leftCurtain.x = x, -curtainTarget, curtainTime));
        seq.Insert(1.0f, DOTween.To(()=> rightCurtain.x, x => rightCurtain.x = x, curtainTarget, curtainTime));
        seq.AppendInterval(2.0f);
        seq.AppendCallback(StartNextMadlib);
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
