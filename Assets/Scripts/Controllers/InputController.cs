using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    public UnityEvent<Vector2> OnTouchDown;
    public UnityEvent<Vector2> OnTouch;
    public UnityEvent<Vector2> OnTouchUp;

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touches.Length > 0)
            {
                switch (Input.touches[0].phase)
                {
                    case TouchPhase.Began:
                        if(OnTouchDown != null)
                            OnTouchDown.Invoke(Input.touches[0].position);
                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        if(OnTouch != null)
                            OnTouch.Invoke(Input.touches[0].position);
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if(OnTouchUp != null)
                            OnTouchUp.Invoke(Input.touches[0].position);
                        break;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (OnTouchDown != null)
                    OnTouchDown.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                if (OnTouch != null)
                    OnTouch.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (OnTouchUp != null)
                    OnTouchUp.Invoke(Input.mousePosition);
            }
        }
    }
}
