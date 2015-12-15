using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	
	private Paddle paddle; //CHANGED FROM PUBLIC TO PRIVATE TO WORK BETTER WITH PREFAB
	private bool hasStarted = false;
	private Vector3 paddleToBallVector;
	void Start () {
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	void Update(){
		if(!hasStarted){
			this.transform.position = paddle.transform.position + paddleToBallVector;
			
			if(Input.GetMouseButtonDown(0)){
				print("Mouse clicked.");
				hasStarted = true;
				this.rigidbody2D.velocity  = new Vector2 (2f, 10f);
			}
		}
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		// Ball does not trigger sound when brick is destoyed.
		// Not 100% sure why, possibly because brick isn't there.
		
		Vector2 tweak = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
		if (hasStarted) {	
			audio.Play();
			rigidbody2D.velocity += tweak;
		}
	}
}
