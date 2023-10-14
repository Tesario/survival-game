using UnityEngine;

public class ItemInHandAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private Animator animator;

    const string IS_WALKING = "Is Walking";
    const string IS_RUNNING = "Is Running";
    const string PICK_UP = "Pick Up";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, playerMovement.IsWalking());
        animator.SetBool(IS_RUNNING, playerMovement.IsRunning());
    }

    public void AnimatePickUpItem()
    {
        animator.ResetTrigger(PICK_UP);
        animator.SetTrigger(PICK_UP);
    }
}
