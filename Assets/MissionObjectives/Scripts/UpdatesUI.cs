using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdatesUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void OnEnable()
    {
        CapturableObjective.OnCaptured += CapturableObjective_OnObjectiveCaptured;
        DestructableObjective.OnDestroyed += DestructableObjective_OnDestroyed;
    }

    private void DestructableObjective_OnDestroyed()
    {
        //Debug.Log("An objective has been destroyed");
        text.text = "An objective has been destroyed";
    }

    private void CapturableObjective_OnObjectiveCaptured(PlayerTeam team)
    {
        //Debug.Log($"An objective has been captured by {team}");
        text.text = $"An objective has been captured by the {team} team";
    }

    private void OnDisable()
    {
        CapturableObjective.OnCaptured -= CapturableObjective_OnObjectiveCaptured;
        DestructableObjective.OnDestroyed -= DestructableObjective_OnDestroyed;
    }


}
