using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

        /*
         * Task 2:
         *
         * Update camera movement for zooming and turning. Luckily most of the functionality is already implemented.
         *
         *
         * Todo:
         *      First read through the comments to get the general idea how the camera movement works.
         *      
         *      Read mouse input and modify the rotation offset for rotating.
         *      Read mouse input and modify the height and distance variables for zooming
         *      Make sure to clamp the values into reasonable ranges! You can use the Mathf class for that.
         *
         *      If you do not have a mouse:
         *          use up and down arrows for zoom
         *          use left and right arrows for rotation
         *
         * Extra:
         *
         *      Use animation curves to make a better transition from close up to top down vies.
         *
         */



		[Tooltip("The target we wish to look at.")]
		[SerializeField]
		private Transform target;

	    [Tooltip("Offset from the point we look at.")]
        [SerializeField]
	    private Vector3 offset;

		[Tooltip("The distance in the x-z plane to the target")]
		[SerializeField]
		private float distance;

	    [Tooltip("The height we want the camera to be above the target")]
		[SerializeField]
		private float height;

        [SerializeField]
	    private float rotationSpeed = 1f;

		[SerializeField]
		private float rotationDamping;
		[SerializeField]
		private float heightDamping;

	    private float rotationOffset;

		// Use this for initialization
		void Start() { }

		// Update is called once per frame
		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;

			// TODO: Read input and modify distance and height
			//if (Input.mouseScrollDelta)
			float scroll = Input.GetAxis("Mouse ScrollWheel");
			distance = Mathf.Clamp(distance - scroll, 1, 5);
			height = Mathf.Clamp(height - scroll*2, 1, 10);

			// TODO: Read input and modify rotation offset
			if (Input.GetMouseButton(1))
			{
				rotationOffset = Input.GetAxis("Mouse X");
				// TODO not working completely. The camera shakes but never moves
			}

			float wantedHeight = target.position.y + height;

			float currentRotationAngle = transform.eulerAngles.y;
			float currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, rotationOffset, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation 
            // TODO: Apply the rotation to correct axis
			Quaternion currentRotation = Quaternion.Euler(0, rotationOffset, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position + offset;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x, currentHeight , transform.position.z);

			// Always look at the target
			transform.LookAt(target.position + offset);
		}
	}
}