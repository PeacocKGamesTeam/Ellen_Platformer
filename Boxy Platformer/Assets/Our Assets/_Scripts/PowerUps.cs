using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit2D
{

    public class PowerUps : MonoBehaviour
    {
        public static PowerUps Instance;

        public int PowerUpCoins { get; set; }
        public float PowerUpDuration { get; set; }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                PowerUpCoins = 100;
                PowerUpDuration = 8f;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != null)
            {
                Destroy(this.gameObject);
            }

        }

        public void GainSuperJump()
        {
            StartCoroutine(SuperJumpCoroutine());
        }

        IEnumerator SuperJumpCoroutine()
        {
            PlayerCharacter.PlayerInstance.jumpSpeed = 22f;

            yield return new WaitForSeconds(PowerUpDuration);

            PlayerCharacter.PlayerInstance.jumpSpeed = 16.5f;
        }

        public void GainSuperSpeed()
        {
            StartCoroutine(SuperSpeedCoroutine());
        }

        IEnumerator SuperSpeedCoroutine()
        {
            PlayerCharacter.PlayerInstance.maxSpeed = 10f;

            yield return new WaitForSeconds(PowerUpDuration);

            PlayerCharacter.PlayerInstance.maxSpeed = 7f;
        }

        public void GainInvinsibility()
        {
            StartCoroutine(InvinsibilityCoroutine());
        }

        IEnumerator InvinsibilityCoroutine()
        {
            PlayerCharacter.PlayerInstance.gameObject.GetComponent<Damageable>().EnableInvulnerabilityCustom(PowerUpDuration, false);
            PlayerCharacter.PlayerInstance.StartFlickeringCustom();

            yield return new WaitForSeconds(PowerUpDuration);

            PlayerCharacter.PlayerInstance.gameObject.GetComponent<Damageable>().DisableInvulnerabilityCustom();
            PlayerCharacter.PlayerInstance.StopFlickering();
        }

    }
}
