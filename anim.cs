using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        this.m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Press the up arrow button to reset the trigger and set another one
        if (Input.GetKey(KeyCode.UpArrow))
        {
            print("UpArrow");
            //Reset the "Crouch" trigger
            // m_Animator.ResetTrigger("Walking");

            //Send the message to the Animator to activate the trigger parameter named "Jump"
            m_Animator.SetTrigger("Walking");
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            // print("DownArrow");
            // Reset the "Jump" trigger
            // m_Animator.ResetTrigger("Idle");

            //Send the message to the Animator to activate the trigger parameter named "Crouch"
            // m_Animator.SetTrigger("Walking");
            // m_Animator.ResetTrigger("Walking");
        }
    }
}
