using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
	
public class r2d2 : MonoBehaviour
{

	public GameObject canvas;
	public AudioManager audio ;
    public Camera cam;
    public NavMeshAgent robot;

    private Vector3 mousePos;
    private Vector3 destination;
	private RectTransform rectTransform;
	private bool mes1 = true;
	private bool mes2 = true;
	private bool mes3 = true;


	string[] entranceMessagesStr = new string []{
		"Oh you must be new! Welcome to the Game!",
		"If you want to play just go and enter the door",
		"We will meet there!"
	};
	
	
	void Start()
	{
		audio =FindObjectOfType<AudioManager>();
		rectTransform = canvas.GetComponent<RectTransform>();
		canvas.SetActive(false);
	}
    void Awake ()
    {
     	mousePos = Input.mousePosition;
    }
    
    // Update is called once per frame
    void Update()
    {		
        if (mousePos != Input.mousePosition)
        {	
			MoveRobot(Input.mousePosition);	
			mousePos = Input.mousePosition;
		}
		StartCoroutine(Entrance(5));
    }

	void MoveRobot(Vector3 dest)
	{
		Ray ray =cam.ScreenPointToRay(dest);
        RaycastHit hit;
		
        if(Physics.Raycast(ray, out hit))
        {
			//Make noise 
			audio.Play("Beeping");

        	//Move the robot
			destination = hit.point;
			float dest_X = (destination.x < 0f) ? destination.x + 0.8f : destination.x - 0.8f;
			float dest_Z = (destination.z < 0f) ? destination.z + 0.8f : destination.z - 0.8f;
				
			destination.Set(dest_X, destination.y, dest_Z);
        	robot.SetDestination(destination);
		}
	}
	
	
	void ShowInfo(string message)
	{
	
		Vector3 rob_pos = robot.transform.position;
		rectTransform.position = new Vector3 (rob_pos.x, rob_pos.y+1f, rob_pos.z);
		canvas.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = message;
		canvas.SetActive(true);
		audio.Play("Excited");
	}
	IEnumerator Entrance(int t)
    {
		if(Vector3.Distance(robot.transform.position, destination) <= 0.3f )
		{ 
			if(mes1)
			{
				audio.Play("Excited");
				ShowInfo(entranceMessagesStr[0]);
				yield return new WaitForSecondsRealtime(t);
				mes1 = false;
			}
			if(mes2)
			{
				audio.Play("Excited");
				ShowInfo(entranceMessagesStr[1]);
				yield return new WaitForSecondsRealtime(t);
				mes2 = false;
			}
			if(mes3)
			{
				audio.Play("Excited");	
				ShowInfo(entranceMessagesStr[2]);
				yield return new WaitForSecondsRealtime(t);
				mes3 = false;
			}
		}	
	}	
	
}
