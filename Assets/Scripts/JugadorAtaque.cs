using UnityEngine;

public class JugadorAtaque : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            animator.SetTrigger("Atacar");
        }
    }
}

