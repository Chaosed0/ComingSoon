using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
    public int player;

    Text text;
    int score = 0;

    void Start() {
        text = GetComponent<Text>();
    }

    public void incrementScore() {
        ++score;
        text.text = "" + score;
    }
}
