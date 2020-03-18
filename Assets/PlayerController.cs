using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        currentForm = humanForm;
        canChangeForm = true;
        torchLight = torch.GetComponent<Light>();
    }

    void Update()
    {
        inputLaneDown = Input.GetKeyDown("s");
        inputLaneUp = Input.GetKeyDown("w");

        // Change current lane
        currentLane += inputLaneUp ? 1 : 0;
        currentLane -= inputLaneDown ? 1 : 0;

        if(Input.GetKeyDown("e") && canChangeForm)
        {
            GameObject.Instantiate(explosionPrefab, currentForm.transform.position,
            Quaternion.identity);

            if (currentForm == humanForm) {
                currentForm = eagleForm;

                eagleForm.transform.position = new Vector3(humanForm.transform.position.x,
                    eagleForm.transform.position.y, humanForm.transform.position.z);

                eagleForm.SetActive(true);
                humanForm.SetActive(false);

                mainCamera.player = eagleForm;
                mainCamera.cameraTarget = eagleForm;
                mainCamera.SetPosition();
            }
            else if (currentForm == eagleForm) {
                currentForm = humanForm;

                humanForm.transform.position = new Vector3(eagleForm.transform.position.x,
                    humanForm.transform.position.y, eagleForm.transform.position.z);

                eagleForm.SetActive(false);
                humanForm.SetActive(true);

                mainCamera.player = humanForm;
                mainCamera.cameraTarget = humanForm;
                mainCamera.SetPosition();
            }
        }

        if (!canChangeForm)
        {
            StartCoroutine(trapEntered());
        }

        // Move only in lane increments
        moveInput.x = 1.0f;//Mathf.Clamp(Input.GetAxisRaw("Horizontal") + 1.0f, 0.5f, 1.5f);
        moveInput.y = -Input.GetAxisRaw("Horizontal");
        moveInput.z = (new Vector3(currentLane * laneWidth - currentForm.transform.position.z, 0.0f, 0.0f)).x;

        if (currentForm == humanForm)
        {
            moveInput.y = 0.0f;

			humanForm.GetComponent<RPGCharacterAnims.RPGCharacterInputControllerFREE>().inputHorizontal = Input.GetAxisRaw("Horizontal");
			humanForm.GetComponent<RPGCharacterAnims.RPGCharacterInputControllerFREE>().inputVertical = Input.GetAxisRaw("Vertical");
			humanForm.GetComponent<RPGCharacterAnims.RPGCharacterInputControllerFREE>().moveInput = moveInput;
        }
        else if (currentForm == eagleForm)
        {
            eagleForm.GetComponent<DemoController>().horizontal = Input.GetAxisRaw("Horizontal");
            eagleForm.GetComponent<DemoController>().vertical = Input.GetAxisRaw("Vertical");

            currentVelocity = Vector3.MoveTowards(currentVelocity, moveInput * runSpeed, movementAcceleration * Time.deltaTime);
			eagleForm.transform.position += currentVelocity * Time.deltaTime;
        }

        // Update torch position
        torch.transform.position = new Vector3(
            currentForm.transform.position.x,
            torch.transform.position.y,
            currentForm.transform.position.z - 2.0f
        );

        if(torchLight.range > 5)
        {
            torchLight.range -= Time.deltaTime * 1.5f;
        }
        else
        {
            torchLight.range = 5;
            torchLight.intensity = Mathf.Lerp(2f, 2.8f, Mathf.PingPong(Time.time, 1f));
        }
    }

    //cooldown for changing form after entering a trap
    private IEnumerator trapEntered()
    {
        yield return new WaitForSeconds(1f);
        canChangeForm = true;
    }

    public void resetTorchLight()
    {
        torchLight.range = 15;
        torchLight.intensity = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    public float movementAcceleration = 90.0f;
    public float runSpeed = 6f;

	public Vector3 currentVelocity;
    public int currentLane;
    public int laneWidth;
    public GameObject humanForm;
    public GameObject eagleForm;
    public GameObject explosionPrefab;
    public CameraController mainCamera;
    public GameObject torch;
    private Light torchLight;


    private GameObject currentForm;
    [HideInInspector] public bool canChangeForm;

        [HideInInspector] public bool inputLaneUp;
		[HideInInspector] public bool inputLaneDown;

    private Vector3 moveInput;

}
