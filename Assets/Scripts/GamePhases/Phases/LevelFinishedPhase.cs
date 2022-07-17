using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishedPhase : GamePhase
{

    [SerializeField]
    private LevelFinishedUI ui;

    [SerializeField]
    private CameraFreeMovement CameraMovement;

    public override void EnterPhase(GamePhaseStateMachine statemachine)
    {
        CameraMovement.enabled = false;
        ui.Open();
    }

    public override void LeavePhase()
    {}

    public override void UpdateState(GamePhaseStateMachine statemachine)
    {}
}
