using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public InputAction leftClickAction;
    public Camera cam;
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
    void FixedUpdate()
    {
        Event e = Event.current;
        switch (state)
        {
            case TroopState.None:
                if (leftClickAction.WasPressedThisFrame())
                {
                    RaycastHit hit;
                    Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
                    Debug.DrawRay(ray.origin, ray.direction * 15);
                    if (Physics.Raycast(ray, out hit, 15))
                    {
                        UnityEngine.Debug.Log(hit.collider.name);
                        if (hit.collider.gameObject == gameObject)
                        {
                            state = TroopState.Selected;
                        }
                    }
                }
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
                break;
            case TroopState.Selected:
                if (leftClickAction.WasPressedThisFrame())
                {
                    target = Mouse.current.position.ReadValue();
                    state = TroopState.None;
                }
                break;
        }
    }
}
