
using UnityEngine;
using UnityEngine.InputSystem;
using KBCore.Refs;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -5.0f;
    private Vector3 velocity;
    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;
    [SerializeField] private float mouseSensY = 5.0f;
    [SerializeField, Scene] private audioController audioController;
    private InputAction jump;
  
    private float camXRotation;//new

    private void OnValidate()
    {
        this.ValidateRefs();
    }
    void Start()
    {
       move = InputSystem.actions.FindAction("Player/Move");
       look = InputSystem.actions.FindAction("Player/Look");
        jump = InputSystem.actions.FindAction("Player/Jump");
        jump.started += Jump;
        Cursor.lockState = CursorLockMode.Locked;
       
    }
    private void OnDisable()
    {
        jump.started -= Jump;
    }
    void Update()
    {
       Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();
        //movement of player
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        controller.Move(movement * maxSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.SimpleMove(velocity);
        //rotation of player
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);// rotates based off mouse
        
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1;//new
        camXRotation = Mathf.Clamp(camXRotation, -90f, 90f);//new
        cam.gameObject.transform.localRotation= Quaternion.Euler(camXRotation,0,0); //new
        //jump

    }
    public void ChangeMouseSensibility(float value)
    {
        Debug.Log($"value changed {value}");
        mouseSensY = value;
        rotationSpeed = value;
    }
    private void Jump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}

