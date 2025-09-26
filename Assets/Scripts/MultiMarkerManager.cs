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

        foreach (var trackedImage in trackedImageManager.trackables)
        {
            UpdateSpawnedPrefab(trackedImage);
        }
    }

    private void UpdateSpawnedPrefab(ARTrackedImage trackedImage)
    {
        // 🔹 Cek null dulu
        if (trackedImage == null || trackedImage.referenceImage == null)
        {
            Debug.LogWarning("[MultiMarkerManager] TrackedImage atau ReferenceImage null, dilewati.");
            return;
        }

        string name = trackedImage.referenceImage.name;

        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning("[MultiMarkerManager] ReferenceImage tanpa nama, dilewati.");
            return;
        }

        // 🔹 Spawn kalau belum ada
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
                    Debug.Log($"[MultiMarkerManager] Spawned prefab '{mp.prefab.name}' untuk marker '{name}'");
                }
            }
        }

        // 🔹 Update posisi kalau prefab sudah ada
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
 