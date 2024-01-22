using UnityEngine;

public class ButtonParam : MonoBehaviour
{
    [HideInInspector] public int param;

    CanvasScroll canvas;

    private void Start()
    {
        canvas = CanvasScroll.instance;
    }

    public void CreateItem()
    {
        GameObject go = Instantiate(canvas.SwitchItemTypeGO(canvas.ItemType)[param], new Vector3(0f, canvas.SwitchItemTypeGO(canvas.ItemType)[param].transform.position.y, 0f), canvas.SwitchItemTypeGO(canvas.ItemType)[param].transform.rotation);
        ObjectInteractionManager.instance.ChangeSelectedObject(go.transform);
    }
}
