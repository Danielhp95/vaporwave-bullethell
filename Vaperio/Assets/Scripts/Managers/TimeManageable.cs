using UnityEngine;

public class TimeManageable : MonoBehaviour {

    private TimeManager timeManager;
    private bool createdOnThisFrame;

    void Start() {
        timeManager = GameObject.FindWithTag("TimeManager").GetComponent("TimeManager") as TimeManager;
        resetCreation();
    }

    public void resetCreation() {
        createdOnThisFrame = true;
    }

    void Update() {
        if (!timeManager.isRewinding) {
            timeManager.addFrameItemToLatestFrame(this.transform.position, this.transform.rotation,
                                                  this.createdOnThisFrame, this);
        }
        createdOnThisFrame = false;
    }
}
