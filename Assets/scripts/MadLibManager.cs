using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadLibManager : MonoBehaviour {

	public MadLib[] madlibs;
	private int currentLibIndex = 0;

	// Use this for initialization
	void Start () {
		StartNextMadlib();	
	}

	void StartNextMadlib() {
		Util.Log(madlibs.Length);
		madlibs[currentLibIndex].StartSelections();
	}

}
