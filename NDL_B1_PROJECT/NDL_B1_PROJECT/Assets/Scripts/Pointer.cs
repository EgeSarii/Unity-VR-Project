using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_Dot;
    public VRInputModule m_InputModule;

    private LineRenderer m_LineRenderer = null;

   private void Awake()
   {
       m_LineRenderer = GetComponent<LineRenderer>();
   }

    // Update is called once per frame
    private void Update()
    {
        float targetLength = m_DefaultLength;

        RaycastHit hit = CreateRayCast(targetLength);

        Vector3 endPosition = transform.position + (transform.forward * targetLength);
        
        if(hit.collider != null)
        {
            endPosition = hit.point;
        }

        m_Dot.transform.position = endPosition;

        m_LineRenderer.SetPosition (0,transform.position);
        m_LineRenderer.SetPosition (1, endPosition);

        bool state = SteamVR_Actions.default_InteractUI.state; //get if pressed

        if(state && hit.collider)
        {
            Debug.Log("Yes I have been summoned by: " + hit.collider.name);
            Button b;
            b = hit.collider.GetComponent<Button>();
            Bow bow;
            bow = hit.collider.GetComponent<Bow>();
            if(bow)
                b.onClick.Invoke();
        }

    }

    private void Updateline()
    {
        
    }


    private RaycastHit CreateRayCast (float length)
    {
        RaycastHit hit;

        Ray ray = new Ray (transform.position, transform.forward);
        Physics.Raycast( ray, out hit , m_DefaultLength );

        return hit;
    }
}
