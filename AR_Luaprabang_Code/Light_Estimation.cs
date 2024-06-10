using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class Light_Estimation : MonoBehaviour
{
    public TMP_Text BrightText;
    public ARCameraManager ARCM;
    Light MyLight;

    // Start is called before the first frame update
    void Start()
    {
        MyLight = GetComponent<Light>();
    }

    private void OnEnable()
    {
        ARCM.frameReceived += GetLight;
    }

    private void OnDisable()
    {
        ARCM.frameReceived -= GetLight;
    }
    void GetLight(ARCameraFrameEventArgs args)
    {
        Debug.Log(args.lightEstimation);
        if(args.lightEstimation.mainLightColor.HasValue)
        {
            //BrightText.text = $"Color_value:{args.lightEstimation.mainLightColor.Value}";
            MyLight.color = args.lightEstimation.mainLightColor.Value;
            float AvgBrightness = 0.2126f * MyLight.color.r + 0.7152f * MyLight.color.g * MyLight.color.b;
            BrightText.text = AvgBrightness.ToString();
        }
    }

}
