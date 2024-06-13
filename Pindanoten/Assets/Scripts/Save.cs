using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour{
    public Transform Player;

    void Start () {
        float x = PlayerPrefs.GetFloat("PPX");
        float y = PlayerPrefs.GetFloat("PPY");
        float z = PlayerPrefs.GetFloat("PPZ");

        Player.position = new Vector3(x, y, z);
    }

    void Update () {
        PlayerPrefs.SetFloat("PPX", Player.position.x);
        PlayerPrefs.SetFloat("PPY", Player.position.y);
        PlayerPrefs.SetFloat("PPZ", Player.position.z);
    }
}