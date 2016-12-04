using System;
using UnityEngine;
using DG.Tweening;

public class DudeController : MonoBehaviour
{
	public float poseSwitchTime { get; set; }
	public Sprite[] poses = null;
	public int restPose = 0;
	public _Mono dude = null;
	private bool nextPoseIsRest = true;

	int currentPoseIndex = 0;
	int nextPoseIndex = 0;
	bool needsSwitchPose = false;
	Vector2 initialScale = new Vector2(0.5f, 0.5f);
	Vector2 lowestScale = new Vector2(0.4f, 0.4f);
	Sequence actionSequence;

	void Start() {
		actionSequence = DOTween.Sequence();
        currentPoseIndex = restPose;
		dude.spriteRenderer.sprite = poses[restPose];
		poseSwitchTime = 0.25f;
	}

	public void StartCharacterTransformSequence() {
		actionSequence = DOTween.Sequence();
		nextPoseIsRest = false;
		actionSequence.AppendInterval(poseSwitchTime);
		actionSequence.Append(transform.DOScale(0.4f, poseSwitchTime / 2f));
		actionSequence.AppendCallback(switchToRandomActionOrRestPose);
		actionSequence.Append(transform.DOScale(0.5f, poseSwitchTime / 2f));
	}

	public void StartResting() {
		actionSequence = DOTween.Sequence();
		nextPoseIsRest = true;
		actionSequence.AppendInterval(poseSwitchTime);
		actionSequence.Append(transform.DOScale(0.4f, poseSwitchTime / 2f));
		actionSequence.AppendCallback(switchToRandomActionOrRestPose);
		actionSequence.Append(transform.DOScale(0.5f, poseSwitchTime / 2f));
	}

    void Update() {
		/*
        if (poseSwitchTimer <= poseSwitchTime) {
            poseSwitchTimer += Time.deltaTime;
            float actualSwitchTime = poseSwitchTime / 2.0f;

            if (poseSwitchTimer <= poseSwitchTime - actualSwitchTime) {
                float interp = Util.easeInQuad(poseSwitchTimer / (poseSwitchTime - actualSwitchTime));
                transform.localScale = new Vector3(initialScale.x, initialScale.y + scaling * interp, initialScale.z);
            } else {
                if (needsSwitchPose) {
                    needsSwitchPose = false;
                    spriteRenderer.sprite = poses[nextPoseIndex];
                    currentPoseIndex = nextPoseIndex;
                }

                float interp = Util.easeOutQuad((poseSwitchTimer - actualSwitchTime) / (poseSwitchTime - actualSwitchTime));
                transform.localScale = new Vector3(initialScale.x, initialScale.y + scaling * (1.0f - interp), initialScale.z);
            }
        } else {
            transform.localScale = initialScale;
        }*/
    }

    public void switchToRandomActionOrRestPose() {
		Util.Log(nextPoseIsRest);
		if (nextPoseIsRest) {
			nextPoseIndex = restPose;
		} else {
			do {
				nextPoseIndex = UnityEngine.Random.Range(0, poses.Length);
			} while (nextPoseIndex == currentPoseIndex || nextPoseIndex == restPose);
			dude.xs *= UnityEngine.Random.Range(0, 1.0f) < 0.5f ? -1.0f : 1.0f;
		}
		dude.spriteRenderer.sprite = poses[nextPoseIndex];
		currentPoseIndex = nextPoseIndex;
	}
}
