using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{    
    [Header("Ajustes")]
    public bool doorOpen;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0.0f;
    [Tooltip("Entre mas alto mas rapido")]
    public float smooth = 3.0f; // Velocidad de cierre de puerta mientas mas alta sea el valor mas rapido se va abrir
    [Tooltip("la puerta se abre una sola vez")]    
    public bool unloked;    
    // Sonidos
    /*[Header("Sonidos")]
    public AudioClip openDoorSound;
    public AudioClip closeDoorSound;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "DoorSound_TG") AudioSource.PlayClipAtPoint(closeDoorSound, transform.position, 1);
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "DoorSound_TG") AudioSource.PlayClipAtPoint(openDoorSound, transform.position, 1);
    }*/
    public void ChangeDoorState(){
        doorOpen = !doorOpen;
    }
    private void Update() {
        if(doorOpen){
            Quaternion targetRotation = Quaternion.Euler(-90f, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }else {
            Quaternion targetRotation2 = Quaternion.Euler(-90f, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
        }
    }

}
