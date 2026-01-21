using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private InputSystem_Actions playerControls;
    public bool touchedGround = false;
    public enum State 
    {
        Grounded, Aired
    }

    public State playerState = State.Grounded;

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
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float jump = playerControls.Player.Jump.ReadValue<float>();
        switch (playerState)
        {
            case State.Grounded:
                if (jump > 0)
                {
                    playerState = State.Aired;
                }
                break;
            case State.Aired:
                if (touchedGround)
                {
                    touchedGround = false;
                    playerState = State.Grounded;
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            touchedGround = true;
        }
    }
}
