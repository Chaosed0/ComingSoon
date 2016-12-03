using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour {
    Rigidbody2D body = null;
    public float moveSpeed = 3.0f;

	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        float vertical = Input.GetAxis("Vertical");
        body.velocity = new Vector2(0.0f, vertical * moveSpeed);
	}
}
