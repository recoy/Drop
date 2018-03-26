using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour {

    public PlayerController playerController;

    public bool tap, touch, swipeLeft, swipeRight, swipeUp, swipeDown;
    public bool isDraging = false;
    public Vector2 startTouch, swipeDelta;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Standalone
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("mouse down");
            //tap = true;
            touch = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }

        //else if(Input.GetMouseButtonUp(0))
        //{
        //    Debug.Log("mouse up");
        //    isDraging = false;
        //    Reset();
        //}
        #endregion

        #region Mobile
        //if(Input.touches.Length > 0)
        //{
        //    if(Input.touches[0].phase == TouchPhase.Began)
        //    {
        //        //tap = true;
        //        Debug.Log("touch began");
        //        touch = true;
        //        isDraging = true;
        //        startTouch = Input.touches[0].position;
        //    }
        //    else if(Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
        //    {
        //        Debug.Log("touch end");
        //        //tap = true;
        //        isDraging = false;
        //        Reset();
        //    }
        //}

        #endregion

        swipeDelta = Vector2.zero;
        if(isDraging)
        {
            
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
            //Debug.Log(swipeDelta.magnitude);

        }
        if(swipeDelta.magnitude > 125)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    swipeLeft = true;
                    //Debug.Log("swipe left");
                }
                else
                {
                    swipeRight = true;
                    //Debug.Log("swipe right");
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                    //Debug.Log("swipe down");
                }
                else
                {
                    swipeUp = true;
                    //Debug.Log("swipe up");
                }
            }

            Reset();
        }
        //if(Input.GetMouseButtonUp(0) && swipeDelta.magnitude < 100)
        //{
        //    tap = true;
        //    Debug.Log("tap");
        //}

        else if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("mouse up");
            isDraging = false;
            Reset();
        }
    }

    private void Reset()
    {
        startTouch = Vector2.zero;
        swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
