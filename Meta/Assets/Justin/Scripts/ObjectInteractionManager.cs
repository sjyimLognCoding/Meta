using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    float touchThreshold = 0.3f;
    Transform selectedObject;
    RaycastHit hit_selectObject;
    RaycastHit hit_floor;

    int itemLayerNumber = 6;
    int floorLayerNumber = 7;
    Vector3 objectPositionOffset;

    Vector3 initialClickPoint;
    Vector3 mouseInitialPosition;

    float mousePositionDelta;
    float mousePositionThreshold = 10f;

    bool changeObjectFlag = false;

    private void Start()
    {
        mainCam = Camera.main;
    }


    Vector3 positionOffset;



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject != null)
            {
                positionOffset = selectedObject.position - GetMouseWorldPos(selectedObject);
            }
        }

        if (Input.GetMouseButton(0))
        {
            touchTime += Time.deltaTime;

            mouseInitialPosition = Input.mousePosition;
            mousePositionDelta += (mouseInitialPosition - Input.mousePosition).magnitude;

            if (touchTime > touchThreshold && mousePositionDelta < mousePositionThreshold && !changeObjectFlag)
            {
                if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit_selectObject))
                {
                    if (hit_selectObject.transform.CompareTag("Item"))
                    {
                        ChangeSelectedObject(hit_selectObject.transform);
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

        if (Input.GetMouseButtonUp(0))
        {
            if (touchTime < touchThreshold)
            {
                if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit_note))
                {
                    if (hit_note.transform.CompareTag("Item"))
                    {
                        //! 노트 UI 활성화
                        if (!itemNoteEditField.gameObject.activeInHierarchy || !closeNoteButton.gameObject.activeInHierarchy)
                        {
                            itemNoteEditField.gameObject.SetActive(true);
                            closeNoteButton.gameObject.SetActive(true);
                            itemTypeText.gameObject.SetActive(true);
                            itemPriceText.gameObject.SetActive(true);
                        }

                        else
                        {
                            hitTransform.GetComponent<NoteHolder>().myNote.ThisNote = itemNoteEditField.text;
                        }

                        hitTransform = hit_note.transform;
                        NoteHolder hitNoteHolder = hitTransform.GetComponent<NoteHolder>();

                        // todo : retrieve the note info held by the item
                        // - apply note text to item note field
                        itemNoteEditField.text = hitNoteHolder.myNote.ThisNote;
                        itemTypeText.text = hitNoteHolder.myNote.ItemType;
                        itemPriceText.text = "$" + hitNoteHolder.myNote.Price.ToString();
                    }
                }
            }

            touchTime = 0f;
            mousePositionDelta = 0f;
            changeObjectFlag = false;
        }
    }


    public void CloseNote()
    {
        hitTransform.GetComponent<NoteHolder>().myNote.ThisNote = itemNoteEditField.text;

        itemNoteEditField.gameObject.SetActive(false);
        closeNoteButton.gameObject.SetActive(false);
        itemTypeText.gameObject.SetActive(false);
        itemPriceText.gameObject.SetActive(false);
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
}
