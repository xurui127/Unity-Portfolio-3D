using System;
using UnityEngine;

public abstract class CharacterBaseState : MonoBehaviour
{
    public abstract void EnterState(CharacterState_FSM characterState_FSM);
    public abstract void ExitState(CharacterState_FSM characterState_FSM);
    public abstract void Update();


}
