using UnityEngine;
using System;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour 
{
    private int vcrFrameRate = 60;
   
    public bool isRewinding { get; private set;}
    private List<Frame> recordedFrames;
    private int rewindFrameIndex;
    private List<TimeManageable> deleteOnNextRewindFrame;

    void Awake() {
        // Set framerate
        setFrameRate(vcrFrameRate);

        // Initialize variables
        isRewinding = false;
        deleteOnNextRewindFrame = new List<TimeManageable>();
        recordedFrames = new List<Frame>();
        recordedFrames.Add(new Frame());
    }

    void Start() {

    }

    void Update() {
       if (checkRewindCondition() && !isRewinding) {
           isRewinding = true; 
           rewindFrameIndex = recordedFrames.Count - 1;
           setFrameRate(vcrFrameRate * 2);
       }
       if (isRewinding) {

           bool hasRewindFinished = HandleRewind(0, rewindFrameIndex);
           rewindFrameIndex -= 1;

           if (hasRewindFinished) {
               setInitialConditions();
           }
       }
    }

    void LateUpdate() {
        recordedFrames.Add(new Frame());
    }

    private bool HandleRewind(int targetFrame, int rewindFrameIndex) {
        bool hasRewindFinished = targetFrame == rewindFrameIndex;
        if (!hasRewindFinished) { 
            Frame previousFrame = this.recordedFrames[rewindFrameIndex];
            RewindOneFrame(previousFrame);
            DeleteEntitiesCreatedLastFrame();
            this.deleteOnNextRewindFrame.Clear();
        }
        return hasRewindFinished; 
    }

    private void RewindOneFrame(Frame frame) {
        foreach (FrameItem fi in frame.frameEntities) {
            TimeManageable tm = fi.item;
            tm.gameObject.SetActive(true);
            tm.transform.position = fi.transformData.position;
            tm.transform.rotation = fi.transformData.rotation;

            if (fi.deactivateAfterThisFrame) {
                this.deleteOnNextRewindFrame.Add(tm);
            }
        }
    }

    private void DeleteEntitiesCreatedLastFrame() {
        foreach (TimeManageable tm in this.deleteOnNextRewindFrame) {
            tm.gameObject.SetActive(false);
            tm.resetCreation();
        }
    }

    private void setInitialConditions() {
        isRewinding = false;
        setFrameRate(vcrFrameRate);

        // For all objects
        StopPlayerMovement();

        // Reset stored positions
        recordedFrames.Clear();
        recordedFrames.Add(new Frame());
        deleteOnNextRewindFrame.Clear();
       
    }

    private void StopPlayerMovement() {
        Rigidbody player = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        player.velocity = Vector3.zero;
        player.angularVelocity = Vector3.zero;
    }

    public void addFrameItemToLatestFrame(Vector3 position, Quaternion rotation,
                                          bool deactivateAfterThisFrame, TimeManageable item) {
        TransformData tfd = new TransformData(); tfd.position = position; tfd.rotation = rotation;
        FrameItem newFrameItem = new FrameItem(); newFrameItem.transformData = tfd;
        newFrameItem.deactivateAfterThisFrame = deactivateAfterThisFrame;
        newFrameItem.item = item;
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

    public bool deactivateAfterThisFrame;

    public TimeManageable item;
}

public struct TransformData {
    public Vector3 position;
    public Quaternion rotation;
}
