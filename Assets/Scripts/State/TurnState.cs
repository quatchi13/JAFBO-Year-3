using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StatePattern
{
    public interface TurnState {
        void OnTileClick();
    }

    public class GameAction : TurnState { 
        public TurnState tState { get; set; }

        public GameAction()
        {
            tState = null;
        }

        public void OnTileClick()
        {
            tState.OnTileClick();

        }


    }

    public class MoveState : TurnState {
        public void OnTileClick()
        {
            GameObject playerRef = ObjectReferencer.instance.GetPlayer();
            switch (playerRef.GetComponent<PlayerInput>().lookDir)
            {
                case 0:
                    playerRef.transform.position = playerRef.transform.position + new Vector3(1, 0, 0);
                    break;
                case 1:
                    playerRef.transform.position = playerRef.transform.position + new Vector3(0, 0, -1);
                    break;
                case 2:
                    playerRef.transform.position = playerRef.transform.position + new Vector3(-1, 0, 0);
                    break;
                case 3:
                    playerRef.transform.position = playerRef.transform.position + new Vector3(0, 0, 1);
                    break;
            }

            ActiveSelections.instance.ClearSelection();
        }
    }

    public class AttackState : TurnState { 
        public void OnTileClick()
        {
            for (int i = 0; i < ActiveSelections.instance.GetSelection().Count; i++)
            {
                ActiveSelections.instance.DestroySelections();
                ActiveSelections.instance.ClearSelection();
            }
        }
    }



}
