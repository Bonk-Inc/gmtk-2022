using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseStateAdder : MonoBehaviour
{


    [SerializeField]
    private GamePhaseStateMachine machine; 


    [SerializeField]
    private GamePhaseType initialState;

    [SerializeField]
    private PhaseStateType[] states;

    private void Start() {
        for (int i = 0; i < states.Length; i++)
        {
            machine.AddState(states[i].type, states[i].phase);
        }
        machine.SetState(initialState);
    }

    [System.Serializable]
    private class PhaseStateType{
        public GamePhaseType type;
        public GamePhase phase;
    }

}
