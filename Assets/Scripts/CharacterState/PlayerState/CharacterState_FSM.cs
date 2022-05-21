using Character.Player.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterState_FSM : MonoBehaviour
{
    private CharacterBaseState currentState;

    public NavMeshAgent agent;
    public PlayerAnimationHandler anim;
    public bool isClicked = false;

    public readonly CharacterIdleState idleState = new CharacterIdleState();
    public void TransitionToState(CharacterBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
   
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<PlayerAnimationHandler>();
        agent = GetComponent<NavMeshAgent>();
        TransitionToState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.ExitState(this);
    }

    public void IsClicked(InputAction.CallbackContext context)
    {
        isClicked = context.ReadValueAsButton();
    }
}
