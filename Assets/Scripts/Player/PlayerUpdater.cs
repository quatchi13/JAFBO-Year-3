using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerWrapper
{
    public static class PlayerUpdater
    {
    
        private  static float timePassed, lerpDuration = 1;
        private static Vector3 oldPosition;


        public static void UpdatePlayer(GameObject player, Vector3 newPosition, Vector3 playerRotation)
        {
            oldPosition = player.transform.position;
            player.transform.eulerAngles = playerRotation;
            //player.GetComponent<Animator>().SetBool("Walking", true);
            for(float t = 0; t < lerpDuration; t += Time.deltaTime)
            {

                player.transform.position = Vector3.Lerp(oldPosition, newPosition, t);
            }

        }

        public static void AttackPlayer(GameObject player, GameObject target, Vector3 playerRotation, int damage)
        {
            player.transform.eulerAngles = playerRotation;
            //player.GetComponent<Animator>().SetBool("Attack", true);
            target.GetComponent<HealthSystem>().Damage(damage);
            Debug.Log("slap");
        }
    

   
    }
}

