using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public GameObject xArrow; // Reference to the arrow for the X-axis
    public GameObject yArrow; // Reference to the arrow for the Y-axis
    public GameObject zArrow; // Reference to the arrow for the Z-axis
    private bool canMove = false;
    private bool lockX = false;
    private bool lockY = false;
    private bool lockZ = false;
    private Vector2 touchStartPosition;

    void Start()
    {
        // All arrows are active by default
        ToggleArrow(xArrow, true);
        ToggleArrow(yArrow, true);
        ToggleArrow(zArrow, true);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            // Touched the cube, enable movement
                            canMove = true;
                            touchStartPosition = touch.position;
                        }
                        else
                        {
                            // Touched somewhere else, disable movement
                            canMove = false;
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    if (canMove)
                    {
                        float moveSpeed = 0.05f;
                        Vector2 touchDelta = touch.position - touchStartPosition;

                        // Move the cube
                        if (!lockX)
                            transform.Translate(touchDelta.x * moveSpeed * Time.deltaTime, 0, 0);
                        if (!lockY)
                            transform.Translate(0, touchDelta.y * moveSpeed * Time.deltaTime, 0);
                        if (!lockZ)
                            transform.Translate(0, 0, touchDelta.y * moveSpeed * Time.deltaTime);
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    canMove = false;
                    break;
            }
        }
    }

     public void ToggleLockX()
    {
        lockX = !lockX;
        ToggleArrow(xArrow, !lockX); // Enable X arrow if X-axis is unlocked, disable if locked
    }

    public void ToggleLockY()
    {
        lockY = !lockY;
        ToggleArrow(yArrow, !lockY); // Enable Y arrow if Y-axis is unlocked, disable if locked
    }

    public void ToggleLockZ()
    {
        lockZ = !lockZ;
        ToggleArrow(zArrow, !lockZ); // Enable Z arrow if Z-axis is unlocked, disable if locked
    }

    void ToggleArrow(GameObject arrow, bool state)
    {
        arrow.SetActive(state); // Enable or disable arrow GameObject
    }
}











/* public class Movement : MonoBehaviour{

    private Touch touch;
    private float speedModifier;

    void Start () {
        speedModifier = 0.02f;
    }

    void Update () {
        if (Input.touchCount > 0){
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved){
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedModifier,
                    transform.position.y,
                    transform.position.z + touch.deltaPosition.y * speedModifier);
            }
        }
    }
} */