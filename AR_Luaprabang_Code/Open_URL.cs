using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_URL : MonoBehaviour
{
    public void OpenURL_GIS(string url)
    {
        Application.OpenURL("https://afrygis-th.maps.arcgis.com/apps/mapviewer/index.html?webmap=39fc2004f81a47659c708c9937bea87b");
    }
}
