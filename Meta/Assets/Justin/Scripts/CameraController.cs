using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class CameraController : MonoBehaviour
{
    RaycastHit hit;

    Camera mainCam;

    [SerializeField] TMP_InputField itemNoteEditField;
    [SerializeField] Button closeNoteButton;
    [SerializeField] TextMeshProUGUI itemTypeText;
    [SerializeField] TextMeshProUGUI itemPriceText;

    Transform hitTransform;

    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.CompareTag("Item"))
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

                    hitTransform = hit.transform;
                    NoteHolder hitNoteHolder = hitTransform.GetComponent<NoteHolder>();

                    // todo : retrieve the note info held by the item
                    // - apply note text to item note field
                    itemNoteEditField.text = hitNoteHolder.myNote.ThisNote;
                    itemTypeText.text = hitNoteHolder.myNote.ItemType;
                    itemPriceText.text = "$" + hitNoteHolder.myNote.Price.ToString();
                }
            }
        }

        //todo : add camera controls with wasd & hold right click to rotate the camera

    }


    public void CloseNote()
    {
        hitTransform.GetComponent<NoteHolder>().myNote.ThisNote = itemNoteEditField.text;

        itemNoteEditField.gameObject.SetActive(false);
        closeNoteButton.gameObject.SetActive(false);
        itemTypeText.gameObject.SetActive(false);
        itemPriceText.gameObject.SetActive(false);
    }
}
