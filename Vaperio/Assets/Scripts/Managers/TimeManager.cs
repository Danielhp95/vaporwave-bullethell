using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour 
{
    public GameObject ship;
    private List<TransformData> spaceshipPositions;

    private int vcrFrameRate = 30;
   
    private bool isRewinding = false;
    private int rewindFrameIndex;

    void Awake() {
        // Set framerate
        setFrameRate(vcrFrameRate);

        // Initialize variables
        spaceshipPositions = new List<TransformData>();
    }

    void Start() {

    }

    void Update() {
       if (checkRewindCondition()) {
           isRewinding = true; 
           rewindFrameIndex = spaceshipPositions.Count - 1;
           setFrameRate(vcrFrameRate * 2);
       }
       if (isRewinding) {

           ship.transform.position = spaceshipPositions[rewindFrameIndex].position;
           ship.transform.rotation = spaceshipPositions[rewindFrameIndex].rotation;

           rewindFrameIndex -= 1;

           if (rewindFrameIndex == 0) {
               setInitialConditions();
           }
       } else {
           TransformData td = new TransformData();
           td.position = ship.transform.position;
           td.rotation = ship.transform.rotation;
           spaceshipPositions.Add(td);   
       }

    }

    private bool checkRewindCondition() {
        return Input.GetKeyDown(KeyCode.R);
    }

    private void setInitialConditions() {
        isRewinding = false;
        setFrameRate(vcrFrameRate);

        // For all objects
        Rigidbody shipBody = ship.GetComponent<Rigidbody>();
        shipBody.velocity = Vector3.zero;
        shipBody.angularVelocity = Vector3.zero;

        // Reset stored positions
        spaceshipPositions.Clear();        
       
    }

    private void setFrameRate(int targetFrameRate) {
        if (QualitySettings.vSyncCount != 0) {
            QualitySettings.vSyncCount = 0; 
        }
        Application.targetFrameRate = targetFrameRate;
    }

}

public struct TransformData
{
    public Vector3 position;
    public Quaternion rotation;
}
