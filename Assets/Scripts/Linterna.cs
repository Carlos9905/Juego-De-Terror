using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    private bool isOn = false;
    public new Light light;
    
    private void Start() {
        light = GetComponent<Light>();
    }
    public void ChangeLigthState(){
        isOn = !isOn;
    }
    private void Update() {
        if(isOn) light.enabled = true;
        else light.enabled = false;
    }
}
