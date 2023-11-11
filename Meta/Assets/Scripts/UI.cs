using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public Dropdown dropdown;

    public void GetSelectedValue()
    {
        int selectedIndex = dropdown.value;
        string selectedValue = dropdown.options[selectedIndex].text;

        if (selectedValue == "Chair") 
        {
            //Create Chair
        }
        else if (selectedValue == "Table") 
        {
            //Create Table
        }
    }
}