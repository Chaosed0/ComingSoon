using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
    public BallControl ball;
    public int player;

    Text text;
    int score = 0;

    void Start() {
        text = GetComponent<Text>();
        ball.OnPlayerScored.AddListener(onPlayerScored);
    }

    void onPlayerScored(int player) {
        if (this.player == player) {
            ++score;
            text.text = "" + score;
        }
    }
}
