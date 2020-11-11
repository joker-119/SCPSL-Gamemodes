using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace Outbreak.Components
{
    public class Infection : MonoBehaviour
    {
        public Player Attacker;
        private Player _player;
        private CoroutineHandle _infectionHandle;
        
        private void Awake()
        {
            _player = Player.Get(gameObject);
            if (_player == null)
            {
                Log.Warn($"Player for new infection component is null?");
                Destroy(this);
            }
        }

        private void Start()
        {
            _infectionHandle = Timing.RunCoroutine(DoInfectionDamage());
        }

        private void OnDestroy()
        {
            if (_infectionHandle.IsValid)
                Timing.KillCoroutines(_infectionHandle);
        }

        IEnumerator<float> DoInfectionDamage()
        {
            for (;;)
            {
                if (_player.Role != RoleType.ClassD)
                    yield break;

                _player.Hurt(3.5f, Attacker ?? _player, DamageTypes.Poison);

                yield return Timing.WaitForSeconds(2.5f);
            }
        }
    }
}