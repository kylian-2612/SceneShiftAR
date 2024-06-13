using UnityEngine;
using UnityEngine.UI;

public class AssetLibrary : MonoBehaviour
{
    public GameObject[] assets;
    public GameObject uiPanel;
    public GameObject gridContainer;
    public Transform spawnPoint;
    public int numColumns = 3;
    public float spacingBetweenButtons = 10f;
    public UIManager uiManager;
    public GameObject assetButtonPrefab;
    public GameObject moveArrowPrefab;
    public GameObject rotateArrowPrefab;
    public GameObject scaleArrowPrefab;
    public Transform sceneParent;

    void Start()
    {
        if (assetButtonPrefab == null)
        {
            Debug.LogError("AssetButtonPrefab is not assigned.");
            return;
        }
        PopulateAssetPanel();
    }

    void PopulateAssetPanel()
    {
        foreach (GameObject asset in assets)
        {
            Transform rowContainer = GetNextRowContainer();
            GameObject button = Instantiate(assetButtonPrefab, rowContainer);
            AssetButton assetButton = button.GetComponent<AssetButton>();
            if (assetButton == null)
            {
                Debug.LogError("AssetButton component is not found on the button prefab.");
                continue;
            }
            assetButton.Initialize(asset, this);
        }
    }

    Transform GetNextRowContainer()
    {
        for (int i = 0; i < gridContainer.transform.childCount; i++)
        {
            Transform rowContainer = gridContainer.transform.GetChild(i);
            if (rowContainer.childCount < numColumns)
            {
                return rowContainer;
            }
        }

        GameObject newRowContainer = new GameObject("Row" + (gridContainer.transform.childCount + 1));
        newRowContainer.transform.SetParent(gridContainer.transform);
        HorizontalLayoutGroup horizontalLayoutGroup = newRowContainer.AddComponent<HorizontalLayoutGroup>();
        horizontalLayoutGroup.spacing = spacingBetweenButtons;
        horizontalLayoutGroup.childControlWidth = false;
        horizontalLayoutGroup.childControlHeight = false;
        return newRowContainer.transform;
    }

    public void SelectAsset(GameObject asset)
{
    // Instantiate the selected asset at the spawn point's position and rotation
    GameObject instantiatedAsset = Instantiate(asset, spawnPoint.position, spawnPoint.rotation);
    instantiatedAsset.transform.SetParent(spawnPoint);
    instantiatedAsset.AddComponent<SelectableItem>();

    // Set up movement, scaling, and rotation components
    CubeMovement cubeMovement = instantiatedAsset.GetComponent<CubeMovement>();
    CubeScaling cubeScaling = instantiatedAsset.GetComponent<CubeScaling>();
    CubeRotation cubeRotation = instantiatedAsset.GetComponent<CubeRotation>();

    // Instantiate arrows as children of the instantiated asset
    GameObject moveArrowsInstance = Instantiate(moveArrowPrefab, instantiatedAsset.transform);
    moveArrowsInstance.name = "MoveArrows";

    GameObject rotateArrowsInstance = Instantiate(rotateArrowPrefab, instantiatedAsset.transform);
    rotateArrowsInstance.name = "RotateArrows";

    GameObject scaleArrowsInstance = Instantiate(scaleArrowPrefab, instantiatedAsset.transform);
    scaleArrowsInstance.name = "ScaleArrows";

    // Assign arrow references to movement, scaling, and rotation components
    cubeMovement.xArrow = moveArrowsInstance.transform.Find("XArrowMove").gameObject;
    cubeMovement.yArrow = moveArrowsInstance.transform.Find("YArrowMove").gameObject;
    cubeMovement.zArrow = moveArrowsInstance.transform.Find("ZArrowMove").gameObject;

    cubeScaling.xArrow = scaleArrowsInstance.transform.Find("XArrowScale").gameObject;
    cubeScaling.yArrow = scaleArrowsInstance.transform.Find("YArrowScale").gameObject;
    cubeScaling.zArrow = scaleArrowsInstance.transform.Find("ZArrowScale").gameObject;

    cubeRotation.xArrow = rotateArrowsInstance.transform.Find("XArrowRotate").gameObject;
    cubeRotation.yArrow = rotateArrowsInstance.transform.Find("YArrowRotate").gameObject;
    cubeRotation.zArrow = rotateArrowsInstance.transform.Find("ZArrowRotate").gameObject;

    // Set the current asset in the UIManager
    uiManager.SetCurrentAsset(instantiatedAsset);
    uiPanel.SetActive(false);
}

}
