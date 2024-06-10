using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderFloor : MonoBehaviour
{
    [SerializeField] private Slider FloorSlider;
    [SerializeField] private TextMeshProUGUI FloorText;
    [SerializeField] private GameObject Floor1;
    [SerializeField] private GameObject Floor2;
    [SerializeField] private GameObject Floor3;
    [SerializeField] private GameObject Floor4;

    [SerializeField] private GameObject Floor1Opaque;
    [SerializeField] private GameObject Floor2Opaque;
    [SerializeField] private GameObject Floor3Opaque;
    [SerializeField] private GameObject Floor4Opaque;

    // Start is called before the first frame update
    void Start()
    {

        FloorSlider.onValueChanged.AddListener((v) =>
        {
            FloorText.text = "Floor " + v.ToString("0");
        });
    }

    // Update is called once per frame
    void Update()
    {
        FloorText.text = FloorSlider.value.ToString();
        FloorChanged(FloorSlider.value);
    }

    private void FloorChanged(float FloorNumber)
    {
        if (FloorNumber == 1)
        {
            Floor1.SetActive(true);
            Floor2.SetActive(false);
            Floor3.SetActive(false);
            Floor4.SetActive(false);

            Floor1Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            Floor2Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
            Floor3Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
            Floor4Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
        }
        else if (FloorNumber == 2)
        {
            Floor1.SetActive(true);
            Floor2.SetActive(true);
            Floor3.SetActive(false);
            Floor4.SetActive(false);

            Floor1Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
            Floor2Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            Floor3Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
            Floor4Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
        }
        else if (FloorNumber == 3)
        {
            Floor1.SetActive(true);
            Floor2.SetActive(true);
            Floor3.SetActive(true);
            Floor4.SetActive(false);

            Floor1Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
            Floor2Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
            Floor3Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            Floor4Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);

        }
        else if (FloorNumber == 4)
        {
            Floor1.SetActive(true);
            Floor2.SetActive(true);
            Floor3.SetActive(true);
            Floor4.SetActive(true);

            Floor1Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
            Floor2Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
            Floor3Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.2f);
            Floor4Opaque.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
        }
    }
}
