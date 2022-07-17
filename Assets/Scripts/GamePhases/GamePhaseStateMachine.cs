using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePhaseStateMachine : MonoBehaviour
{

    private Dictionary<GamePhaseType, GamePhase> states = new Dictionary<GamePhaseType, GamePhase>();
    public GamePhaseType CurrentState {get; private set;} = GamePhaseType.DEFAULT;

    public void AddState(GamePhaseType type, GamePhase phase){
        if (states.ContainsKey(type))
        {
            states[type] = phase;
        }else {
            states.Add(type, phase);
        }
        
    }

    public void SetState(GamePhaseType type){
        if(states.ContainsKey(CurrentState))
            states[CurrentState]?.LeavePhase();
        
        CurrentState = type;
        
        if(states.ContainsKey(CurrentState))
            states[CurrentState]?.EnterPhase(this);
    }

    private void Update() {
        if(states.ContainsKey(CurrentState))
            states[CurrentState]?.UpdateState(this);
    }
    
}
