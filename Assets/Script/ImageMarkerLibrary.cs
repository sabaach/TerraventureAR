using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Image Marker Library", fileName = "Image Marker Library")]
public class ImageMarkerLibrary : ScriptableObject
{
    public List<ImageMarkerData> ImageMarkerDataList;
}

[System.Serializable]
public class ImageMarkerData
{
    public string Name;
    public GameObject PrefabToInstantiate;
}