using UnityEngine;
using System;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour 
{
    public GameObject ship;
    private List<Frame> recordedFrames;

    private int vcrFrameRate = 30;
   
    private bool isRewinding = false;
    private int rewindFrameIndex;

    void Awake() {
        // Set framerate
        setFrameRate(vcrFrameRate);

        // Initialize variables
        recordedFrames = new List<Frame>();
        recordedFrames.Add(new Frame());
    }

    void Start() {

    }

    void Update() {
       if (checkRewindCondition()) {
           isRewinding = true; 
           rewindFrameIndex = recordedFrames.Count - 1;
           setFrameRate(vcrFrameRate * 2);
       }
       if (isRewinding) {

           bool hasRewindFinished = Rewind(0, rewindFrameIndex);

           rewindFrameIndex -= 1;

           if (hasRewindFinished) {
               setInitialConditions();
           }
       } else {
           // Record information, obsolete
           addFrameItemToLatestFrame(ship.transform.position, ship.transform.rotation, "Player");
       }

    }

    void LateUpdate() {
        recordedFrames.Add(new Frame());
    }

    private bool Rewind(int targetFrame, int rewindFrameIndex) {
        bool hasRewindFinished = targetFrame == rewindFrameIndex;
        if (!hasRewindFinished) { 
            RewindOneFrame(targetFrame, rewindFrameIndex);
        }
        return hasRewindFinished; 
    }

    private void RewindOneFrame(int endFrame, int rewindFrameIndex) {
        foreach (FrameItem fi in recordedFrames[rewindFrameIndex].frameEntities) {
            GameObject go = retrieveItem(fi.id);
            go.transform.position = fi.transformData.position;
            go.transform.rotation = fi.transformData.rotation;
        }
    }

    private void setInitialConditions() {
        isRewinding = false;
        setFrameRate(vcrFrameRate);

        // For all objects
        Rigidbody shipBody = ship.GetComponent<Rigidbody>();
        shipBody.velocity = Vector3.zero;
        shipBody.angularVelocity = Vector3.zero;

        // Reset stored positions
        recordedFrames.Clear();
       
    }

    public void addFrameItemToLatestFrame(Vector3 position, Quaternion rotation, String tag) {
        TransformData tfd = new TransformData(); tfd.position = position; tfd.rotation = rotation;
        FrameItem newFrameItem = new FrameItem(); newFrameItem.transformData = tfd; newFrameItem.id = tag;
        recordedFrames[recordedFrames.Count - 1].AddFrameItem(newFrameItem);
    }

    private bool checkRewindCondition() {
        return Input.GetKeyDown(KeyCode.R);
    }

    private void setFrameRate(int targetFrameRate) {
        if (QualitySettings.vSyncCount != 0) {
            QualitySettings.vSyncCount = 0; 
        }
        Application.targetFrameRate = targetFrameRate;
    }

    private GameObject retrieveItem(String s) {
        return ship;
    }

}

public class Frame {
    public List<FrameItem> frameEntities{ get; private set;}

    public Frame() {
        frameEntities = new List<FrameItem>();
    }

    public void AddFrameItem(FrameItem fi) {
        frameEntities.Add(fi);
    }
}

public struct FrameItem {
    public TransformData transformData;
    public String id; // tag
}

public struct TransformData {
    public Vector3 position;
    public Quaternion rotation;
}
