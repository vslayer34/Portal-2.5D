using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    int speedParameter;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speedParameter = Animator.StringToHash("Speed");
    }

    public void RunForwardAnimations(float speed)
    {
        animator.SetFloat(speedParameter, speed);
    }
}
