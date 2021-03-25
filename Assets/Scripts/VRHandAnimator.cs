using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandAnimator : MonoBehaviour
{
    private VRInput controller;
    public Animator handAnimator;
    // Awake is called before the Start method
    void Awake()
    {
        controller = GetComponent<VRInput>();
        handAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller)
        {
            handAnimator.Play("HandClosing", 0, controller.gripValue);
        }
    }
}
