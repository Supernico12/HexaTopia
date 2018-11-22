using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{

    public Resource resourceToAdd;
    UIController uIController;



    public void OnFocus()
    {
        uIController.SetResourceDescription(name, resourceToAdd);
    }

    public void OnDisFocus()
    {
        uIController.CloseUnitDescriptions();
    }

    void Start()
    {
        uIController = UIController.instance;
    }

}
