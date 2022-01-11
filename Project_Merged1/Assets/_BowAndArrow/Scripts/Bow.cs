using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
	[Header ("Assets")] 
	public GameObject arrowPrefab = null;
	
	[Header("Bow")]
	public float grabThreshold = 0.15f;
	public Transform start = null;
	public Transform end = null;
	public Transform socket = null;
	
	private Transform pullingHand = null;
	private Arrow currentArrow = null;
	private Animator animator = null;
	
	private float pullValue = 0.0f;
	
	private void Awake(){
		animator = GetComponent<Animator>();
	}
	
	private void Start () {
		StartCoroutine(CreateArrow(0.0f));
	}
	
	private void Update () {
		if (!pullingHand || ! currentArrow) {
			return;
		 }
		 
		 pullValue = calculatePull(pullingHand);
		 pullValue = Mathf.Clamp(pullValue,0.0f,1.0f);
		 
		 animator.SetFloat("Blend", pullValue);
	}
	
	private float calculatePull (Transform pullHand) {
	
		Vector3 direction = end.position - start.position;
		float magnitude = direction.magnitude;
		
		direction.Normalize();
		Vector3 difference = pullHand.position - start.position;
		
		return Vector3.Dot(difference, direction)/ magnitude;
	}
	
	private IEnumerator CreateArrow(float waitTime){
		//Wait
		yield return new WaitForSeconds(waitTime);
		
		//Create Ob and child it
		GameObject arrowObject = Instantiate (arrowPrefab, socket);
		arrowObject.name = "Arrow";
		
		//Orient 
		arrowObject.transform.localPosition = new Vector3 (0,0,0.425f);
		arrowObject.transform.localEulerAngles = new Vector3(0,0,0);
		
		//Set
		currentArrow = arrowObject.GetComponent<Arrow>();  
	}
	
	public void Pull (Transform hand) {
	
		float distance = Vector3.Distance(hand.position, start.position);
		
		if (distance > grabThreshold ){
			return;
		}
		
		pullingHand = hand;
	}
	
	public void Release () {
		
		if (pullValue > 0.25f) {
			fireArrow();
		}
		
		pullingHand = null;
		pullValue = 0.0f;
		animator.SetFloat("Blend",0);
		
		if ( !currentArrow) {
			StartCoroutine(CreateArrow(0.25f));
		}
	}
	
	private void fireArrow() {
	
		currentArrow.fire(pullValue);
		currentArrow = null;
	
	}
	
	
}
