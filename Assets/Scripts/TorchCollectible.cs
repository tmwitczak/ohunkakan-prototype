using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchCollectible : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.resetTorchLight();
            Destroy(gameObject);
        }
    }
}
