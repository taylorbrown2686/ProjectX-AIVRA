using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackingManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Image manager on the AR Session Origin")]
    ARTrackedImageManager m_ImageManager;

    /// <summary>
    /// Get the <c>ARTrackedImageManager</c>
    /// </summary>
    public ARTrackedImageManager ImageManager
    {
        get => m_ImageManager;
        set => m_ImageManager = value;
    }

    [SerializeField]
    [Tooltip("Reference Image Library")]
    XRReferenceImageLibrary m_ImageLibrary;

    /// <summary>
    /// Get the <c>XRReferenceImageLibrary</c>
    /// </summary>
    public XRReferenceImageLibrary ImageLibrary
    {
        get => m_ImageLibrary;
        set => m_ImageLibrary = value;
    }

    [SerializeField]
    [Tooltip("Prefabs")]
    GameObject[] objects;
    GameObject[] spawnedObjects;


    int m_NumberOfTrackedImages;

    static Guid[] ImageGUID;

    void OnEnable()
    {
        spawnedObjects = new GameObject[objects.Length];
        ImageGUID = new Guid[m_ImageLibrary.count];

        for (int i = 0; i < ImageGUID.Length; i++)
            ImageGUID[i] = m_ImageLibrary[i].guid;

        m_ImageManager.trackedImagesChanged += ImageManagerOnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= ImageManagerOnTrackedImagesChanged;
    }

    void ImageManagerOnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        // added, spawn prefab
        foreach (ARTrackedImage image in obj.added)
        {

            for (int i = 0; i < ImageGUID.Length; i++)
            {
                if (image.referenceImage.guid == ImageGUID[i])
                    spawnedObjects[i] = Instantiate(objects[i], image.transform.position, image.transform.rotation);
            }

        }

        // updated, set prefab position and rotation
        foreach (ARTrackedImage image in obj.updated)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                for (int i = 0; i < ImageGUID.Length; i++)
                {
                    if (image.referenceImage.guid == ImageGUID[i])
                    {
                        spawnedObjects[i].SetActive(true);
                        spawnedObjects[i].transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    }
                }

            }
            else
            {
                for (int i = 0; i < ImageGUID.Length; i++)
                {
                    if (image.referenceImage.guid == ImageGUID[i])
                        spawnedObjects[i].SetActive(false);
                }
            }
        }

        // removed, destroy spawned instance
        foreach (ARTrackedImage image in obj.removed)
        {
            for (int i = 0; i < ImageGUID.Length; i++)
            {
                if (image.referenceImage.guid == ImageGUID[i])
                    Destroy(spawnedObjects[i]);
            }
        }
    }

    public int NumberOfTrackedImages()
    {
        m_NumberOfTrackedImages = 0;
        foreach (ARTrackedImage image in m_ImageManager.trackables)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                m_NumberOfTrackedImages++;
            }
        }
        return m_NumberOfTrackedImages;
    }
}
