using UnityEngine;

public class Aminationn : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;
    //public float rotationSpeed = 10f;
    public GameObject colliderArma; // 
    
    public float mouseSensitivity = 200f; // sensibilidad del mouse

    private CharacterController controller;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        colliderArma.SetActive(false); // apaga el collider al inicio
    }

    void Update()
    {
        //movimiento con el mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(0f, mouseX, 0f);

        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");   

        // Dirección 
        Vector3 moveDir = (transform.right * h + transform.forward * v);

        ///camianndo
        bool isWalking = moveDir.sqrMagnitude > 0.01f;
        animator.SetBool("isWalking", isWalking);


        // ataque 
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            colliderArma.SetActive(true);   //  daño
            //Invoke("DesactivarColliderArma", 0.4f); // ← se desactiva  0.4s 
        }

        if (isWalking)
        {
            //moveDir = moveDir.normalized;

            // Mover 
            controller.SimpleMove(moveDir * speed);
        }
    }
    void DesactivarColliderArma()
    {
        colliderArma.SetActive(false);
    }
}
