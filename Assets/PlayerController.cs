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

    public void changeToHuman() {
        changeForm(humanForm);
    }

    private void changeForm(GameObject newForm)
    {
        Object.Destroy(GameObject.Instantiate(explosionPrefab,
            currentForm.transform.position, Quaternion.identity), 10.0f);

        GameObject previousForm = currentForm;
        currentForm = newForm;

        newForm.transform.position = previousForm.transform.position;

        newForm.SetActive(true);
        previousForm.SetActive(false);

        mainCamera.player = newForm;
        mainCamera.cameraTarget = newForm;
        mainCamera.SetPosition();
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
            Object.Destroy(GameObject.Instantiate(explosionPrefab, currentForm.transform.position,
            Quaternion.identity), 10.0f);

            if (currentForm != eagleForm) {
                changeForm(eagleForm);
            }
            else{
                changeForm(humanForm);
            }
        }

        if (Input.GetKeyDown("e") && !canChangeForm)
        {
            Object.Destroy(GameObject.Instantiate(explosionPrefabFail, currentForm.transform.position, Quaternion.identity), 10.0f);
        }

        if(Input.GetKeyDown("c") && canChangeForm)
        {
            Object.Destroy(GameObject.Instantiate(explosionPrefab, currentForm.transform.position,
            Quaternion.identity), 10.0f);

            if (currentForm != catForm)
            {
                changeForm(catForm);
            }
            else if (currentForm == catForm)
            {
                changeForm(humanForm);
            }
        }

        if (Input.GetKeyDown("c") && !canChangeForm)
        {
            Object.Destroy(GameObject.Instantiate(explosionPrefabFail, currentForm.transform.position, Quaternion.identity), 10.0f);
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
        else
        {
            moveInput.y = 0.0f;
            currentVelocity = Vector3.MoveTowards(currentVelocity, moveInput * runSpeed, movementAcceleration * Time.deltaTime);
            catForm.transform.position += currentVelocity * Time.deltaTime;
        }

        // Update torch position
        torch.transform.position = new Vector3(
            currentForm.transform.position.x,
            currentForm.transform.position.y + 3,
            // torch.transform.position.y,
            currentForm.transform.position.z - 2.0f
        );

        if(torchLight.range > 7)
        {
            torchLight.range -= Time.deltaTime * 1.4f;
        }
        else
        {
            torchLight.range = 7;
            torchLight.intensity = Mathf.Lerp(3f, 3.5f, Mathf.PingPong(Time.time, 1f));
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
        torchLight.range = 20;
        torchLight.intensity = 4;
    }

    public float movementAcceleration = 90.0f;
    public float runSpeed = 6f;

	public Vector3 currentVelocity;
    public int currentLane;
    public int laneWidth;
    public GameObject humanForm;
    public GameObject eagleForm;
    public GameObject catForm;
    public GameObject explosionPrefab;
    public GameObject explosionPrefabFail;
    public CameraController mainCamera;
    public GameObject torch;
    private Light torchLight;


    public GameObject currentForm;
    [HideInInspector] public bool canChangeForm;

        [HideInInspector] public bool inputLaneUp;
		[HideInInspector] public bool inputLaneDown;

    private Vector3 moveInput;

}
