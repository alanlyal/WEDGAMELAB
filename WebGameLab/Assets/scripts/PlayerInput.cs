
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
    private float camXRotation;//new

    private void OnValidate()
    {
        this.ValidateRefs();
    }
    void Start()
    {
       move = InputSystem.actions.FindAction("Player/Move");
       look = InputSystem.actions.FindAction("Player/Look");
        Cursor.lockState = CursorLockMode.Locked;
       
    }
    void Update()
    {
       Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();
        //movement of player
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        controller.Move(movement * maxSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        // controller.Move(velocity * Time.deltaTime);
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.SimpleMove(velocity);
        //rotation of player
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);// rotates based off mouse
        //rotate the camera
        //mouseSensY = mouseSensY * readLook.y;
        //mouseSensY = Mathf.Clamp(mouseSensY, -90f, 90f);
        //cam.gameObject.transform.localRotation = Quaternion.Euler(mouseSensY * readLook.y, 0, 0);
        //new stuff from jan 30th
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1;//new
        camXRotation = Mathf.Clamp(camXRotation, -90f, 90f);//new
        cam.gameObject.transform.localRotation= Quaternion.Euler(camXRotation,0,0); //new
    }
}

