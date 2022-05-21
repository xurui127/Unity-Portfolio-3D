
using UnityEngine;

public class CharacterIdleState :CharacterBaseState
{
    public override void EnterState(CharacterState_FSM characterState_FSM)
    {
        characterState_FSM.anim.Speed = 0;
    }

    public override void ExitState(CharacterState_FSM characterState_FSM)
    {
        if (characterState_FSM.isClicked == true)
        {

        }
    }

    public override void Update()
    {
       
    }


}
