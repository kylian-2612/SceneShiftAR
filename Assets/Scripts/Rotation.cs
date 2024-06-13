using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public GameObject xArrow;
    public GameObject yArrow;
    public GameObject zArrow;
    private bool canRotate = false;
    private bool lockX = false;
    private bool lockY = false;
    private bool lockZ = false;
    private Vector2 touchStartPosition;

    void Start()
    {
        ToggleArrow(xArrow, false);
        ToggleArrow(yArrow, false);
        ToggleArrow(zArrow, false);
    }

    void Update()
    {
        if (!canRotate || Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStartPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Vector2 delta = touch.position - touchStartPosition;
            RotateCube(delta);
            touchStartPosition = touch.position;
        }
    }

    void RotateCube(Vector2 delta)
    {
        float rotateAmount = (delta.x + delta.y) * 2f;
        Vector3 rotateDelta = new Vector3(lockX ? 0 : rotateAmount, lockY ? 0 : rotateAmount, lockZ ? 0 : rotateAmount) * Time.deltaTime;

        if (lockX) rotateDelta.x = 0;
        if (lockY) rotateDelta.y = 0;
        if (lockZ) rotateDelta.z = 0;

        transform.Rotate(rotateDelta);
    }

    void ToggleArrow(GameObject arrow, bool active)
    {
        arrow.SetActive(active);
    }

    public void ToggleRotation(bool enable)
    {
        canRotate = enable;
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
