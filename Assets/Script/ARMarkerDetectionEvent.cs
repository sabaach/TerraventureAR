using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class ARMarkerDetectionEvent : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager ARManager;
    [SerializeField] string thisLevelMarker;
    [SerializeField] GameObject ARPrefab;
    [SerializeField] UnityEvent onTracked;

    // Start is called before the first frame update
    void Start()
    {
        // ARManager.trackedImagesChanged += context => TrackingChanged();
        ARManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    // [SerializeField] ImageMarkerLibrary imageMarkerLibrary;
    private Dictionary<string, GameObject> _instantiatedTrackedPrefabs = new();

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // print(newImage.referenceImage.name);
            if (newImage.referenceImage.name == thisLevelMarker)
            {
                onTracked.Invoke();

                if (_instantiatedTrackedPrefabs.ContainsKey(thisLevelMarker))
                {
                    continue;
                }

                if (ARPrefab != null)
                {
                    _instantiatedTrackedPrefabs.Add(thisLevelMarker,
                          Instantiate(ARPrefab, newImage.transform));
                }
            }
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (updatedImage.name == thisLevelMarker)
            {
                _instantiatedTrackedPrefabs[thisLevelMarker].SetActive(updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking);
            }
        }

        foreach (var removedImage in eventArgs.removed)
        {
            var key = removedImage.name;

            if (!_instantiatedTrackedPrefabs.ContainsKey(key))
            {
                continue;
            }

            var objectToDestroy = _instantiatedTrackedPrefabs[key];
            _instantiatedTrackedPrefabs.Remove(key);
            Destroy(objectToDestroy);
        }
    }
}
