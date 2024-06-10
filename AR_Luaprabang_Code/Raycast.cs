using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Raycast : MonoBehaviour
{
    public GameObject MyCamera;
    public GameObject SpawnPrefab;
    GameObject SpawnObject;
    bool CanSpawn;
    ARRaycastManager ARRCM;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    Vector2 TouchFirst;
    Vector2 TouchSecond;
    float DistanceCurrent;
    float DistancePrevious;
    bool PinchFirst;

    //Scale
    public float ScaleSpeed = 0.01f;
    private bool CanZoomIn;
    private bool CanZoomOut;
    //Rotate
    public float RotateSpeed = 20f;
    private bool CanRotateRight = false;
    private bool CanRotateLeft = false;
    private bool CanMove = false;
    //Move
    public float MoveSpeed = 4f;
    private bool CanMoveForward = false;
    private bool CanMoveBackward = false;

    ARPlaneManager ARPM;

    // Start is called before the first frame update
    void Start()
    {
        PinchFirst = true;
        CanSpawn = false;
        //Move = true;
        ARRCM = GetComponent<ARRaycastManager>();
        ARPM = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 1 && SpawnObject)
        {
            TouchFirst = Input.GetTouch(0).position;
            TouchSecond = Input.GetTouch(1).position;
            DistanceCurrent = TouchSecond.magnitude - TouchFirst.magnitude;

            if (PinchFirst)
            {
                DistancePrevious = DistanceCurrent;
                PinchFirst = false;
            }
            if (DistanceCurrent != DistancePrevious)
            {
                Vector3 ScaleValue = SpawnObject.transform.localScale * (DistanceCurrent / DistancePrevious);
                SpawnObject.transform.localScale = ScaleValue;
                DistancePrevious = DistanceCurrent;
            }
        }
        else
        {
            PinchFirst = true;
        }

        if (Input.touchCount > 0)
        {
            
            if (ARRCM.Raycast(Input.GetTouch(0).position,hits,TrackableType.PlaneWithinPolygon))
            {
                var HitPose = hits[0].pose;
                if(!CanSpawn)
                {
                    SpawnObject = Instantiate(SpawnPrefab,HitPose.position,HitPose.rotation);
                    CanSpawn = true;
                    ARRCM.enabled = false;
                    //ARPM.enabled = false;
                    ARPM.enabled = !ARPM.enabled;
                    foreach (ARPlane Plane in ARPM.trackables)
                    {
                        Plane.gameObject.SetActive(ARPM.enabled);
                    }
                }
                else if (Input.touchCount > 1)
                {
                    SpawnObject.transform.position = HitPose.position;
                    //SpawnObject.transform.rotation = HitPose.rotation;
                }

            }
        }


        if (CanZoomIn)
        {
            SpawnObject.transform.localScale += new Vector3(ScaleSpeed, ScaleSpeed, ScaleSpeed);
        }
        if (CanZoomOut)
        {
            SpawnObject.transform.localScale -= new Vector3(ScaleSpeed, ScaleSpeed, ScaleSpeed);
        }
        if (CanRotateRight)
        {
            SpawnObject.transform.Rotate(Vector3.up, RotateSpeed * Time.deltaTime);
        }
        if (CanRotateLeft)
        {
            SpawnObject.transform.Rotate(Vector3.down, RotateSpeed * Time.deltaTime);
        }
        if(CanMoveForward)
        {
            MyCamera.transform.position += new Vector3(0,0, MoveSpeed) * Time.deltaTime;
        }
        if(CanMoveBackward)
        {
            MyCamera.transform.position += new Vector3(0, 0, -MoveSpeed) * Time.deltaTime;
        }

    }

    public void OnPressZoomIn()
    {
        CanZoomIn = true;
    }

    public void OnReleaseZoomIn()
    {
        CanZoomIn = false;
    }
    public void OnPressZoomOut()
    {
        CanZoomOut = true;
    }

    public void OnReleaseZoomOut()
    {
        CanZoomOut = false;
    }

    public void OnPressRotateRight()
    {
        CanRotateRight = true;
    }
    public void OnReleaseRotateRight()
    {
        CanRotateRight = false;
    }
    public void OnPressRotateLeft()
    {
        CanRotateLeft = true;
    }
    public void OnReleaseRotateLeft()
    {
        CanRotateLeft = false;
    }
    public void OnPressMoveForward()
    {
        CanMoveForward = true;
    }
    public void OnReleaseMoveForward()
    {
        CanMoveForward = false;
    }
    public void OnPressMoveBackward()
    {
        CanMoveBackward = true;
    }
    public void OnReleaseMoveBackward()
    {
        CanMoveBackward = false;
    }

    public void MoveObj()
    {
        if (CanMove == false)
        {
            CanMove = true;
        }
        else
        {
            CanMove = false;
        }
    }
}
