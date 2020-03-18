using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrap : MonoBehaviour
{
    void Start() {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider collider) {
        // if (collider.gameObject == playerController.gameObject) {
            playerController.changeToHuman();
            Object.Destroy(gameObject);
        // }
    }

    public PlayerController playerController;
}
