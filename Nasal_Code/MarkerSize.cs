using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerSize : MonoBehaviour
{
    public Toggle Marker_05;
    public Toggle Marker_03;

    void Start()
    {
        StaticData.Marker05 = Marker_05.isOn;
        StaticData.Marker03 = Marker_03.isOn;

        Marker_05.onValueChanged.AddListener(delegate {
            ToggleValueChanged05(Marker_05);
            });

        Marker_03.onValueChanged.AddListener(delegate {
            ToggleValueChanged03(Marker_03);
        });
    }

    void ToggleValueChanged05(Toggle marker)
    {
        StaticData.Marker05 = Marker_05.isOn;
        Debug.Log("05: " + StaticData.Marker05);
    }
    void ToggleValueChanged03(Toggle marker)
    {
        StaticData.Marker03 = Marker_03.isOn;
        Debug.Log("03: " + StaticData.Marker03);
    }

}
