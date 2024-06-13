using UnityEngine;

public class SelectableItem : MonoBehaviour
{
    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        // This item was tapped, make it the current asset
                        uiManager.SetCurrentAsset(gameObject);
                    }
                }
            }
        }
    }
}
