using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlighter : MonoBehaviour {
    void Start() {
        color = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update() {
        float z = Mathf.Abs(transform.position.z - playerController.currentForm.transform.position.z);

        if (z > 2.0f) {
            intensity = 0.0f;
        }
        else {
            intensity = 1.0f - z / 2.0f;
        }

        GetComponent<Renderer>().material.SetColor("_EmissionColor", color * intensity);
    }

    public PlayerController playerController;
    public Color color;
    public float intensity = 0.0f;
}
