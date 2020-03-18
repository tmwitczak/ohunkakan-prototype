using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public GameObject cameraTarget;
	public float rotateSpeed;
	float rotate;
	public float offsetDistance;
	public float offsetHeight;
	public float smoothing;
	Vector3 offset;
	bool following = true;
	Vector3 lastPosition;
	public GameObject player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		cameraTarget = player;
		// lastPosition = new Vector3(cameraTarget.transform.position.x + 5.0f, cameraTarget.transform.position.y + offsetHeight, cameraTarget.transform.position.z - offsetDistance);
		// offset = new Vector3(cameraTarget.transform.position.x + 5.0f, cameraTarget.transform.position.y + offsetHeight, cameraTarget.transform.position.z - offsetDistance);
		offset = transform.position - player.transform.position;
	}

	public void SetPosition() {
		// lastPosition = new Vector3(cameraTarget.transform.position.x + 5.0f, cameraTarget.transform.position.y + offsetHeight, cameraTarget.transform.position.z - offsetDistance);
		// offset = new Vector3(cameraTarget.transform.position.x + 5.0f, cameraTarget.transform.position.y + offsetHeight, cameraTarget.transform.position.z - offsetDistance);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F))
		{
			if(following)
			{
				following = false;
			} 
			else
			{
				following = true;
			}
		} 
		// if(Input.GetKey(KeyCode.Q))
		// {
		// 	rotate = -1;
		// } 
		// else if(Input.GetKey(KeyCode.E))
		// {
		// 	rotate = 1;
		// } 
		// else
		// {
		// 	rotate = 0;
		// }
		// if(following)
		// {
		Vector3 temp = Vector3.zero;

		// transform.position = Vector3.Lerp(lastPosition, cameraTarget.transform.position + offset,
		// ref temp, 0.1f);
		transform.position = new Vector3(Mathf.Lerp(lastPosition.x, cameraTarget.transform.position.x + offset.x, smoothing * Time.deltaTime), 
			Mathf.Lerp(lastPosition.y, cameraTarget.transform.position.y * 0.75f + offset.y, smoothing * Time.deltaTime), 
			Mathf.Lerp(lastPosition.z, cameraTarget.transform.position.z + offset.z, smoothing * Time.deltaTime));
		// } 
		// else
		// {
		// 	transform.position = lastPosition; 
		// }
		// transform.LookAt(new Vector3(cameraTarget.transform.position.x,
		// cameraTarget.transform.position.y, cameraTarget.transform.position.z));
	}

	void LateUpdate()
	{
		lastPosition = transform.position;
	}
}