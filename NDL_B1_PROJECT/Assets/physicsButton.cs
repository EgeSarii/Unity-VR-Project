using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class physicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = .2f;
    [SerializeField] private float deadzone = .025f;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;
    public UnityEvent onPressed, onReleased;
    // Start is called before the first frame update

    void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();

    }

    private float getValue () {
        var value = Vector3.Distance(startPos,transform.localPosition) / joint.linearLimit.limit;
        if (Math.Abs(value) < deadzone)
            value = 0;
        return Mathf.Clamp(value, -1f, 1f);
    }
    // Update is called once per frame
    void Update()
    {
        if ( !isPressed && getValue() + threshold >= 1  )
            pressed();
        if ( isPressed && getValue() + threshold <= 0  )
            released();
    }

    private void pressed() {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed!");
    }

    private void released (){
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released!");

    }
}
