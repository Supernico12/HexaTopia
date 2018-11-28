using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{

    UIController uIController;
    public virtual void SetContent(Resource res)
    {
        uIController = UIController.instance;
        TextMeshProUGUI[] text = GetComponentsInChildren<TextMeshProUGUI>();
        Image[] sprites = GetComponentsInChildren<Image>();
        sprites[1].sprite = uIController.GetResourceSprite(res);
        text[1].text = res.GetResourceCost().ToString();
    }


}
