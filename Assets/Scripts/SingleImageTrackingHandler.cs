using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class SingleImageTrackingHandler : MonoBehaviour
{
    public GameObject ScanImage;
    public ARTrackedImageManager trackedImageManager;
    public List<GameObject> modelPrefabs;

    public Text text1;


    private Dictionary<string, GameObject> spawnedModels = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
           // CreateModel(trackedImage);
            text1.text = "Image Found: " + trackedImage.referenceImage.name;
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateModel(trackedImage);

             //text1.text = "Image Found Update::: " + trackedImage.referenceImage.name;
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
           // RemoveModel(trackedImage);

             text1.text = "Image Removed: " + trackedImage.referenceImage.name;
        }

        
    }

    void CreateModel(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (spawnedModels.ContainsKey(imageName))
            return;

        foreach (GameObject prefab in modelPrefabs)
        {
            if (prefab.name == imageName)
            {
                // ✅ Spawn in world (NOT parented)
                GameObject obj = Instantiate(
                    prefab,
                    trackedImage.transform.position,
                    trackedImage.transform.rotation
                );

                spawnedModels.Add(imageName, obj);
                ScanImage.SetActive(false);
                break;
            }
        }
    }

    void UpdateModel(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        // ✅ If tracking found again → recreate model
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            if (!spawnedModels.ContainsKey(imageName))
            {
                CreateModel(trackedImage);
                return;
            }

            GameObject obj = spawnedModels[imageName];

            // ✅ Update position
            obj.transform.position = trackedImage.transform.position;

            // ✅ FACE CAMERA (Y-axis only)
            Transform cam = Camera.main.transform;
            Vector3 direction = cam.position - obj.transform.position;
            direction.y = 0;
            obj.transform.rotation = Quaternion.LookRotation(-direction);

            // if (direction != Vector3.zero)
            // {
            //     Quaternion targetRotation = Quaternion.LookRotation(direction);
            //     obj.transform.rotation = Quaternion.Slerp(
            //         obj.transform.rotation,
            //         targetRotation,
            //         Time.deltaTime * 5f
            //     );
            // }

            ScanImage.SetActive(false);
        }
        else
        {
            //✅ Destroy when tracking lost
            if (spawnedModels.ContainsKey(imageName))
            {
                Destroy(spawnedModels[imageName]);
                spawnedModels.Remove(imageName);
            }
 text1.text = "Image Removed: " + trackedImage.referenceImage.name;
            ScanImage.SetActive(true);
        }
    }

    void RemoveModel(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (spawnedModels.ContainsKey(imageName))
        {
            Destroy(spawnedModels[imageName]);
            spawnedModels.Remove(imageName);
        }

        ScanImage.SetActive(true);
    }

    // void OnApplicationFocus(bool focus)
    // {
    //     if (focus)
    //     {
    //         trackedImageManager.enabled = false;
    //         trackedImageManager.enabled = true;
    //     }
    // }
}