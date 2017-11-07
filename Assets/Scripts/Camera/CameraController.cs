using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private PlayerController player;
	private const float MAX_Y = 10.85f;
	private const float MIN_Y = -5.0f;

    private Vector3 lastPlayerPosition;
    private float distanceToMove;
	// Use this for initialization
	void Start ()
    {
        player = FindObjectOfType<PlayerController>();
        lastPlayerPosition = player.transform.position;

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
		if (IsInBounds ()) {
			distanceToMove = player.transform.position.x - lastPlayerPosition.x;

			float distanceToMoveY = player.transform.position.y - lastPlayerPosition.y;

			transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y + distanceToMoveY, transform.position.z);

			lastPlayerPosition = player.transform.position;
		}
	}

	private bool IsInBounds()
	{
		return (this.transform.position.y > MIN_Y && this.transform.position.y < MAX_Y);
	}
}
