using UnityEngine;

public class Movement : MonoBehaviour
{
    enum TroopState
    {
        None, Selected
    }
    private TroopState state;
    private Event e = Event.current;

    //private Vector2 target = transform.position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case TroopState.None:
                break;
            case TroopState.Selected:
                if (e.button == 0)
                {
                    
                    state = TroopState.None;
                }
                break;
        }

    }

    private void OnMouseDown()
    {
        if (state != TroopState.Selected)
        {
            state = TroopState.Selected;
        }
    }
}
