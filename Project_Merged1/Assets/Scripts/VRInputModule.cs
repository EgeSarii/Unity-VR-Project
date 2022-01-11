using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera m_camera;
    public SteamVR_Input_Sources m_TargetSource;
     public SteamVR_Action_Boolean m_clickAction;

     private GameObject m_currentObject = null;
     private PointerEventData m_data = null;

     protected override void Awake() {
         base.Awake();
         m_data = new PointerEventData(eventSystem);
     }

     public override void Process() {
        //Reset data, set camera
        m_data.Reset();
        m_data.position = new Vector2(m_camera.pixelWidth / 2, m_camera.pixelHeight / 2);
     
        //Raycast
        eventSystem.RaycastAll(m_data, m_RaycastResultCache);
        m_data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_currentObject = m_data.pointerCurrentRaycast.gameObject;

        //Clear
        m_RaycastResultCache.Clear();

        //Hover
        HandlePointerExitAndEnter(m_data, m_currentObject);

        //Press
        if (m_clickAction.GetStateDown(m_TargetSource)) {
            processPress(m_data);
        }

        //Release
        if (m_clickAction.GetStateUp(m_TargetSource)){
            processRelease(m_data);
        }
     }

     public PointerEventData getData(){
            return m_data;
     }

     private void processPress (PointerEventData data){

     }
     private void processRelease (PointerEventData data){
         
     }
}
