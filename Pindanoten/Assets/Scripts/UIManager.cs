using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject cube; // Reference to the cube GameObject

    public void EnableMovementMode()
    {
        // Enable CubeMovement script and disable CubeScaling script
        cube.GetComponent<CubeMovement>().enabled = true;
        cube.GetComponent<CubeScaling>().enabled = false;
    }

    public void EnableScalingMode()
    {
        // Enable CubeScaling script and disable CubeMovement script
        cube.GetComponent<CubeMovement>().enabled = false;
        cube.GetComponent<CubeScaling>().enabled = true;
    }
}

