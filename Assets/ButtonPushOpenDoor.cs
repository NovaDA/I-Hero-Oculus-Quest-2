using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPushOpenDoor : MonoBehaviour
{
    #region
    ////public Animator _Animator;
    ////public string _BoolName = "Open";
    ////// Start is called before the first frame update
    //void Start()
    //{
    //    GetComponent<XRSimpleInteractable>().selectEntered.AddListener( x => ToggleDoorOpen());
    //}

    //public void ToggleDoorOpen()
    //{
    //    GameLevelManager.instance.LoadNextLevel();
    //    //bool isOpen = _Animator.GetBool(_BoolName);
    //    //_Animator.SetBool(_BoolName, !isOpen);
    //}
    #endregion

    // Define a UnityEvent
    public UnityEvent onSelectEntered;

    void Start()
    {
        // Ensure the UnityEvent is initialized
        if (onSelectEntered == null)
            onSelectEntered = new UnityEvent();

        // Add a listener to XR interactor's select entered event
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => OnSelectEntered());
    }

    // Function to be called when the XR interactor's select entered event is triggered
    void OnSelectEntered()
    {
        Debug.Log("Select Entered");
        // Invoke the UnityEvent
        onSelectEntered.Invoke();
    }
}
