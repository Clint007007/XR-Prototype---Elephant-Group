using System.Collections;
using UnityEngine;

public class ElephantController : MonoBehaviour
{
    public static ElephantController Instance;

    [Header("References")]
    public Animator animator;
    public Transform playerTarget;
    public Transform leavingLocation;

    [Header("Movement")]
    public float walkSpeed    = 5f;
    public float stopDistance = 5f;

    [Header("Ground Snapping")]
    public float groundOffset = 0f;
    public LayerMask groundLayer;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void SnapToGround()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + Vector3.up * 10f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, 100f, groundLayer))
        {
            Vector3 pos = transform.position;
            pos.y = hit.point.y + groundOffset;
            transform.position = pos;
        }
    }

    public void StartWalking() => StartCoroutine(WalkToPlayer());

    IEnumerator WalkToPlayer()
    {
        animator.SetTrigger("Walk");
        AudioManager.Instance.PlayFootsteps();

        while (true)
        {
            float dist = Vector3.Distance(transform.position, playerTarget.position);
            if (dist <= stopDistance) break;

            Vector3 dir = (playerTarget.position - transform.position);
            dir.y = 0f;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                Time.deltaTime * 5f
            );
            transform.position += dir.normalized * walkSpeed * Time.deltaTime;
            SnapToGround();
            yield return null;
        }

        AudioManager.Instance.StopFootsteps();
        animator.SetTrigger("OnReached");
        GameManager.Instance.SetIdle();
    }

    public void PlayAttack()   => StartCoroutine(AttackRoutine());
    public void PlayWalkAway() => StartCoroutine(WalkAwayRoutine());

    IEnumerator AttackRoutine()
    {
        animator.SetTrigger("Gesture1");
        AudioManager.Instance.PlayAttackSounds();

        yield return new WaitForSeconds(1.5f);

        DeathScreen deathScreen = (DeathScreen)FindObjectOfType(typeof(DeathScreen));
        if (deathScreen != null)
            deathScreen.ShowDeathScreen();
    }

    IEnumerator WalkAwayRoutine()
    {
        animator.SetTrigger("Gesture2");
        AudioManager.Instance.PlayWalkAwaySounds();

        while (true)
        {
            float dist = Vector3.Distance(transform.position, leavingLocation.position);
            if (dist <= 5f) break;

            Vector3 dir = (leavingLocation.position - transform.position);
            dir.y = 0f;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                Time.deltaTime * 5f
            );
            transform.position += dir.normalized * walkSpeed * Time.deltaTime;
            SnapToGround();
            yield return null;
        }

        AudioManager.Instance.StopFootsteps();
        animator.SetTrigger("OnReached");

        VictoryScreen victoryScreen = (VictoryScreen)FindObjectOfType(typeof(VictoryScreen));
        if (victoryScreen != null)
            victoryScreen.ShowVictoryScreen();
        else
            GameManager.Instance.ReturnToStart();
    }
}