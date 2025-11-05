using UnityEngine;

public class Movement : MonoBehaviour
{
    enum TroopState
    {
        None, Selected
    }
    private TroopState state;

    private Vector2 target;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Event e = Event.current;
        switch (state)
        {
            case TroopState.None:
                transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * speed);
                break;
            case TroopState.Selected:
                if (e.button == 0)
                {
                    target = e.mousePosition;
                    state = TroopState.None;
                }
                break;
        }

    }

    private void OnMouseDown()
    {
        UnityEngine.Debug.Log("I got Clicked");
        if (state != TroopState.Selected)
        {
            state = TroopState.Selected;
        }
    }

    private void OnMouseOver()
    {
        
    }
}
