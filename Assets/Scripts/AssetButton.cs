using UnityEngine;
using UnityEngine.UI;

public class AssetButton : MonoBehaviour
{
    private GameObject asset;
    private AssetLibrary assetLibrary;

    public void Initialize(GameObject asset, AssetLibrary assetLibrary)
    {
        this.asset = asset;
        this.assetLibrary = assetLibrary;

        // Find the SpriteRenderer in the asset
        SpriteRenderer spriteRenderer = asset.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Set the button's image to the sprite from the SpriteRenderer
            Image buttonImage = GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.sprite = spriteRenderer.sprite;
            }
            else
            {
                Debug.LogError("No Image component found on the button prefab.");
            }

            // Hide the sprite on the prefab object itself
            spriteRenderer.enabled = false;
        }
        else
        {
            Debug.LogWarning("No SpriteRenderer found on asset: " + asset.name);
        }

        // Add the onClick listener to the button
        Button buttonComponent = GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("No Button component found on the button prefab.");
        }
    }

    void OnClick()
    {
        assetLibrary.SelectAsset(asset);
    }
}
