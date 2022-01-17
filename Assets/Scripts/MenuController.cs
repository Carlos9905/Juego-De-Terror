using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    //bool isPausa;
    public GameObject Menu;
    public GameObject zoomBtn;
    public GameObject pausaBtn;
    public GameObject linternaBtn;
    public ZoomController camara;
    
    //Para saber si agarro la linterna o no
    [HideInInspector]public bool getLinterna;

    private void Start() {        
        getLinterna = false;
        Menu.SetActive(false);        
    }
    public void PausaMenu(){
        Time.timeScale = 0;
        zoomBtn.SetActive(false);
        pausaBtn.SetActive(false);
        linternaBtn.SetActive(false);
        camara.enabled = false;
        Menu.SetActive(true);
    }
    public void Resumen(){
        Time.timeScale = 1;
        zoomBtn.SetActive(true);
        pausaBtn.SetActive(true);        
        camara.enabled = true;
        Menu.SetActive(false);
        //########## Si no a tomado la linterna entoces no se puede mostrar el boton ###### //
        if(getLinterna) linternaBtn.SetActive(true);
        else linternaBtn.SetActive(false);      
    }
    public void Opciones(){

    }
}
