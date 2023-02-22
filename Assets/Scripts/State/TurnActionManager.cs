using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatePattern;

public class TurnActionManager : MonoBehaviour
{
    private GameAction turnActionContext;
    [SerializeField]
    private PlayerInput player;

    // Start is called before the first frame update
    void Start()
    {
        turnActionContext = new GameAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetToMoveState()
    {
        if(turnActionContext.tState is not MoveState)
        {
            //player.ActivateMoveMode();
            turnActionContext.tState = new MoveState();
        }
    }

    public void SetToAttackState()
    {
        if (turnActionContext.tState is not AttackState)
        {
            //player.ActivateAttackMode();
            turnActionContext.tState = new AttackState();
        }
    }

    public void PerformTurnAction()
    {
        turnActionContext.OnTileClick();
    }
}
