using System;
using UnityEngine;
using DG.Tweening;

public class DudeController : MonoBehaviour
{
	public float poseSwitchTime = 0.25f;
	public Sprite[] poses = null;
	public int restPose = 0;
	public _Mono dude = null;
	private int nextPose = -1;

    public float initialScale = 0.5f;
    public float bounceScale = 0.4f;

	int currentPoseIndex = 0;
	Sequence actionSequence;

	void Start() {
		actionSequence = DOTween.Sequence();
        currentPoseIndex = restPose;
		dude.spriteRenderer.sprite = poses[restPose];
	}

	public void StartCharacterTransformSequence() {
		actionSequence = DOTween.Sequence();
        nextPose = -1;
		actionSequence.AppendInterval(poseSwitchTime);
		actionSequence.Append(transform.DOScale(bounceScale, poseSwitchTime / 2f));
		actionSequence.AppendCallback(switchToRandomActionOrRestPose);
		actionSequence.Append(transform.DOScale(initialScale, poseSwitchTime / 2f));
	}

	public void StartResting() {
		actionSequence = DOTween.Sequence();
        nextPose = restPose;
		actionSequence.AppendInterval(poseSwitchTime);
		actionSequence.Append(transform.DOScale(bounceScale, poseSwitchTime / 2f));
		actionSequence.AppendCallback(switchToRandomActionOrRestPose);
		actionSequence.Append(transform.DOScale(initialScale, poseSwitchTime / 2f));
	}

	public void StartPose(int index) {
		actionSequence = DOTween.Sequence();
        nextPose = index;
		actionSequence.AppendInterval(poseSwitchTime);
		actionSequence.Append(transform.DOScale(bounceScale, poseSwitchTime / 2f));
		actionSequence.AppendCallback(switchToRandomActionOrRestPose);
		actionSequence.Append(transform.DOScale(initialScale, poseSwitchTime / 2f));
	}

    public void switchToRandomActionOrRestPose() {
        int nextPoseIndex = restPose;
		if (nextPose >= 0) {
			nextPoseIndex = nextPose;
		} else {
			do {
				nextPoseIndex = UnityEngine.Random.Range(0, poses.Length);
			} while (nextPoseIndex == currentPoseIndex || nextPoseIndex == restPose);
			dude.xs *= UnityEngine.Random.Range(0, 1.0f) < initialScale ? -1.0f : 1.0f;
		}
		dude.spriteRenderer.sprite = poses[nextPoseIndex];
		currentPoseIndex = nextPoseIndex;
	}
}
