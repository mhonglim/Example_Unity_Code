using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderLP : MonoBehaviour
{
    [SerializeField] private Slider MySlider;
    [SerializeField] private TextMeshProUGUI FloorText;

    //Powerhouse
    [SerializeField] private GameObject Left_Pier;
    [SerializeField] private GameObject Right_Pier;
    [SerializeField] private GameObject Object006;
    [SerializeField] private GameObject Powerhouse;
    [SerializeField] private GameObject Roof001;
    [SerializeField] private GameObject Roof002;

    //Spillway
    [SerializeField] private GameObject Spillway;
    //Navigation Lock
    [SerializeField] private GameObject Navigation_Lock;
    //RCC
    [SerializeField] private GameObject Generic_Models_Cover_rc01;
    [SerializeField] private GameObject Object005;
    [SerializeField] private GameObject Railings_Handrail;
    [SerializeField] private GameObject Walls;
    [SerializeField] private GameObject Walls_Fuse_Plug;
    [SerializeField] private GameObject Walls_Top_Concrete;
    [SerializeField] private GameObject Walls_Walls_1;
    [SerializeField] private GameObject Walls_Walls_4;

    //Panels
    [SerializeField] private GameObject Content1;
    [SerializeField] private GameObject Content2;
    [SerializeField] private GameObject Content3;
    [SerializeField] private GameObject Content4;

    public Material[] MyMaterials;
    Renderer Rend;

    // Start is called before the first frame update
    void Start()
    {
        Left_Pier.GetComponent<Renderer>().enabled = true;
        Right_Pier.GetComponent<Renderer>().enabled = true;
        Object006.GetComponent<Renderer>().enabled = true;
        Powerhouse.GetComponent<Renderer>().enabled = true;
        Roof001.GetComponent<Renderer>().enabled = true;
        Roof002.GetComponent<Renderer>().enabled = true;
        Spillway.GetComponent<Renderer>().enabled = true;
        Navigation_Lock.GetComponent<Renderer>().enabled = true;
        Generic_Models_Cover_rc01.GetComponent<Renderer>().enabled = true;
        Object005.GetComponent<Renderer>().enabled = true;
        Railings_Handrail.GetComponent<Renderer>().enabled = true;
        Walls.GetComponent<Renderer>().enabled = true;
        Walls_Fuse_Plug.GetComponent<Renderer>().enabled = true;
        Walls_Top_Concrete.GetComponent<Renderer>().enabled = true;
        Walls_Walls_1.GetComponent<Renderer>().enabled = true;
        Walls_Walls_4.GetComponent<Renderer>().enabled = true;

        Left_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Right_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Object006.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Powerhouse.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Roof001.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Roof002.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Spillway.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Navigation_Lock.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Generic_Models_Cover_rc01.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Object005.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Railings_Handrail.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Walls.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Walls_Fuse_Plug.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Walls_Top_Concrete.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Walls_Walls_1.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        Walls_Walls_4.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];


        MySlider.onValueChanged.AddListener((v) =>
        {
            FloorText.text = "Floor " + v.ToString("0");
        });
    }

    // Update is called once per frame
    void Update()
    {
        //FloorText.text = FloorSlider.value.ToString();
        //Powerhouse
        if (MySlider.value == 1)
        {
            Content1.SetActive(true);
            Content2.SetActive(false);
            Content3.SetActive(false);
            Content4.SetActive(false);

            Left_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Right_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Object006.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Powerhouse.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Roof001.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Roof002.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];

            Spillway.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Navigation_Lock.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Generic_Models_Cover_rc01.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Object005.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Railings_Handrail.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Fuse_Plug.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Top_Concrete.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Walls_1.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Walls_4.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        }
        //Spillway
        else if (MySlider.value == 2)
        {
            Content1.SetActive(false);
            Content2.SetActive(true);
            Content3.SetActive(false);
            Content4.SetActive(false);

            Left_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Right_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Object006.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Powerhouse.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Roof001.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Roof002.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];

            Spillway.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];

            Navigation_Lock.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Generic_Models_Cover_rc01.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Object005.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Railings_Handrail.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Fuse_Plug.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Top_Concrete.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Walls_1.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Walls_4.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        }
        //RCC
        else if (MySlider.value == 3)
        {
            Content1.SetActive(false);
            Content2.SetActive(false);
            Content3.SetActive(true);
            Content4.SetActive(false);

            Left_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Right_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Object006.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Powerhouse.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Roof001.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Roof002.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Spillway.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Navigation_Lock.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];

            Generic_Models_Cover_rc01.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Object005.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Railings_Handrail.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Walls.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Walls_Fuse_Plug.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Walls_Top_Concrete.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Walls_Walls_1.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];
            Walls_Walls_4.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];

        }
        //Navigation Lock
        else if (MySlider.value == 4)
        {
            Content1.SetActive(false);
            Content2.SetActive(false);
            Content3.SetActive(false);
            Content4.SetActive(true);

            Left_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Right_Pier.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Object006.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Powerhouse.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Roof001.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Roof002.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Spillway.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];

            Navigation_Lock.GetComponent<Renderer>().sharedMaterial = MyMaterials[1];

            Generic_Models_Cover_rc01.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Object005.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Railings_Handrail.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Fuse_Plug.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Top_Concrete.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Walls_1.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
            Walls_Walls_4.GetComponent<Renderer>().sharedMaterial = MyMaterials[0];
        }
    }
}
