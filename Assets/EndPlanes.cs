using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPlanes : MonoBehaviour
{
    public GameObject winText, loseText;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }
}
