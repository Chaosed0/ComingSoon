﻿using UnityEngine;
using UnityEngine.Events;

public class BallControl : _Mono {
    public float moveSpeed = 3.0f;
    public Vector2 stageSize = new Vector2(10.0f, 5.0f);

    Rigidbody2D body = null;
    BoxCollider2D boxCollider = null;
    float moveAngle = 0.0f;

    [System.Serializable]
    public class PlayerScored : UnityEvent {};
    public PlayerScored OnPlayerScored = new PlayerScored();

    [System.Serializable]
    public class AIScored : UnityEvent {};
    public AIScored OnAIScored = new AIScored();

    [System.Serializable]
    public class BallCollided : UnityEvent {};
    public BallCollided OnBallCollided = new BallCollided();

	void Start () {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        ResetBall();
	}

	void FixedUpdate () {
        if (transform.localPosition.y + boxCollider.size.y >= stageSize.y ||
            transform.localPosition.y - boxCollider.size.y <= -stageSize.y)
        {
            Vector2 normal = new Vector2(0.0f, - Mathf.Sign(transform.localPosition.y));
            this.y = Mathf.Sign(transform.localPosition.y) * (stageSize.y - boxCollider.size.y - 0.1f);
            this.reflectMovement(normal);
            if (OnBallCollided != null) {
                OnBallCollided.Invoke();
            }
        }

        if (transform.localPosition.x + boxCollider.size.x >= stageSize.x) {
            if (OnPlayerScored != null) {
                OnPlayerScored.Invoke();
            }
            ResetBall();
        } else if (transform.localPosition.x - boxCollider.size.x <= -stageSize.x) {
            if (OnAIScored != null) {
                OnAIScored.Invoke();
            }
            ResetBall();
        }

        body.velocity = getVelocityVector();
	}

    void ResetBall() {
        int direction = UnityEngine.Random.Range(0, 4);
        float rotation = 45.0f + direction * 90.0f;
        moveAngle = rotation;
        this.x = this.transform.parent.transform.localPosition.x;
        this.y = this.transform.parent.transform.localPosition.y;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        ContactPoint2D contact = collision.contacts[0];
        this.reflectMovement(new Vector2(contact.normal.x, 0.0f));
        if (OnBallCollided != null) {
            OnBallCollided.Invoke();
        }
    }

    // Reflect the angle around the normal of contact
    void reflectMovement(Vector2 normal) {
        normal = normal.normalized;
        Vector2 velocity = getVelocityVector();
        Vector2 reflection = velocity - 2 * (Vector2.Dot(velocity, normal)) * normal;
        moveAngle = Mathf.Atan2(reflection.y, reflection.x) * Mathf.Rad2Deg;
    }

    Vector2 getVelocityVector() {
        return Quaternion.AngleAxis(moveAngle, Vector3.forward) * Vector3.right * moveSpeed;
    }
}
