using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScaling : MonoBehaviour
{
    public GameObject xArrow; // Reference to the arrow for the X-axis
    public GameObject yArrow; // Reference to the arrow for the Y-axis
    public GameObject zArrow; // Reference to the arrow for the Z-axis

    private bool canScale = false;
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
            //Debug.Log("Touch phase: " + touch.phase);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            // Touched the cube, enable scaling
                            canScale = true;
                            touchStartPosition = touch.position;
                        }
                        else
                        {
                            // Touched somewhere else, disable scaling
                            canScale = false;
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    if (canScale)
                    {
                        float scaleFactor = 3f;
                        Vector2 touchDelta = touch.deltaPosition;

                        // Scale the cube
                        float scaleAmount = Mathf.Max(touchDelta.x, touchDelta.y) * scaleFactor;
                        Vector3 newScale = transform.localScale + new Vector3(scaleAmount, scaleAmount, scaleAmount);

                        // Apply locking on each axis
                        if (!lockX)
                            newScale.x = transform.localScale.x;
                        if (!lockY)
                            newScale.y = transform.localScale.y;
                        if (!lockZ)
                            newScale.z = transform.localScale.z;

                        // Directly apply the new scale to the transform
                        transform.localScale = newScale;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    canScale = false;
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