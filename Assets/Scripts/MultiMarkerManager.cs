using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MultiMarkerManager : MonoBehaviour
{
    [System.Serializable]
    public struct MarkerPrefab
    {
        public string markerName;   // Nama marker di Reference Image Library
        public GameObject prefab;   // Prefab yang akan muncul
    }

    public List<MarkerPrefab> markerPrefabs;
    public ARTrackedImageManager trackedImageManager;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void Update()
    {
        if (trackedImageManager == null) return;

        // Loop semua marker yang sedang ter-track
        foreach (var trackedImage in trackedImageManager.trackables)
        {
            UpdateSpawnedPrefab(trackedImage);
        }
    }

    private void UpdateSpawnedPrefab(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        // Kalau marker baru pertama kali terdeteksi → spawn prefab
        if (!spawnedPrefabs.ContainsKey(name))
        {
            foreach (var mp in markerPrefabs)
            {
                if (mp.markerName == name && mp.prefab != null)
                {
                    GameObject newPrefab = Instantiate(
                        mp.prefab,
                        trackedImage.transform.position,
                        trackedImage.transform.rotation
                    );
                    spawnedPrefabs[name] = newPrefab;
                }
            }
        }

        // Kalau sudah ada prefabnya → update posisi dan rotasi
        if (spawnedPrefabs.ContainsKey(name))
        {
            GameObject prefab = spawnedPrefabs[name];
            prefab.transform.SetPositionAndRotation(
                trackedImage.transform.position,
                trackedImage.transform.rotation
            );

            prefab.SetActive(
                trackedImage.trackingState == TrackingState.Tracking
                || trackedImage.trackingState == TrackingState.Limited
            );
        }
    }
}
