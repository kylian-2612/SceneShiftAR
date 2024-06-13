 using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;

public enum TransformMode
{
    Move,
    Rotate,
    Scale
}

public class UIManager : MonoBehaviour
{
    public TransformMode currentMode = TransformMode.Move;
    public GameObject uiPanel;
    public GameObject exportAlertPanel;
    public GameObject alertTextObject;
    public Transform spawnPoint;

    private GameObject currentAsset;
    private TMP_Text alertText;

    void Start()
    {
        uiPanel.SetActive(false);
        exportAlertPanel.SetActive(false);

        if (alertTextObject != null)
        {
            alertText = alertTextObject.GetComponent<TMP_Text>();
            if (alertText == null)
            {
                Debug.LogError("Alert TMP_Text component is missing on alertTextObject.");
            }
            else
            {
                Debug.Log("Alert TMP_Text component found and initialized.");
            }
        }
        else
        {
            Debug.LogError("alertTextObject is not assigned in the Inspector.");
        }

        if (exportAlertPanel == null)
        {
            Debug.LogError("exportAlertPanel is not assigned in the Inspector.");
        }
    }

    public void SetCurrentAsset(GameObject asset)
    {
        if (currentAsset != null)
        {
            DisableCurrentMode();
        }

        currentAsset = asset;

        // Enable movement mode to show movement arrows immediately
        EnableMovementMode();
    }

    public void EnableMovementMode()
    {
        currentMode = TransformMode.Move;
        EnableMode();
    }

    public void EnableScalingMode()
    {
        currentMode = TransformMode.Scale;
        EnableMode();
    }

    public void EnableRotationMode()
    {
        currentMode = TransformMode.Rotate;
        EnableMode();
    }

    private void EnableMode()
    {
        if (currentAsset == null) return;

        CubeMovement cubeMovement = currentAsset.GetComponent<CubeMovement>();
        CubeScaling cubeScaling = currentAsset.GetComponent<CubeScaling>();
        CubeRotation cubeRotation = currentAsset.GetComponent<CubeRotation>();

        cubeMovement.ToggleMovement(currentMode == TransformMode.Move);
        cubeScaling.ToggleScaling(currentMode == TransformMode.Scale);
        cubeRotation.ToggleRotation(currentMode == TransformMode.Rotate);

        // Toggle arrows based on current mode
        ToggleArrows(currentAsset, true);
    }

    private void DisableCurrentMode()
    {
        if (currentAsset == null) return;

        CubeMovement cubeMovement = currentAsset.GetComponent<CubeMovement>();
        CubeScaling cubeScaling = currentAsset.GetComponent<CubeScaling>();
        CubeRotation cubeRotation = currentAsset.GetComponent<CubeRotation>();

        cubeMovement.ToggleMovement(false);
        cubeScaling.ToggleScaling(false);
        cubeRotation.ToggleRotation(false);

        // Disable all arrows
        ToggleArrows(currentAsset, false);
    }

    private void ToggleArrows(GameObject asset, bool active)
    {
        if (asset == null) return;

        CubeMovement cubeMovement = asset.GetComponent<CubeMovement>();
        CubeScaling cubeScaling = asset.GetComponent<CubeScaling>();
        CubeRotation cubeRotation = asset.GetComponent<CubeRotation>();

        bool moveMode = currentMode == TransformMode.Move;
        bool rotateMode = currentMode == TransformMode.Rotate;
        bool scaleMode = currentMode == TransformMode.Scale;

        // Toggle arrows based on current mode
        cubeMovement.xArrow.SetActive(active && moveMode && !cubeMovement.IsXLocked());
        cubeMovement.yArrow.SetActive(active && moveMode && !cubeMovement.IsYLocked());
        cubeMovement.zArrow.SetActive(active && moveMode && !cubeMovement.IsZLocked());

        cubeScaling.xArrow.SetActive(active && scaleMode && !cubeScaling.IsXLocked());
        cubeScaling.yArrow.SetActive(active && scaleMode && !cubeScaling.IsYLocked());
        cubeScaling.zArrow.SetActive(active && scaleMode && !cubeScaling.IsZLocked());

        cubeRotation.xArrow.SetActive(active && rotateMode && !cubeRotation.IsXLocked());
        cubeRotation.yArrow.SetActive(active && rotateMode && !cubeRotation.IsYLocked());
        cubeRotation.zArrow.SetActive(active && rotateMode && !cubeRotation.IsZLocked());
    }

    public void ToggleLockX()
    {
        if (currentAsset == null) return;

        CubeMovement cubeMovement = currentAsset.GetComponent<CubeMovement>();
        CubeScaling cubeScaling = currentAsset.GetComponent<CubeScaling>();
        CubeRotation cubeRotation = currentAsset.GetComponent<CubeRotation>();

        cubeMovement.ToggleLockX();
        cubeScaling.ToggleLockX();
        cubeRotation.ToggleLockX();

        // Toggle arrows based on new lock status
        ToggleArrows(currentAsset, true);
    }

    public void ToggleLockY()
    {
        if (currentAsset == null) return;

        CubeMovement cubeMovement = currentAsset.GetComponent<CubeMovement>();
        CubeScaling cubeScaling = currentAsset.GetComponent<CubeScaling>();
        CubeRotation cubeRotation = currentAsset.GetComponent<CubeRotation>();

        cubeMovement.ToggleLockY();
        cubeScaling.ToggleLockY();
        cubeRotation.ToggleLockY();

        // Toggle arrows based on new lock status
        ToggleArrows(currentAsset, true);
    }

    public void ToggleLockZ()
    {
        if (currentAsset == null) return;

        CubeMovement cubeMovement = currentAsset.GetComponent<CubeMovement>();
        CubeScaling cubeScaling = currentAsset.GetComponent<CubeScaling>();
        CubeRotation cubeRotation = currentAsset.GetComponent<CubeRotation>();

        cubeMovement.ToggleLockZ();
        cubeScaling.ToggleLockZ();
        cubeRotation.ToggleLockZ();

        // Toggle arrows based on new lock status
        ToggleArrows(currentAsset, true);
    }

    public void ToggleUIPanel()
    {
        uiPanel.SetActive(!uiPanel.activeSelf);
    }

    public void ExportTransformations()
    {
        List<GameObject> placedAssets = new List<GameObject>();
        foreach (Transform child in spawnPoint)
        {
            placedAssets.Add(child.gameObject);
        }

        string fileName = "Transformations.txt";
        string filePath = Path.Combine(Application.temporaryCachePath, fileName);
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (GameObject asset in placedAssets)
            {
                Vector3 position = asset.transform.localPosition;
                Vector3 rotation = asset.transform.localEulerAngles;
                Vector3 scale = asset.transform.localScale;
                writer.WriteLine($"{asset.name}: Position({position.x}, {position.y}, {position.z}), Rotation({rotation.x}, {rotation.y}, {rotation.z}), Scale({scale.x}, {scale.y}, {scale.z})");
            }
        }

        Application.OpenURL(filePath);

        // Display an alert with the file path
        ShowExportAlert($"Transformations saved successfully. Please check your Downloads folder.");
    }

    // Method to show an export alert
    private void ShowExportAlert(string message)
    {
        if (alertText == null)
        {
            Debug.LogError("alertText is not initialized.");
            return;
        }
        alertText.text = message;
        exportAlertPanel.SetActive(true);

        // Hide the alert after a few seconds
        Invoke("HideExportAlert", 10f);
    }

    // Method to hide the export alert
    private void HideExportAlert()
    {
        exportAlertPanel.SetActive(false);
    }
}
