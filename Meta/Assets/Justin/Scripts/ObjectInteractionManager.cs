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

    private void Start()
    {
        mainCam = Camera.main;
    }


    Vector3 positionOffset;

    Vector2 initialPosition;

    void Update()
    {
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
            }

            if (touch.phase == TouchPhase.Ended)
            {
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
            }

            else
            {
                RaycastHit tempHit;

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
    }

    private void OpenNote()
    {
        itemNoteEditField.gameObject.SetActive(true);
        closeNoteButton.gameObject.SetActive(true);
        itemTypeText.gameObject.SetActive(true);
        itemPriceText.gameObject.SetActive(true);
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


}