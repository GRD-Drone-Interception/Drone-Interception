using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public InputActions inputActions;
    private InputActionMap currentActionMap;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning($"There should only be one instance of {Instance.GetType()}");
        }

        inputActions = new InputActions();
        currentActionMap = inputActions.UnitDeployment;
    }

    public void SetActiveActionMap(InputActionMap activeActionMap)
    {
        if(currentActionMap != null)
        {
            currentActionMap.Disable();
        }
        currentActionMap = activeActionMap;
        activeActionMap.Enable();
    }
    
    public InputActionMap GetActiveActionMap()
    {
        return currentActionMap;
    }

    void Update()
    {
        
    }
}
