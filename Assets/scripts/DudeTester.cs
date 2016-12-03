using System;
using UnityEngine;

class DudeTester : MonoBehaviour
{
    public float period = 3.0f;
    float timer = 0.0f;
    DudeController controller;

    void Start() {
        controller = GetComponent<DudeController>();
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer >= period) {
            timer = 0.0f;
            controller.switchPose();
        }
    }
}
