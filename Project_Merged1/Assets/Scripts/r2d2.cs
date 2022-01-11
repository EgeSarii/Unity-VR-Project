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

    private Vector3 playerPos;
    private Vector3 destination;
	private RectTransform rectTransform;
	private int sceneIndex;
	string[] entranceMessagesStr = new string []{
		"Oh you must be new! Welcome to the Game!",
		"If you want to play just go and enter the door",
		"We will meet there!"
	};
	bool[] entranceMes = new bool[] {true, true, true};
	string[] stairsMessagesStr = new string []{
		"Hey look at the stairs!",
		"It seems it always go up or always down",
		"Interesting, right?",
		"Do you wanna inspect?",
		"Then teleport on it!"
	};
	bool[] stairsMes = new bool[] {true, true, true,true,true};
	
	string[]onStairsMessageStr = new string[]{
		"See if the stairs ends or not",
		"Does this end?",
		"After your discovery get on the ground"
	};

	bool[] onStairsMes = new bool[] {true, true, true};

	string[]afterStairsMessageStr = new string[]{
		"Did you understand the illusion?",
		"It is called Penrose stairs",
		"Let's move to the climbing wall!"
	};

	bool[] afterStairsMes = new bool[] {true, true, true};

	void Start()
	{
		audio =FindObjectOfType<AudioManager>();
		rectTransform = canvas.GetComponent<RectTransform>();
		canvas.SetActive(false);
	}
    void Awake ()
    {
     	playerPos = Input.mousePosition;
		sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    
    // Update is called once per frame
    void Update()
    {		
        if (playerPos != Input.mousePosition)
        {	
			MoveRobot(Input.mousePosition);	
			playerPos = Input.mousePosition;
		}
		if(sceneIndex ==0)
		{
			StartCoroutine(Entrance(3));
		}
		if(sceneIndex ==1)
		{
			StartCoroutine(Stairs(2));
			StartCoroutine(OnStairs(2));
			StartCoroutine(AfterStairs(2));
		}
		

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
		if(Vector3.Distance(robot.transform.position, destination) <= 0.3f )//add if it is an entrance scene
		{ 
			for(int i = 0; i< entranceMessagesStr.Length; i++)
			{
				if (entranceMes[i])
				{
					audio.Play("Excited");
					ShowInfo(entranceMessagesStr[i]);
					yield return new WaitForSecondsRealtime(t);
					entranceMes[i]= false;
				}
			}
		}	
	}

	IEnumerator Stairs(int t)
    {
		for(int i = 0; i< stairsMessagesStr.Length; i++)
		{
			if (stairsMes[i])
			{
				audio.Play("Excited");
				ShowInfo(stairsMessagesStr[i]);
				yield return new WaitForSecondsRealtime(t);
				stairsMes[i]= false;
			}
		}	
	}
	IEnumerator OnStairs(int t)
    {
		Vector3 newPos = new Vector3(0.3f, 0.3f,2.0f);
		if(Vector3.Distance(playerPos, newPos)<=1.0f)
		{
			for(int i = 0; i< onStairsMessageStr.Length; i++)
			{
				if (onStairsMes[i])
				{
					audio.Play("Excited");
					ShowInfo(onStairsMessageStr[i]);
					yield return new WaitForSecondsRealtime(t);
					onStairsMes[i]= false;
				}
			}
		}
	}
	IEnumerator AfterStairs(int t)
    {
		
		if(playerPos.y<=1.0f)
		{
			for(int i = 0; i< afterStairsMessageStr.Length; i++)
			{
				if (afterStairsMes[i])
				{
					audio.Play("Excited");
					ShowInfo(afterStairsMessageStr[i]);
					yield return new WaitForSecondsRealtime(t);
					afterStairsMes[i]= false;
				}
			}
		}
	}
	
}
