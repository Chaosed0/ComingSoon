using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweatDrop : _Mono {
    public float dropFormationTime = 1.0f;
    public float dropAccelerationTime = 2.0f;
    public float finalDropSpeed = 3.0f;

    public float initialScale = 0.1f;
    public float finalScale = 1.0f;

    enum DropState {
        Forming,
        Accelerating,
        Falling
    }

    DropState state = DropState.Forming;
    float timer = 0.0f;
    float moveSpeed = 0.0f;

	void Start () {
        transform.localScale = new Vector2(initialScale, initialScale);
	}

	void Update () {
        timer += Time.deltaTime;
        if (state == DropState.Forming) {
            float interp = Util.easeInQuad(timer / dropFormationTime);
            float scale = initialScale + interp * (finalScale - initialScale);
            transform.localScale = new Vector2(scale, scale);

            if (timer >= dropFormationTime) {
                transform.localScale = new Vector2(finalScale, finalScale);
                state = DropState.Accelerating;
                timer = 0.0f;
            }
        } else if (state == DropState.Accelerating) {
            float interp = Util.easeInQuad(timer / dropAccelerationTime);
            float speed = interp * finalDropSpeed;
            moveSpeed = speed;

            if (timer >= dropAccelerationTime) {
                moveSpeed = finalDropSpeed;
                state = DropState.Falling;
                timer = 0.0f;
            }
        }

        this.y -= moveSpeed * Time.deltaTime;
	}
}
