using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemStruct Item_Struct;
    private Vector3 offset;

    private void Start()
    {
        BoxCollider col = transform.GetComponent<BoxCollider>();
        offset = new Vector3(0, (col.bounds.max.y / 2) + 2f, 0);
    }

    public void ShowLikeButton(GameObject likeObject)
    {
        likeObject.transform.position = gameObject.transform.position + offset;
        likeObject.SetActive(true);
    }

    public void AttachOnAvatar()
    {
        gameObject.transform.position = GameObject.Find("FemaleDummy").transform.position;
        gameObject.tag = "AttachedItem";
    }
}
