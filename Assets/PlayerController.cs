using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private InputSystem_Actions playerControls;

    private void Awake()
    {
        playerControls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float jump = playerControls.Player.Jump.ReadValue<float>();
        Debug.Log(jump);
    }
}
