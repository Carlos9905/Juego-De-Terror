using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CameraBobbing))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CharacterController characterController;    

    [Header("Ajustes del jugador")]
    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveInputDeadZone;

    // deteccion tactil
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth;

    // Control de camara
    private Vector2 lookInput;
    private float cameraPitch;

    // Movimiento del personaje
    private Vector2 moveTouchStartPosition;
    private Vector2 moveInput;

    //Simulacion de movimiento de caminar
    private CameraBobbing cb;
    //private CameraAminController ca;
    
    void Start()
    {
        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // Divicion de pantalla
        halfScreenWidth = Screen.width / 2;

        // calcula la zona muerta con respecto a la altura de la pantalla
        moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);

        //Componentes        
        cb = GetComponent<CameraBobbing>();        
    }
    
    void Update()
    {
        // Input
        GetTouchInput();

        if (rightFingerId != -1) {
            // Si es el dedo derecho rota        
            LookAround();
        }

        if (leftFingerId != -1)
        {
            // Si es el dedo derecho se mueve
            Move();
        }
        else
        {
            //Ninguno de los dos entonces no se esta moviendo
            cb.isWalking = false;
        }
    }

    void GetTouchInput() {
        // Interaccion de todos los toques en pantalla
        #region Deteccion Tactil
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Faces del Touch
            switch (t.phase)
            {
                //Comienzo
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerId == -1)
                    {
                        // Start tracking the left finger if it was not previously being tracked
                        leftFingerId = t.fingerId;

                        // Set the start position for the movement control finger
                        moveTouchStartPosition = t.position;
                    }
                    else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                // Suelta el dedo de la pantalla
                case TouchPhase.Ended:
                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        moveInput.Normalize();                        
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        //ca.movC = false;                                         
                    }
                    break;
                // El sistema cancela el touch
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerId)
                    {
                        // Stop tracking the left finger
                        leftFingerId = -1;
                        moveInput.Normalize();                                             
                    }
                    else if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        //ca.movC = false;                                     
                    }

                    break;
                // Moviendo el dedo en pantalla
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                        //ca.movC = true;                        
                    }
                    else if (t.fingerId == leftFingerId) {
                        // calculating the position delta from the start position
                        moveInput = t.position - moveTouchStartPosition;                        
                    }

                    break;
                // Precionando el dedo en un solo lugar de la pantalla
                case TouchPhase.Stationary:
                    // Recetea el valor del moviento de la camara con respecto al dedo
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
        #endregion
    }

    #region Movimiento y rotacion
    void LookAround() {
        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move() {

        // No se mueve si la distancia del input es menor a la zana muerta     
        if (moveInput.sqrMagnitude <= moveInputDeadZone) {
            cb.isWalking = false;
            //ca.isWalking = false;         
            return;
        }

        cb.isWalking = true;
        //ca.isWalking = true;

        // Si es mayor entonces si se puede mover
        Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime; // Direccion donde se dirije el personaje
        // Se mueve en relacion a la posision local del Player
        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
    }

    #endregion

}