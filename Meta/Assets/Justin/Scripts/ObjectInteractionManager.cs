using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ObjectInteractionManager : MonoBehaviour
{
    RaycastHit hit_note;
    Camera mainCam;

    [SerializeField] TMP_InputField itemNoteEditField;
    [SerializeField] Button closeNoteButton;
    [SerializeField] TextMeshProUGUI itemTypeText;
    [SerializeField] TextMeshProUGUI itemPriceText;

    Transform hitTransform;

    float touchTime = 0f;
    float touchTimeThreshold = 0.3f;
    Transform selectedObject;
    RaycastHit hit_selectObject;
    RaycastHit hit_floor;

    int itemLayerNumber = 6;
    int floorLayerNumber = 7;
    Vector3 objectPositionOffset;

    Vector2 touchPrevPosition;
    float positionDelta;
    float positionThreshold = 10f;

    bool changeObjectFlag = false;


    [SerializeField] Image moveImage;
    [SerializeField] Slider rotateSlider;
    bool ignoreDrag;
    public bool MoveRotateSwitch
    {
        get => _switch;
        private set
        {
            _switch = value;
            if (value)
            {
                moveImage.gameObject.SetActive(true);
                rotateSlider.gameObject.SetActive(false);
            }
            else if (!value)
            {
                moveImage.gameObject.SetActive(false);
                rotateSlider.gameObject.SetActive(true);
                rotateSlider.value = selectedObject.transform.eulerAngles.y;
            }
        }
    }
    bool _switch;

    private void Start()
    {
        mainCam = Camera.main;

        moveImage.gameObject.SetActive(false);
        rotateSlider.gameObject.SetActive(false);
    }


    Vector3 positionOffset;

    Vector2 initialPosition;

    void Update()
    {
        if (selectedObject)
        {
            if (MoveRotateSwitch)
            {
                moveImage.transform.position = new Vector3(mainCam.WorldToScreenPoint(selectedObject.position).x, mainCam.WorldToScreenPoint(selectedObject.position).y);
                moveImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, FindScreenSpace(selectedObject, false).x);
                moveImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, FindScreenSpace(selectedObject, false).y);
            }

            else if (!MoveRotateSwitch)
            {
                rotateSlider.transform.position = mainCam.WorldToScreenPoint(selectedObject.position) + new Vector3(0, FindScreenSpace(selectedObject, true).y, 0);
            }
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                if (selectedObject != null)
                {
                    positionOffset = selectedObject.position - GetMouseWorldPos(selectedObject);
                }
                initialPosition = touch.position;

                if (IsPointerOverUI(touch)) ignoreDrag = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (IsPointerOverUI(touch)) return;
                if (touchTime < touchTimeThreshold && !changeObjectFlag)
                {
                    bool result = Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit_note);
                    if (result)
                    {
                        if (hit_note.transform.CompareTag("Item"))
                        {
                            if (!itemNoteEditField.gameObject.activeInHierarchy || !closeNoteButton.gameObject.activeInHierarchy)
                            {
                                OpenNote();
                            }

                            else
                            {
                                hitTransform.GetComponent<ItemData>().note = itemNoteEditField.text;
                            }

                            hitTransform = hit_note.transform;
                            ItemData data = hitTransform.GetComponent<ItemData>();

                            itemNoteEditField.text = data.note;
                            itemTypeText.text = data.type;
                            itemPriceText.text = "$" + data.price.ToString();
                        }
                    }

                    if ((selectedObject != null && (initialPosition - touch.position).magnitude < positionThreshold) && (!hit_note.transform.CompareTag("Item") || !result))
                    {
                        selectedObject = null;
                        CloseNote();
                        return;
                    }
                }

                touchTime = 0f;
                positionDelta = 0f;
                changeObjectFlag = false;
                ignoreDrag = false;

            }

            else
            {
                if (ignoreDrag) return;

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

                touchPrevPosition = touch.position;
                positionDelta += (touchPrevPosition - touch.position).magnitude;


                if (touchTime > touchTimeThreshold && positionDelta < positionThreshold && !changeObjectFlag)
                {
                    if (Physics.Raycast(mainCam.ScreenPointToRay(touch.position), out hit_selectObject))
                    {
                        if (hit_selectObject.transform.CompareTag("Item"))
                        {
                            ChangeSelectedObject(hit_selectObject.transform);
                            OpenNote();
                            positionOffset = selectedObject.position - GetMouseWorldPos(selectedObject);
                            changeObjectFlag = true;
                        }
                    }
                }

                if (selectedObject != null)
                {
                    Vector3 translate = GetMouseWorldPos(selectedObject) + positionOffset;
                    selectedObject.position = new Vector3(translate.x, selectedObject.position.y, translate.z);
                }
            }
        }
    }

    private void CloseNote()
    {
        itemNoteEditField.gameObject.SetActive(false);
        closeNoteButton.gameObject.SetActive(false);
        itemTypeText.gameObject.SetActive(false);
        itemPriceText.gameObject.SetActive(false);

        moveImage.gameObject.SetActive(false);
        rotateSlider.gameObject.SetActive(false);
    }

    private void OpenNote()
    {
        itemNoteEditField.gameObject.SetActive(true);
        closeNoteButton.gameObject.SetActive(true);
        itemTypeText.gameObject.SetActive(true);
        itemPriceText.gameObject.SetActive(true);
    }

    public void Rotate()
    {
        selectedObject.transform.eulerAngles = new Vector3(selectedObject.transform.eulerAngles.x, rotateSlider.value, selectedObject.transform.eulerAngles.z);
    }

    private Vector3 GetMouseWorldPos(Transform t)
    {
        Vector3 pos = Input.mousePosition;
        pos.z = mainCam.WorldToScreenPoint(t.position).z;

        return mainCam.ScreenToWorldPoint(pos);
    }

    private void ChangeSelectedObject(Transform go)
    {
        if (selectedObject != null && (selectedObject == go || !selectedObject.CompareTag("Item"))) return;

        selectedObject = go;

        MoveRotateSwitch = true;

        moveImage.gameObject.SetActive(true);
        rotateSlider.gameObject.SetActive(false);
    }

    public void ChangeMoveRotateState()
    {
        MoveRotateSwitch = MoveRotateSwitch == true ? false : true;
        print(MoveRotateSwitch);
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
        Bounds GOBounds = paramGO.GetComponent<Renderer>().bounds;
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
            if (corners[i].x < minX)
            {
                minX = corners[i].x;
            }
            if (corners[i].y < minY)
            {
                minY = corners[i].y;
            }
            if (corners[i].x > maxX)
            {
                maxX = corners[i].x;
            }
            if (corners[i].y > maxY)
            {
                maxY = corners[i].y;
            }
            if (corners[i].z > maxZ)
            {
                maxZ = corners[i].z;
            }
            if (corners[i].z < minZ)
            {
                minZ = corners[i].z;
            }
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