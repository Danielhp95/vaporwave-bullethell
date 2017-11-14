using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientCameraMovements : MonoBehaviour {
	public float spaceSpeed = 0.2f;
	public Vector3 spaceDirection = new Vector3(0f, 0f, 0f);
	public Vector3 defaultPositions = new Vector3(0f, 0f, -9f);
	public Vector3 postionWidths = new Vector3(1.25f, 0.625f, 0.125f);

	public float rotationSpeed = 0.5f;
	public Vector3 rotationDirection = new Vector3(0f, 0f, 0f);
	public Vector3 defaultRotations = new Vector3(0f, 0f, 0f);
	public Vector3 rotationWidths = new Vector3(3f, 3f, 3f);
	 
	// Use this for initialization
	void Start () {
		spaceDirection = getDirection ();
		rotationDirection = getDirection ();
	}
		
	private Vector3 getDirection () {
		float x = (float) Random.Range (-100, 100) * 0.01f;
		float y = (float) Random.Range (-100, 100) * 0.01f;
		float z = (float) Random.Range (-100, 100) * 0.01f;
		return new Vector3 (x, y, z); 	
	}

	// Update is called once per frame
	void Update () {
		if (!Pause.paused) {
			Vector3 translation = transformCamera (transform.position, scaleVector(spaceDirection, spaceSpeed), true);
			transform.Translate(translation, Space.World);
			Vector3 eulerAngles = getNegativeAngles(transform.eulerAngles);
			Vector3 rotation = transformCamera (eulerAngles, scaleVector(rotationDirection, rotationSpeed), false);
			transform.Rotate (rotation);
		}
	}

	private Vector3 getNegativeAngles (Vector3 eulerAngles) {
		float x = eulerAngles.x < 90 ? eulerAngles.x : eulerAngles.x - 360;
		float y = eulerAngles.y < 90 ? eulerAngles.y : eulerAngles.y - 360;
		float z = eulerAngles.z < 90 ? eulerAngles.z : eulerAngles.z - 360;
		return new Vector3 (x, y, z);
	}

	private Vector3 transformCamera(Vector3 currentPosition, Vector3 scaledDirection, bool isSpace) {
		Vector3 newPosition = getNewPosition (currentPosition, scaledDirection);
		Vector3 internalPostion = makePositionInternal (newPosition, isSpace);
		float xMovement = internalPostion.x - currentPosition.x;
		float yMovement = internalPostion.y - currentPosition.y;
		float zMovement = internalPostion.z - currentPosition.z;


		return new Vector3 (xMovement, yMovement, zMovement);
	}

	private Vector3 scaleVector (Vector3 vector, float targetSpeed){
		float targetLength = targetSpeed * Time.deltaTime;
		float directionLengthSquared = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		float targetLengthSquared = targetLength * targetLength;
		float ratioSquared = targetLengthSquared / directionLengthSquared;
		float ratio = Mathf.Sqrt ( Mathf.Abs(ratioSquared));
		return new Vector3 ( vector.x * ratio, vector.y * ratio, vector.z * ratio);
	}



	private Vector3 getNewPosition(Vector3 currentPosition, Vector3 scaledDirection) {
		return new Vector3 (
			currentPosition.x + scaledDirection.x,
			currentPosition.y + scaledDirection.y,
			currentPosition.z + scaledDirection.z
		);
	}

	private Vector3 makePositionInternal(Vector3 newPosition, bool isSpace) {
		float newX = newPosition.x;
		float newY = newPosition.y;
		float newZ = newPosition.z;

		Vector3 defaultPositions = isSpace ? this.defaultPositions : this.defaultRotations;
		Vector3 widths = isSpace ? this.postionWidths : this.rotationWidths;

		if (Mathf.Abs(newX) - defaultPositions.x > widths.x) {
			newX = getInternalPoint (newX, widths.x, defaultPositions.x);
			reverseXDirection (isSpace);
		} if(Mathf.Abs(newY) - defaultPositions.y > widths.y) {
			newY = getInternalPoint (newY, widths.y, defaultPositions.y);
			reverseYDirection (isSpace);
		} if(Mathf.Abs(newZ - defaultPositions.z) > widths.z) {
			newZ = getInternalPoint (newZ, widths.z, defaultPositions.z);
			reverseZDirection (isSpace);	 
		}
		return new Vector3 (newX, newY, newZ);
	}

	private float getInternalPoint(float position, float width, float defaultPostion) {
		float relativePosition = position - defaultPostion;
		float distanceOutside = Mathf.Abs (relativePosition) - width;
		float internalPosition =  width - distanceOutside;
		if (relativePosition < 0) {
			internalPosition = - internalPosition;
		}
		return internalPosition + defaultPostion;
	}

	private void reverseXDirection(bool isSpace) {
		if (isSpace) {
			spaceDirection = new Vector3 (-spaceDirection.x, spaceDirection.y, spaceDirection.z);
		} else {
			rotationDirection = new Vector3 (-rotationDirection.x, rotationDirection.y, rotationDirection.z);
		}
	}

	private void reverseYDirection(bool isSpace) {
		if (isSpace) {
			spaceDirection = new Vector3 (spaceDirection.x, -spaceDirection.y, spaceDirection.z);
		} else {
			rotationDirection = new Vector3 (rotationDirection.x, -rotationDirection.y, rotationDirection.z);
		}
	}

	private void reverseZDirection(bool isSpace) {
		if (isSpace) {
			spaceDirection = new Vector3 (spaceDirection.x, spaceDirection.y, -spaceDirection.z);
		} else {
			rotationDirection = new Vector3 (rotationDirection.x, rotationDirection.y, -rotationDirection.z);
		}
	}
}
