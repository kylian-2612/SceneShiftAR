using UnityEngine;

public class CubeScaling : MonoBehaviour
{
    public GameObject xArrow;
    public GameObject yArrow;
    public GameObject zArrow;
    private bool canScale = false;
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
        if (!canScale || Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStartPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Vector2 delta = touch.position - touchStartPosition;
            ScaleCube(delta);
            touchStartPosition = touch.position;
        }
    }

    void ScaleCube(Vector2 delta)
    {
        float scaleAmount = (delta.x + delta.y) * 0.15f;
        Vector3 scaleDelta = Vector3.one * scaleAmount * Time.deltaTime;

        if (lockX) scaleDelta.x = 0;
        if (lockY) scaleDelta.y = 0;
        if (lockZ) scaleDelta.z = 0;

        transform.localScale += scaleDelta;
    }

    void ToggleArrow(GameObject arrow, bool active)
    {
        arrow.SetActive(active);
    }

    public void ToggleScaling(bool enable)
    {
        canScale = enable;
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
