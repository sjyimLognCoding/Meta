using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cinemachine;

public class ObjectInteractionManager : MonoBehaviour
{
    public static ObjectInteractionManager instance { get; private set; }

    Camera mainCam;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    CinemachinePOV pov;
    CinemachineFramingTransposer framingTransposer;


    [Header("Note")]
    [SerializeField] Canvas canvas;
    [SerializeField] Slider rotationSlider;
    float WorldToScreenSpaceY;

    Transform selectedObject;

    Vector3 positionOffset;
    float touchTime, positionDelta;
    float positionThreshold = 30f;
    float touchTimeThreshold = 0.3f;

    Vector2 initialPosition, touchPrevPosition;

    Vector2 inputTwoInitialOne, inputTwoInitialTwo;

    bool changeObjectFlag;

    bool isrot;
    bool UI_flag = false;
    bool isRotating
    {
        get => isrot;
        set
        {
            isrot = value;
            if (value)
            {
                rotationSlider.gameObject.SetActive(true);
                WorldToScreenSpaceY = FindScreenSpace(selectedObject, true).y / 1.5f;
                rotationSlider.transform.position = mainCam.WorldToScreenPoint(selectedObject.transform.position) + new Vector3(0, WorldToScreenSpaceY, 0);

                rotationSlider.value = selectedObject.eulerAngles.y;

                selectedObject.GetComponent<Outline>().enabled = false;
            }
            else
            {
                rotationSlider.gameObject.SetActive(false);
                selectedObject.GetComponent<Outline>().enabled = true;
            }
        }
    }
    RaycastHit hit_note, hit_selectObject;

    float camera_sensitivity = 0.05f;

    float zoomMin = 1f;
    float zoomMax = 7f;


    bool touchCount2_updateInitialPosition = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
    }

    private void Start()
    {
        mainCam = Camera.main;

        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.I))
        {
            framingTransposer.m_CameraDistance -= 0.25f;
        }

        if (Input.GetKey(KeyCode.O))
        {
            framingTransposer.m_CameraDistance += 0.25f;
        }
