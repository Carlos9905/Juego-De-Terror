using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{    
    [SerializeField] private float speedZoomIn;
    [SerializeField] private float speedZoomOut;
    [SerializeField] private int zoomIn, zoomOut;
    private bool IsZoom = false;

    private void Start() {
        IsZoom = false;
    }

    private void Update() {
        if(IsZoom ==true) ZoomIn();
        else if (IsZoom == false) ZoomOut();
    }

    public void ZOOMSI(){
        IsZoom = true;
    }
    public void ZOOMNO(){
        IsZoom = false;
    }

    /*Para hacer zoom*/
    public void ZoomIn(){
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomIn,Time.deltaTime * speedZoomIn);
    }
    /*Para quitar el zoom*/
    public void ZoomOut(){
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomOut,Time.deltaTime * speedZoomOut);
    }
}