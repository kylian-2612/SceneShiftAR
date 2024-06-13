using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public GameObject xArrow;
    public GameObject yArrow;
    public GameObject zArrow;
    public float zMovementScale = 2f;  // Added scaling factor for Z axis movement
    private bool canMove = false;
    private bool lockX = false;
    private bool lockY = false;
    private bool lockZ = false;
    private Vector3 touchStartPosition;
    private Plane movementPlane;

    void Start()
    {
        ToggleArrow(xArrow, false);
        ToggleArrow(yArrow, false);
        ToggleArrow(zArrow, false);
    }

    void Update()
    {
        if (!canMove || Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            movementPlane = new Plane(-Camera.main.transform.forward, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (movementPlane.Raycast(ray, out float enter))
            {
                touchStartPosition = ray.GetPoint(enter);
            }
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (movementPlane.Raycast(ray, out float enter))
            {
                Vector3 touchCurrentPosition = ray.GetPoint(enter);
                Vector3 delta = touchCurrentPosition - touchStartPosition;

                delta.z *= zMovementScale;

                MoveCube(delta);
                touchStartPosition = touchCurrentPosition;
            }
        }
    }

    void MoveCube(Vector3 delta)
    {
        if (lockX) delta.x = 0;
        if (lockY) delta.y = 0;
        if (lockZ) delta.z = 0;

        transform.position += delta;
    }

    void ToggleArrow(GameObject arrow, bool active)
    {
        arrow.SetActive(active);
    }

    public void ToggleMovement(bool enable)
    {
        canMove = enable;
        ToggleArrow(xArrow, enable);
        ToggleArrow(yArrow, enable);
        ToggleArrow(zArrow, enable);
    }

    public void ToggleLockX()
    {
        lockX = !lockX;
    }

    public void ToggleLockY()
    {
        lockY = !lockY;
    }

    public void ToggleLockZ()
    {
        lockZ = !lockZ;
    }

    public bool IsXLocked()
    {
        return lockX;
    }

    public bool IsYLocked()
    {
        return lockY;
    }

    public bool IsZLocked()
    {
        return lockZ;
    }
}

