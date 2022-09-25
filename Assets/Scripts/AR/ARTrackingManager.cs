using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARTrackingManager : MonoBehaviour
{
    public ARTrackedImageManager arTrackedImageManager;
    public GameObject whenImgTracked;

    void Start()
    {
        arTrackedImageManager.trackedImagesChanged += ArTrackedImageManager_trackedImagesChanged;
    }

    private void ArTrackedImageManager_trackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        List<ARTrackedImage> addedImages = obj.added;
        List<ARTrackedImage> removedImages = obj.removed;

        foreach (ARTrackedImage image in addedImages)
        {

            if (image.trackingState == TrackingState.Tracking)
            {
                whenImgTracked.SetActive(true);
                this.enabled = false;
            }
            else
            {
                whenImgTracked.SetActive(false);
            }
        }
    }
}
