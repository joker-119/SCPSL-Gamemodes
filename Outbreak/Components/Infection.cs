using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace Outbreak.Components
{
    public class Infection : MonoBehaviour
    {
        public Player attacker;
        private Player player;
        private CoroutineHandle infectionHandle;
        
        private void Awake()
        {
            player = Player.Get(gameObject);
            if (player == null)
            {
                Log.Warn($"Player for new infection component is null?");
                Destroy(this);
            }
        }

        private void Start()
        {
            infectionHandle = Timing.RunCoroutine(DoInfectionDamage());
        }

        private void OnDestroy()
        {
            if (infectionHandle.IsValid)
                Timing.KillCoroutines(infectionHandle);
        }

        IEnumerator<float> DoInfectionDamage()
        {
            for (;;)
            {
                if (player.Role != RoleType.ClassD)
                    yield break;

                player.Hurt(3.5f, attacker ?? player, DamageTypes.Poison);

                yield return Timing.WaitForSeconds(2.5f);
            }
        }
    }
}