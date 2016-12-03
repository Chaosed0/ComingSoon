using System;
using UnityEngine;

class DudeController : MonoBehaviour
{
    public float poseSwitchTime = 1.0f;
    public Sprite[] poses = null;
    public int initialPose = 0;

    SpriteRenderer spriteRenderer = null;
    int currentPoseIndex = 0;

    float poseSwitchTimer = 1.0f;
    bool needsSwitchPose = false;
    Vector3 initialScale = new Vector3(1.0f, 1.0f, 1.0f);

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;
        currentPoseIndex = initialPose;
        spriteRenderer.sprite = poses[initialPose];
    }

    void Update() {
        if (poseSwitchTimer <= poseSwitchTime) {
            poseSwitchTimer += Time.deltaTime;
            float actualSwitchTime = poseSwitchTime / 2.0f;

            if (poseSwitchTimer <= poseSwitchTime - actualSwitchTime) {
                float interp = Util.easeInOutQuad(poseSwitchTimer / (poseSwitchTime - actualSwitchTime));
                transform.localScale = new Vector3(initialScale.x * (1.0f - interp), initialScale.y, initialScale.z);
            } else {
                if (needsSwitchPose) {
                    needsSwitchPose = false;
                    currentPoseIndex = UnityEngine.Random.Range(0,poses.Length);
                    spriteRenderer.sprite = poses[currentPoseIndex];
                }

                float interp = Util.easeInOutQuad((poseSwitchTimer - actualSwitchTime) / (poseSwitchTime - actualSwitchTime));
                transform.localScale = new Vector3(initialScale.x * interp, initialScale.y, initialScale.z);
            }
        } else {
            transform.localScale = initialScale;
        }
    }

    public void switchPose() {
        poseSwitchTimer = 0.0f;
        needsSwitchPose = true;
    }
}
