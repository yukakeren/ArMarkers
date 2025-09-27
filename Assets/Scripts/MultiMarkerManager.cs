using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiMarkerManager : MonoBehaviour
{
    [System.Serializable]
    public struct MarkerPrefab
    {
        public string markerName;
        public GameObject prefab;
    }

    public List<MarkerPrefab> markerPrefabs;
    public ARTrackedImageManager trackedImageManager;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        // Marker baru terdeteksi
        foreach (var trackedImage in args.added)
        {
            SpawnPrefab(trackedImage);
        }

        // Marker posisi update
        foreach (var trackedImage in args.updated)
        {
            UpdatePrefab(trackedImage);
        }

        // Marker hilang (tidak terlihat lagi)
        foreach (var trackedImage in args.removed)
        {
            HidePrefab(trackedImage);
        }
    }

    private void SpawnPrefab(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        foreach (var mp in markerPrefabs)
        {
            if (mp.markerName == name && mp.prefab != null)
            {
                GameObject newPrefab = Instantiate(mp.prefab, trackedImage.transform.position, trackedImage.transform.rotation);
                spawnedPrefabs[name] = newPrefab;
                Debug.Log($"Spawned '{mp.prefab.name}' for marker '{name}'");
            }
        }
    }

    private void UpdatePrefab(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        if (spawnedPrefabs.TryGetValue(name, out GameObject prefab))
        {
            prefab.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);

            // 🔹 Sembunyikan kalau tidak lagi terdeteksi
            prefab.SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }
    }

    private void HidePrefab(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        if (spawnedPrefabs.TryGetValue(name, out GameObject prefab))
        {
            prefab.SetActive(false); // bisa Destroy(prefab) kalau mau hapus total
            Debug.Log($"Marker '{name}' hilang, prefab disembunyikan.");
        }
    }
}
