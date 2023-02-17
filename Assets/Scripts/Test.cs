using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Test : MonoBehaviour 
{
    
    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;

    private bool stopTouch = false;
    
    public Text _text;
    public float swipeRange;
    public float tapRange;

    private void Update() {
        Swipe();
    }

    private void Swipe()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 distance = currentPosition - startTouchPosition;

            if(!stopTouch)
            {
                if(distance.x < -swipeRange)
                {
                    _text.text = "Left";
                    stopTouch = true;
                }
                else if(distance.x > swipeRange)
                {
                    _text.text = "Right";
                    stopTouch = true;
                }
                else if(distance.y > swipeRange)
                {
                    _text.text = "Up";
                    stopTouch = true;
                }
                else if(distance.y < -swipeRange)
                {
                    _text.text = "Down";
                    stopTouch = true;
                }

            }
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 distance = endTouchPosition - startTouchPosition;
            if(Mathf.Abs(distance.x) < tapRange && Mathf.Abs(distance.y) < tapRange)
            {
                _text.text = "Tap";
            }
        }
    }
}