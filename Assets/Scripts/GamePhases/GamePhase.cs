using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GamePhase : MonoBehaviour
{

    public abstract void EnterPhase(GamePhaseStateMachine statemachine);

    public abstract void UpdateState(GamePhaseStateMachine statemachine);

    public abstract void LeavePhase();
}
