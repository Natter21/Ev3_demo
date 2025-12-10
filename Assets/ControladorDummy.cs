using UnityEngine;

public class ControladorDummy : MonoBehaviour
{
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void SetAnimatorBoolGolpe(bool value)
    {
        animator.SetBool("golpe", value);
    }
}
