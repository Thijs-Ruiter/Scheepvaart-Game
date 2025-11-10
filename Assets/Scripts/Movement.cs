using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public InputAction leftClickAction;
    private Camera cam;
    public enum TroopState
    {
        None, Selected
    }
    public TroopState state;

    private Vector2 target;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        leftClickAction = InputSystem.actions.FindAction("Click");
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Event e = Event.current;
        switch (state)
        {
            case TroopState.None:
                if (leftClickAction.WasPressedThisFrame())
                {
                    UnityEngine.Debug.Log("Mouse Pressed");
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            state = TroopState.Selected;
                        }
                    }
                }
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
                break;
            case TroopState.Selected:
                if (Input.GetMouseButtonDown(0))
                {
                    target = Input.mousePosition;
                    state = TroopState.None;
                }
                break;
        }
    }
}
