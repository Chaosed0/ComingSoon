using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadLibParser : MonoBehaviour {
	public string madLibTemplate = "";
	private char madLibDelim = '_';

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string fillMadLib(string template, string keyword1, string keyword2, string keyword3) {
		List<string> keywords = new List<string>();
		//[keyword1, keyword2, keyword3];
		List<string> plainTexts = new List<string>();
		List<string> inserts = new List<string>();
		string result = "";

		//Include plurals
		int plainTextStart = 0;
		int length = 0;
		for (int i = 0; i < madLibTemplate.Length; i++) {
			if (template[i] == madLibDelim) {
				plainTexts.Add(template.Substring(plainTextStart, length));

				int nextDelimIndex = int.Parse(template[i + 1].ToString());
				inserts.Add(keywords[nextDelimIndex]);
			}
			length++;
		}

		return result;
	}
}
