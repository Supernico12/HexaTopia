using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitSlot : MonoBehaviour
{


    int index;
    TextMeshProUGUI text;

    public void SetContent(Unit unit)
    {
        text.text = unit.name;
    }
}
