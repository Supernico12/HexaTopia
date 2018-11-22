using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{

    [SerializeField] GameObject txtPrefab;
    [SerializeField] Vector3 offset;


    Transform txtTransform;
    Transform parent;

    Transform cam;
    // Use this for initialization
    void Start()
    {


        if (offset == Vector3.zero)
        {
            offset = new Vector3(0f, -0.2f, -0.5f);
        }


    }

    public void CreateText(GameObject newPrefab)
    {
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                parent = c.transform.Find("HealthUI");


                break;
            }
        }

        cam = Camera.main.transform;
        txtTransform = Instantiate(newPrefab, parent).transform;

        OnChangePosition();

    }
    public void OnChangePosition()
    {
        txtTransform.position = transform.position + offset;
        txtTransform.forward = cam.forward;
    }

    public void OnChangeValue(float newValue)
    {
        if (txtTransform == null)
        {
            CreateText(txtPrefab);
            OnChangePosition();

        }
        TextMeshPro txt = txtTransform.GetComponent<TextMeshPro>();
        txt.text = newValue.ToString();


    }

    public void OnDestroyed()
    {
        Destroy(txtTransform.gameObject);
    }


}
