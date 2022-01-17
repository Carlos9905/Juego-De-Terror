using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour{
    public int numeroDeScene;
    public Image pantallaNegra;
    public new Light light;
    private void Start() {
        light.enabled = false;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            StartCoroutine("Cargar");
        }
    }
    public IEnumerator Cargar(){   
        light.enabled = false;     
        pantallaNegra.enabled = true;
        yield return new WaitForSeconds(1.5f);   
        AsyncOperation cargando = SceneManager.LoadSceneAsync(numeroDeScene, LoadSceneMode.Single);          
        /*while(!cargando.isDone){
            //
            
        }*/
    }
}
