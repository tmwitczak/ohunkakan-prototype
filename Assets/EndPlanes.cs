using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPlanes : MonoBehaviour
{
    public GameObject winText, loseText;
    public int ID;

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player") && ID == 1)
        {
            Destroy(other.gameObject);
            winText.SetActive(true);
            StartCoroutine(waitToResetLvl());
        }
        else if (other.gameObject.CompareTag("Player") && ID == 0)
        {
            Destroy(other.gameObject);
            loseText.SetActive(true);
            StartCoroutine(waitToResetLvl());
        }
    }
    IEnumerator waitToResetLvl()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
