using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Rigidbody playerRb;
    public Vector3 offset;
    public float speed;
    public int cameraIndex = 0;
    public Vector3[] cameraPos = new Vector3[3]
	{
		new Vector3(0, 3, -4),
		new Vector3(0, 10, -4),
		new Vector3(0, 20, -4),
	};

    private void Start() {
      playerRb = player.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
      	Vector3 playerForward = (playerRb.velocity + player.transform.forward).normalized;
      	transform.position = Vector3.Lerp(transform.position, 
      	   player.position + player.transform.TransformVector(offset) + playerForward * (-4f), speed * Time.deltaTime);
      	transform.LookAt(player);
    }

    public void ChangeCamera()
    {
		if(cameraIndex >= 2)
		{
			cameraIndex = 0;
		} else 
		{
			cameraIndex ++;
		}
      	offset = cameraPos[cameraIndex];
    }
}
