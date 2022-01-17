using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAminController : MonoBehaviour
{
    [HideInInspector] public bool isWalking;
    [HideInInspector] public bool movC;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if(isWalking) animator.SetBool("walk", true);            
        else animator.SetBool("walk", false);    
            
        if(movC) animator.SetBool("movC", true);
        else animator.SetBool("movC", false);
    }
}
