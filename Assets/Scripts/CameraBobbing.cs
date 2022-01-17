using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobbing : MonoBehaviour
{
    [Header("Referencias")]
    public Transform headTransform;
    public Transform CameraTransform;

    [Header("Rebotes de cabeza")]
    public float bobFrecuency = 5f;
    public float bobHorizontalAmplitud = 0.1f;
    public float bobVerticalAmplitud = 0.1f;
    [Range(0,1)] public float headBobSmoothing = 0.1f;

    //Estado
    public bool isWalking;
    private float walkTime;
    private Vector3 targetCameraPosition;      
    
    void Update()
    {
        if(!isWalking) walkTime = 0;
        else walkTime += Time.deltaTime;

        targetCameraPosition = headTransform.position + CalculateHeadBobOffSet(walkTime);

        CameraTransform.position = Vector3.Lerp(CameraTransform.position, targetCameraPosition, headBobSmoothing);

        if((CameraTransform.position - targetCameraPosition).magnitude <= 0.001) {
            CameraTransform.position = targetCameraPosition;
        }       
    }

    //Calculo de rebotes
    private Vector3 CalculateHeadBobOffSet( float t){
        float horizontalOffSet = 0f;
        float verticalOffSet = 0f;

        Vector3 offSet = Vector3.zero;

        if(t > 0){
            horizontalOffSet = Mathf.Cos(t * bobFrecuency) * bobHorizontalAmplitud;
            verticalOffSet = Mathf.Sin(t * bobFrecuency *2) * bobVerticalAmplitud;

            offSet = headTransform.right * horizontalOffSet + headTransform.up * verticalOffSet;
        }
        return offSet;
    }
}
