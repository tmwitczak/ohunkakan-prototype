using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.x - player.transform.position.x <= playerDistance)
        {
            Move();
        }
    }

    private void Move()
    {
        if(movingLeft)
        {
            transform.Translate(new Vector3(-angle, 0.0f, -angle) * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(new Vector3(-angle, 0.0f, angle) * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Boundry"))
        {
            movingLeft = !movingLeft;
        }
    }

    private float speed = 8.0f;
    private float angle = 0.45f;
    private bool movingLeft = true;
    public GameObject player;
    public float playerDistance = 30.0f;
}
