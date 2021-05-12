using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObject;
    public Text deltaDiff;
    public Text pinch;
    public Text pos;


    private float rotationRate = 0.15f;
    private float scaleRate = -0.15f;

    private float rotateY;

    private bool isPlace = false;


    GameObject spawnObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // updateCenterObject();
        if (!isPlace)placeObjectByTouch();
        rotateObject();
        resizeObjectByTouch();

    }

    private void placeObjectByTouch()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if(arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
            {
                Pose hitPose = hits[0].pose;

              
                placeObject.SetActive(true);
                placeObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);

                isPlace = true;
               
                //if(!spawnObject) spawnObject = Instantiate(placeObject, hitPose.position, hitPose.rotation);
                //else
                //{
                //    spawnObject.transform.position = hitPose.position;
                //    spawnObject.transform.rotation = hitPose.rotation;
                //}
            }

        }

    }
    private void rotateObject()
    {
        if (Input.touchCount >= 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.phase == TouchPhase.Moved && touchOne.phase == TouchPhase.Moved)
            {
                rotateY = (touchOne.deltaPosition.x + touchZero.deltaPosition.x) / 2;

                placeObject.transform.Rotate(0,
                    -rotateY * rotationRate, 0, Space.World);
            }
        }

    }

    private void resizeObjectByTouch()
    {
        if (Input.touchCount == 2)
        {
             float originScale = GameObject.FindWithTag("OriginModel").transform.localScale.x;
            // Get Touch points.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Get prev object scale.
            Vector3 prevScale = placeObject.transform.localScale;

            float rate = (prevScale.x) * 1000f;
            // Calculate pinch amount with max, min.
            float pinchAmount = Mathf.Clamp(prevScale.x + deltaMagnitudeDiff * (-Time.deltaTime), originScale/2, originScale);
            deltaDiff.text = rate.ToString();
            //pinch.text = pinchAmount.ToString();
            //pos.text = placeObject.transform.localScale.x.ToString();

           

            // Set new scale. 
            Vector3 newScale = new Vector3(pinchAmount, pinchAmount, pinchAmount);
            placeObject.transform.localScale = Vector3.Lerp(prevScale, newScale, Time.deltaTime*rate);

            //deltaDiff.text = (51.8f * placeObject.transform.localScale.x * 100).ToString();
            //pinch.text = (77.3f * placeObject.transform.localScale.y * 100).ToString();
            //pos.text = (53.0f * placeObject.transform.localScale.z * 100).ToString();




        }

    }

    private void updateCenterObject()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if(hits.Count > 0)
        {
            Pose placementPose = hits[0].pose;
            placeObject.SetActive(true);
            placeObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placeObject.SetActive(false);
        }
    }
}
