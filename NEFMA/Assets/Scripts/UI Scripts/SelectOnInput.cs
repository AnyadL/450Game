using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected = false;

    // Use this for initialization
    void Start()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
        buttonSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}