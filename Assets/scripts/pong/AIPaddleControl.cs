using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddleControl : MonoBehaviour {
    public float moveSpeed = 3.0f;
    public Transform ball = null;

    Rigidbody2D body = null;

	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        float vertical = Mathf.Sign(ball.position.y - this.transform.position.y);
        body.velocity = new Vector2(0.0f, vertical * moveSpeed);
	}
}
