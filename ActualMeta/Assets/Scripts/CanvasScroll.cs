using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;
using Photon.Pun;

public class CanvasScroll : MonoBehaviour
{
    public static CanvasScroll instance { get; private set; }

    [SerializeField] RectTransform ParentPanel;
    [SerializeField] RectTransform ChildPanel;
    [SerializeField] List<Button> ItemButtons;


    [SerializeField] Button[] ClothesOrFurniture;
    [SerializeField] Button[] ClothesTypeButtons;
    [SerializeField] Button[] FurnitureTypeButtons;
    [SerializeField] Button CloseButton;


    [SerializeField] Sprite[] tableSprite;
    [SerializeField] Sprite[] couchSprite;
    [SerializeField] Sprite[] closetSprite;
    [SerializeField] Sprite[] chairSprite;

    [SerializeField] Sprite[] rackSprite;

    [SerializeField] GameObject[] tableItem;
    [SerializeField] GameObject[] couchItem;
    [SerializeField] GameObject[] closetItem;
    [SerializeField] GameObject[] chairItem;

    [SerializeField] GameObject[] rackItem;

    [SerializeField] ObjectInteractionManager objInteractionManager;

    public string ItemType
    {
        get => itemType;
        set
        {
            itemType = value;
        }
    }

    private string itemType;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
    }

    public GameObject[] SwitchItemTypeGO(string type)
    {
        switch (type)
        {
            case "table":
                return tableItem;
            case "couch":
                return couchItem;
            case "closet":
                return closetItem;
            case "chair":
                return chairItem;
            case "rack":
                return rackItem;
            default:
                return null;
        }
    }

    public Sprite[] SwitchItemTypeSprite(string type)
    {
        switch (type)
        {
            case "table":
                return tableSprite;
            case "couch":
                return couchSprite;
            case "closet":
                return closetSprite;
            case "chair":
                return chairSprite;
            case "rack":
                return rackSprite;
            default:
                return null;
        }
    }

    public void OpenAssetMenu()
    {
        ParentPanel.gameObject.SetActive(false);

        foreach (Button button in FurnitureTypeButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Button button in ClothesTypeButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (Button button in ClothesOrFurniture)
        {
            button.gameObject.SetActive(true);
        }
        CloseButton.gameObject.SetActive(true);
    }

    public void CloseAssetMenu()
    {
        ParentPanel.gameObject.SetActive(false);
        foreach (Button button in FurnitureTypeButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Button button in ClothesTypeButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Button button in ClothesOrFurniture)
        {
            button.gameObject.SetActive(false);
        }
        CloseButton.gameObject.SetActive(false);
    }

    public void OpenClothes()
    {
        foreach (Button button in ClothesTypeButtons)
        {
            button.gameObject.SetActive(true);
        }

        foreach (Button button in ClothesOrFurniture)
        {
            button.gameObject.SetActive(false);
        }

    }
    public void OpenFurniture()
    {
        foreach (Button button in FurnitureTypeButtons)
        {
            button.gameObject.SetActive(true);
        }

        foreach (Button button in ClothesOrFurniture)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void SelectItemType(string type)
    {
        ItemType = type;
        ParentPanel.gameObject.SetActive(true);
        PanelResize();
        ButtonThumbnail();
        foreach (Button button in FurnitureTypeButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Button button in ClothesTypeButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void PanelResize()
    {
        // int count = ItemButtons.Count(obj => obj.gameObject.activeInHierarchy);
        int count = SwitchItemTypeGO(ItemType).Length;

        for (int i = 0; i < count; i++)
        {
            ItemButtons[i].gameObject.SetActive(true);
        }

        for (int i = count; i < ItemButtons.Count; i++)
        {
            ItemButtons[i].gameObject.SetActive(false);
        }

        ChildPanel.sizeDelta = new Vector2(count * 275 + 25, ChildPanel.sizeDelta.y);
    }

    private void ButtonThumbnail()
    {
        int count = ItemButtons.Count(obj => obj.gameObject.activeInHierarchy);

        for (int i = 0; i < count; i++)
        {
            ItemButtons[i].GetComponent<Image>().sprite = SwitchItemTypeSprite(ItemType)[i];
            ItemButtons[i].onClick.RemoveAllListeners();
            ButtonParam param = ItemButtons[i].gameObject.GetComponent<ButtonParam>();
            param.param = i;
            ItemButtons[i].onClick.AddListener(() => param.CreateItem());
        }
    }
}
