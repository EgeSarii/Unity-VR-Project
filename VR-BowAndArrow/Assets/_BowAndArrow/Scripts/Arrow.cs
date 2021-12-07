using UnityEngine;

public class Arrow : MonoBehaviour
{
	public const float SPEED = 2000.0f;
	public Transform tip = null;
	
	private Rigidbody rigidBody = null;
	private bool isStopped = true;
	private Vector3 lastPosition = new Vector3(0,0,0);
	
	private void Awake(){
		GetComponent<Rigidbody>();
	}
	
	private void fixUpdate() {
		if (isStopped) {
			return;
		}
		
		//Rotate
		rigidBody.MoveRotation(Quaternion.LookRotation( rigidBody.velocity, transform.up ));	//To make nice effect of flying
		
		//Collision
		if (Physics.Linecast(lastPosition, tip.position) ){	//Linecast used for high speed objects moving
			Stop();
		}
		
		//Store position
		lastPosition = tip.position;
	}
	
	private void Stop() {
		isStopped = true;
		
		rigidBody.isKinematic = true;
		rigidBody.useGravity = false;
	
	}
	
	public void fire (float pullValue) {
	
		isStopped = false;
		transform.parent = null;
		rigidBody.isKinematic = false;
		rigidBody.useGravity = true;
		rigidBody.AddForce(transform.forward * (pullValue * SPEED));
		
		Destroy(gameObject, 5.0f);	//Remove arrow from scene
		
	}
	 
	
}
