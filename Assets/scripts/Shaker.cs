using System;
using UnityEngine;

class Shaker : MonoBehaviour
{
    public float frequency = 0.5f;
    public float amplitude = 0.5f;
    public float decay = 0.9f;

    float timer = 0.0f;
    float currentDecay = 1.0f;

    Vector3 currentOffset = new Vector3(0.0f, 0.0f, 0.0f);

    Vector3 currentSample = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 nextSample = new Vector3(0.0f, 0.0f, 0.0f);

    void Start() {
        enabled = false;
    }

    void Update() {
        timer += Time.deltaTime;

        // Generate a new sample
        if (timer >= frequency)
        {
            currentDecay *= decay;
            timer -= frequency;
            currentSample = nextSample;

            float theta = UnityEngine.Random.Range(0.0f, 360.0f);
            float x = Mathf.Cos(theta * Mathf.Deg2Rad) * amplitude * currentDecay;
            float y = Mathf.Sin(theta * Mathf.Deg2Rad) * amplitude * currentDecay;

            nextSample = new Vector3(x, y, 0.0f);

            if (currentDecay <= 0.05f) {
                enabled = false;
            }
        }

        float lerp = timer / frequency;
        currentOffset = currentSample * (1.0f - lerp) + nextSample * lerp;
    }

    void startShake() {
        timer = 0.0f;
        currentDecay = 1.0f;
        enabled = true;
    }
}