#endif
        if (rotationSlider.gameObject.activeInHierarchy)
        {
            rotationSlider.transform.position = mainCam.WorldToScreenPoint(selectedObject.position) + new Vector3(0, WorldToScreenSpaceY, 0);
        }

        if (Input.touchCount == 0)
        {
            touchTime = 0f;
            positionDelta = 0f;
            changeObjectFlag = false;
            UI_flag = false;
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                if (IsPointerOverUI(touch)) UI_flag = true;

                if (selectedObject != null)
                {
                    positionOffset = selectedObject.position - GetMouseWorldPos(selectedObject);
                }
                initialPosition = touch.position;

                positionDelta = 0f;
                touchPrevPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (IsPointerOverUI(touch)) return;

                if (touchTime < touchTimeThreshold && !changeObjectFlag)
                {
                    bool result = Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit_note);
                    if (result && positionDelta < positionThreshold && null != selectedObject)
                    {
                        if (hit_note.transform.CompareTag("Item"))
                        {
                            isRotating = !isRotating;
                        }
                    }

                    if ((selectedObject != null && (initialPosition - touch.position).magnitude < positionThreshold) && (!hit_note.transform.CompareTag("Item") || !result))
                    {
                        selectedObject.GetComponent<Outline>().enabled = false;
                        selectedObject = null;
                        rotationSlider.gameObject.SetActive(false);
                        return;
                    }
                }
            }

            else
            {
                if (UI_flag) return;

                // RaycastHit tempHit;

                if (Physics.Raycast(mainCam.ScreenPointToRay(touch.position), out hit_selectObject) && !changeObjectFlag)
                {
                    if (hit_selectObject.transform.tag == "Item")
                    {
                        touchTime += Time.deltaTime;
                    }

                    else
                    {
                        touchTime = 0;
                    }
                }

                positionDelta += (touchPrevPosition - touch.position).magnitude;

                if (touchTime > touchTimeThreshold && positionDelta < positionThreshold && !changeObjectFlag)
                {
                    if (Physics.Raycast(mainCam.ScreenPointToRay(touch.position), out hit_selectObject))
                    {
                        if (hit_selectObject.transform.CompareTag("Item"))
                        {
                            ChangeSelectedObject(hit_selectObject.transform);
                            // OpenNote();
                            positionOffset = selectedObject.position - GetMouseWorldPos(selectedObject);
                            changeObjectFlag = true;
                        }
                    }
                }

                if (selectedObject != null && !isRotating)
                {
                    Vector3 translate = GetMouseWorldPos(selectedObject) + positionOffset;
                    selectedObject.position = new Vector3(translate.x, selectedObject.position.y, translate.z);
                }

                // if (selectedObject == null && positionDelta > positionThreshold && !isRotating)
                if ((selectedObject == null && positionDelta > positionThreshold && !isRotating) || (positionDelta > positionThreshold && isRotating))
                {
                    pov.m_HorizontalAxis.Value += touch.deltaPosition.x * camera_sensitivity;
                    pov.m_VerticalAxis.Value -= touch.deltaPosition.y * camera_sensitivity;
                    // framingTransposer.m_TrackedObjectOffset = Vector3.Lerp(framingTransposer.m_TrackedObjectOffset, Vector3.zero, (framingTransposer.transform.position - virtualCamera.m_Follow.position).magnitude * Time.deltaTime);
                }

                touchPrevPosition = touch.position;
            }
        }

        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (!touchCount2_updateInitialPosition)
            {
                inputTwoInitialOne = touch1.position;
                inputTwoInitialTwo = touch2.position;

                touchCount2_updateInitialPosition = true;
            }

            float dotProduct = Vector2.Dot(touch1.position - inputTwoInitialOne, touch2.position - inputTwoInitialTwo);

            if (dotProduct < 0)
            {
                Vector2 touch1Pos = touch1.position - touch1.deltaPosition;
                Vector2 touch2Pos = touch2.position - touch2.deltaPosition;

                float prevMag = (touch1Pos - touch2Pos).magnitude;
                float currentMag = (touch1.position - touch2.position).magnitude;
                float diff = currentMag - prevMag;

                framingTransposer.m_CameraDistance -= diff * 0.01f;
                framingTransposer.m_CameraDistance = Mathf.Clamp(framingTransposer.m_CameraDistance, zoomMin, zoomMax);

            }
        }
    }

    private Vector3 GetMouseWorldPos(Transform t)
    {
        Vector3 pos = Input.mousePosition;
        pos.z = mainCam.WorldToScreenPoint(t.position).z;

        return mainCam.ScreenToWorldPoint(pos);
    }

    public void ChangeSelectedObject(Transform go)
    {
        if (selectedObject != null) selectedObject.GetComponent<Outline>().enabled = false;

        if (selectedObject != null && (selectedObject == go || !selectedObject.CompareTag("Item"))) return;

        selectedObject = go;
        isRotating = false;
        selectedObject.GetComponent<Outline>().enabled = true;
    }

    public void ChangeObjectRotation()
    {
        selectedObject.eulerAngles = new Vector3(selectedObject.eulerAngles.x, rotationSlider.value, selectedObject.eulerAngles.z);
    }

    private bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = new Vector2(touch.position.x, touch.position.y);

        List<RaycastResult> results = new List<RaycastResult>();
        if (!EventSystem.current) return false;
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public Vector3 FindScreenSpace(Transform paramGO, bool findMax)
    {
        Bounds GOBounds = paramGO.GetComponent<BoxCollider>().bounds;
        // else GOBounds = paramGO.transform.GetChild(0).GetComponent<Renderer>().bounds;
        Vector3[] corners = new Vector3[8];

        corners[0] = Camera.main.WorldToScreenPoint(new Vector3(GOBounds.center.x + GOBounds.extents.x, GOBounds.center.y + GOBounds.extents.y, GOBounds.center.z + GOBounds.extents.z));
        corners[1] = Camera.main.WorldToScreenPoint(new Vector3(GOBounds.center.x + GOBounds.extents.x, GOBounds.center.y + GOBounds.extents.y, GOBounds.center.z - GOBounds.extents.z));
        corners[2] = Camera.main.WorldToScreenPoint(new Vector3(GOBounds.center.x + GOBounds.extents.x, GOBounds.center.y - GOBounds.extents.y, GOBounds.center.z + GOBounds.extents.z));
        corners[3] = Camera.main.WorldToScreenPoint(new Vector3(GOBounds.center.x + GOBounds.extents.x, GOBounds.center.y - GOBounds.extents.y, GOBounds.center.z - GOBounds.extents.z));
        corners[4] = Camera.main.WorldToScreenPoint(new Vector3(GOBounds.center.x - GOBounds.extents.x, GOBounds.center.y + GOBounds.extents.y, GOBounds.center.z + GOBounds.extents.z));
        corners[5] = Camera.main.WorldToScreenPoint(new Vector3(GOBounds.center.x - GOBounds.extents.x, GOBounds.center.y + GOBounds.extents.y, GOBounds.center.z - GOBounds.extents.z));
        corners[6] = Camera.main.WorldToScreenPoint(new Vector3(GOBounds.center.x - GOBounds.extents.x, GOBounds.center.y - GOBounds.extents.y, GOBounds.center.z + GOBounds.extents.z));
        corners[7] = Camera.main.WorldToScreenPoint(new Vector3(GOBounds.center.x - GOBounds.extents.x, GOBounds.center.y - GOBounds.extents.y, GOBounds.center.z - GOBounds.extents.z));

        float minX = corners[0].x;
        float minY = corners[0].y;
        float maxX = corners[0].x;
        float maxY = corners[0].y;
        float minZ = corners[0].z;
        float maxZ = corners[0].z;

        for (int i = 1; i < 8; i++)
        {
            if (corners[i].x < minX) minX = corners[i].x;
            if (corners[i].y < minY) minY = corners[i].y;
            if (corners[i].x > maxX) maxX = corners[i].x;
            if (corners[i].y > maxY) maxY = corners[i].y;
            if (corners[i].z > maxZ) maxZ = corners[i].z;
            if (corners[i].z < minZ) minZ = corners[i].z;
        }
        float minScale = Mathf.Min(maxY - minY, maxX - minX, maxZ - minZ);
        float maxScale = Mathf.Max(maxX - minX, maxZ - minZ, maxY - minY);
        // return new Vector3(scale, scale, );
        if (!findMax)
        {
            return new Vector3(minScale, minScale, minScale);
        }
        else
        {
            return new Vector3(maxScale, maxScale, maxScale);
        }
    }
}
