using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{

    UIController uIController;
    protected Button infoButton;
    public virtual void SetContent(Resource res)
    {
        uIController = UIController.instance;
        TextMeshProUGUI[] text = GetComponentsInChildren<TextMeshProUGUI>();
        Image[] sprites = GetComponentsInChildren<Image>();
        infoButton = transform.Find("Info").GetComponent<Button>();
        infoButton.onClick.AddListener(OnInfoButton);
        //info.gameObject.SetActive(true);
        sprites[1].sprite = uIController.GetResourceSprite(res);
        text[1].text = res.GetResourceCost().ToString();
    }

    public virtual void OnInfoButton()
    {

    }


}
