using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementController : MonoBehaviour
{

    [SerializeField]
    private GameObject placedPrefab;
    private int counter=0;
    private PlacementObject lastSelectedObject;
    public ARSessionOrigin aRSessionOrigin;

    [SerializeField]
    private Slider scaleSlider;

    private GameObject statue;

    private ARPlaneManager aRplaneManager;
  [SerializeField]
      private Button togglePlaneDetection;

private ARPointCloudManager pCloudManager;



    public GameObject PlacedPrefab
    {
        get 
        {
            return placedPrefab;
        }
        set 
        {
            placedPrefab = value;
        }
    }

    private ARRaycastManager arRaycastManager;

    void Awake() 
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        aRSessionOrigin = GetComponent<ARSessionOrigin>();
        aRplaneManager = GetComponent<ARPlaneManager>();
        pCloudManager = GetComponent<ARPointCloudManager>();
        scaleSlider.onValueChanged.AddListener(ScaleChanged);
        togglePlaneDetection.onClick.AddListener(ToggleDetection);

 
    }

    private void ToggleDetection(){
        aRplaneManager.enabled = !aRplaneManager.enabled;
        pCloudManager.pointCloudPrefab.gameObject.SetActive(false);
        scaleSlider.gameObject.SetActive(false);
        togglePlaneDetection.gameObject.SetActive(false);

        foreach(ARPlane plane in aRplaneManager.trackables){
            plane.gameObject.SetActive(aRplaneManager.enabled);
        }
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;

        return false;
    }

    void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if(arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            if(counter<1){
                var hitPose = hits[0].pose;
                statue = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                counter++;
            }
        }
    }

    private void ScaleChanged(float newValue)
    {
        //if(applyScalingPerObject){
            //if(lastSelectedObject != null && lastSelectedObject.Selected)
            //{
            statue.transform.localScale = Vector3.one * newValue;
           // }
        //}
        //else 
            //aRSessionOrigin.transform.localScale = Vector3.one * newValue;

        //scaleTextValue.text = $"Scale {newValue}";
    }
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}
