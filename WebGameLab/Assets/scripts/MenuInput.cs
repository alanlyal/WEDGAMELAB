using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private InputAction openMenu;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private bool isMenuOpen;
    
    void Start()
    {
        openMenu = InputSystem.actions.FindAction("UI/Menu");
        openMenu.started += toggleMenu;
        
    }
    private void toggleMenu(InputAction.CallbackContext context) 
    {
        
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);
        if (isMenuOpen)
        {
            GetComponent<PlayerInput>().enabled = false;
            InputSystem.actions.FindActionMap("Player").Disable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Open menu with p");
        }
        else
        {
            GetComponent<PlayerInput>().enabled = true;
            InputSystem.actions.FindActionMap("Player").Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("closed menu with p");
        }
    }
    private void OnDisable()
    {
        openMenu.started -= toggleMenu;
        
    }
   
   
}
