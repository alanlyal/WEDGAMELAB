using UnityEngine;
using UnityEngine.InputSystem;
using KBCore.Refs;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    private InputAction jump;

    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -5.0f;
    private Vector3 velocity;

    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;
    [SerializeField] private float mouseSensY = 5.0f;

    [SerializeField] private AudioController audioController;

    private float camXRotation;

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

        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        controller.Move(movement * maxSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);

        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1;
        camXRotation = Mathf.Clamp(camXRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    public void ChangeMouseSensibility(float value)
    {
        mouseSensY = value;
        rotationSpeed = value;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (EventChannelManager.Instance != null && EventChannelManager.Instance.voidEvent != null)
            EventChannelManager.Instance.voidEvent.RaiseEvent();

        if (SaveLoadSystem.Instance != null && SaveLoadSystem.Instance.gameData != null && EventChannelManager.Instance.gameDataEvent != null)
            EventChannelManager.Instance.gameDataEvent.RaiseEvent(SaveLoadSystem.Instance.gameData);

        if (controller != null && controller.isGrounded)
        {
            velocity.y = 5f;
        }
    }
}