using System;
using UnityEngine;

public class DudeController : MonoBehaviour
{
    public float poseSwitchTime = 0.4f;
    public Sprite[] poses = null;
    public int restPose = 0;
    public SpriteRenderer spriteRenderer = null;

    int currentPoseIndex = 0;
    int nextPoseIndex = 0;

    float poseSwitchTimer = 1.0f;
    bool needsSwitchPose = false;
    Vector3 initialScale = new Vector3(1.0f, 1.0f, 1.0f);

    void Start() {
        initialScale = transform.localScale;
        currentPoseIndex = restPose;
        spriteRenderer.sprite = poses[restPose];
    }

    void Update() {
        if (poseSwitchTimer <= poseSwitchTime) {
            poseSwitchTimer += Time.deltaTime;
            float actualSwitchTime = poseSwitchTime / 2.0f;

            if (poseSwitchTimer <= poseSwitchTime - actualSwitchTime) {
                float interp = Util.easeInQuad(poseSwitchTimer / (poseSwitchTime - actualSwitchTime));
                transform.localScale = new Vector3(initialScale.x, initialScale.y - 0.1f * interp, initialScale.z);
            } else {
                if (needsSwitchPose) {
                    needsSwitchPose = false;
                    spriteRenderer.sprite = poses[nextPoseIndex];
                    currentPoseIndex = nextPoseIndex;
                }

                float interp = Util.easeOutQuad((poseSwitchTimer - actualSwitchTime) / (poseSwitchTime - actualSwitchTime));
                transform.localScale = new Vector3(initialScale.x, initialScale.y - 0.1f * (1.0f - interp), initialScale.z);
            }
        } else {
            transform.localScale = initialScale;
        }
    }

    public void switchToRandomActionPose() {
        do {
            nextPoseIndex = UnityEngine.Random.Range(0, poses.Length);
        } while (nextPoseIndex == currentPoseIndex || nextPoseIndex == restPose);

        poseSwitchTimer = 0.0f;
        needsSwitchPose = true;
    }

    public void switchToRestPose() {
        nextPoseIndex = restPose;

        poseSwitchTimer = 0.0f;
        needsSwitchPose = true;
    }
}
