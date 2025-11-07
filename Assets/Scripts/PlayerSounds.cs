using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;
    float volume = 1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer >= 0f)
        {
            return;
        }

        footstepTimer = footstepTimerMax;

        if (!player.IsWalking())
        {
            return;
        }

        SoundManager.Instance.PlayFootStepsSound(player.transform.position, volume);
    }
}
