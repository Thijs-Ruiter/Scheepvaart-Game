using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private InputSystem_Actions playerControls;
    public bool touchedGround = false;
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public SidescrollerController sidescrollerController;

    public enum State 
    {
        Running, Paused
    }

    public State playerState = State.Paused;

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
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerState)
        {
            case State.Running:
                Running();
                break;
            case State.Paused:
                if (sidescrollerController.gameState == SidescrollerController.GameState.Running)
                {
                    playerState = State.Running;
                }
                break;
        }
    }

    void Running()
    {
        float jump = playerControls.Player.Jump.ReadValue<float>();
        //float crouch = playerControls.Player.Crouch.ReadValue<float>();
        //float horizontal = playerControls.Player.Move.ReadValue<Vector2>().x;
        transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        if (jump > 0 && touchedGround)
        {
            touchedGround = false;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided!");
        if (collision.gameObject.tag == "Ground")
        {
            touchedGround = true;
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            sidescrollerController.gameState = SidescrollerController.GameState.Lost;
            sidescrollerController.OnLose();
            playerState = State.Paused;
        }
    }
}
