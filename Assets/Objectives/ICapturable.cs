using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjetiveCaptureState
{
    Neutral,
    Capturing,
    Contested,
    Captured,
}

public interface ICapturable
{

    int DefendersOnPoint { get; }
    int AttackersOnPoint { get; }

    PlayerTeam ControllingTeam { get; }

    ObjetiveCaptureState CaptureState { get; }

    void Capture(PlayerTeam capturingTeam);
}
