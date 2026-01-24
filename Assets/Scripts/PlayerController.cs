using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private InputSystem_Actions playerControls;
    public bool touchedGround = false;
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
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
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float jump = playerControls.Player.Jump.ReadValue<float>();
        float horizontal = playerControls.Player.Move.ReadValue<Vector2>().x;
        transform.position = transform.position + new Vector3(horizontal * speed * Time.deltaTime, 0, 0);
        switch (playerState)
        {
            case State.Grounded:
                if (jump > 0)
                {
                    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided!");
        if (collision.gameObject.tag == "Ground")
        {
            touchedGround = true;
        }
    }
}
