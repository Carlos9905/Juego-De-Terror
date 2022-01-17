using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectController : MonoBehaviour
{    
    [Header("Ajustes")]
    [SerializeField]private float distancia;
    [Header("Referencias")]
    [SerializeField] private GameObject linternaBtn;
    [SerializeField] private GameObject hud;

    // Solo objetos interactivos
    LayerMask mask;
    
    private void Start() {
        mask = LayerMask.GetMask("Objetos");
        linternaBtn.SetActive(false);
    }
    private void Update() { 
        // Deteccion de todos los toques en pantalla 
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            //Faces de los toques
            switch(touch.phase){
                //Cuando se comienza a tocar la pantalla
                case TouchPhase.Began:
                    //Aqui va todo lo relacionado a la interaccion de los objetos en escena
                    Ray ray = Camera.main.ScreenPointToRay(touch.position); // Desce la posicion de toque
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, distancia, mask, QueryTriggerInteraction.Collide)){
                        //Para cada objeto interactivo                                                
                        switch(hit.collider.tag){
                            //Puertas de la morgue
                            case "DoorMorgue":
                                hit.collider.transform.GetComponent<OpenDoor>().ChangeDoorState();
                            break;
                            //Puertas de limpierza
                            case "DoorHospital":
                                hit.collider.transform.GetComponent<OpenDoor>().ChangeDoorState();
                            break;
                            //Linterna
                            case "Linterna":
                                Destroy(hit.collider.gameObject);                                
                                linternaBtn.SetActive(true);
                                hud.transform.GetComponent<MenuController>().getLinterna = true;
                            break;
                            //Llave

                        }                            
                    }
                break;
                case TouchPhase.Moved:                
                break;
            }
        }        
    }
}
