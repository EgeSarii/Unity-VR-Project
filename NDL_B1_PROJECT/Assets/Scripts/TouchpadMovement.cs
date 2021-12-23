using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class TouchpadMovement : MonoBehaviour
{
    public float sensitivity = 0.1f;
    public float max_speed = 1.0f;

    public SteamVR_Action_Boolean MovePress = null;
    public SteamVR_Action_Vector2 MoveValue = null;

    private float speed = 0.0f;
    private CharacterController CharacterController = null;
    private Transform cameraRig = null;
    private Transform head = null;

    private void Awake(){
        CharacterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement();
    }

    private void HandleHead() {
        Vector3 oldPosition = cameraRig.position;
        Quaternion oldRotation = cameraRig.rotation;

        transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y, 0.0f);

        cameraRig.position = oldPosition;
        cameraRig.rotation = oldRotation;
    }

    private void CalculateMovement (){
        Vector3 OrientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(OrientationEuler);
        Vector3 movement = new Vector3(0,0,0);

        if (MovePress.GetStateUp(SteamVR_Input_Sources.Any)){
            speed = 0;
        }
        if (MovePress.state){
            speed += MoveValue.axis.y * sensitivity;
            speed = Mathf.Clamp(speed, - max_speed, max_speed);
            movement += orientation * (speed * Vector3.forward) * Time.deltaTime;
        }

        CharacterController.Move(movement);
    }

    private void HandleHeight(){
        float headhHeight = Mathf.Clamp(head.localPosition.y,1,2);
        CharacterController.height = headhHeight;

        Vector3 newCenter = new Vector3(0,0,0);
        newCenter.y = CharacterController.height /2;
        newCenter.y += CharacterController.skinWidth;
        
        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y,0) * newCenter;

        CharacterController.center = newCenter;

    }


} 


