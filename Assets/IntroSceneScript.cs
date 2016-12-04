using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneScript : MonoBehaviour {

	private string[] instructions;
	private int currentIndex = 0;

	// Use this for initialization
	void Start() {
		instructions = new string[5];
		instructions[0] = "Coming Soon (Game of the Year) (DLC)(E3 Demo)\n\nPress Space to Continue";
		instructions[1] = "You are a self-proclaimed PR wizard at a medium-sized PR firm. On the day before the E3 conference, you receive an urgent call from famed game company UbiSoNint-EA:";
		instructions[2] = "Our PR guy has been abducted! We need you to replace him to market our new game Angle of Incidence (trademarked)!";
		instructions[3] = "Being a ardent gamer in the Commidore 64 era, you feel an ardent desire to step up to the challenge!!";
		instructions[4] = "So you brush up on your <Arrow-key-to-choose-what-to-say> skills and <WASD-to-play-demo-game> powers, and step into the announcement hall.";
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			currentIndex++;
			if (currentIndex < instructions.Length) {
				GameObject.Find("IntroTextUI").GetComponent<Text>().text = instructions[currentIndex];
			} else {
				SceneManager.LoadScene("PlayableFirstDemo");
			}
		}
	}
}
