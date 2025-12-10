using UnityEngine;

public class Enemigo_s : MonoBehaviour
{
    public Transform player;                // Referencia al jugador
    public float distanciaDeteccion = 10f;  // Distancia para empezar a seguir al jugador
    public float distanciaAtaque = 2f;      // Distancia para atacar
    public int daño = 10;                   // Daño por ataque
    public float tiempoEntreAtaques = 1.5f; // Cooldown entre ataques

    UnityEngine.AI.NavMeshAgent agente;
    Animator anim;
    float ultimoAtaque;

    void Start()
    {
        agente = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector3.Distance(transform.position, player.position);

        // —— Movimiento usando NavMeshAgent ——
        if (distancia <= distanciaDeteccion && PuedeVerAlJugador())
        {
            agente.SetDestination(player.position);
        }
        else
        {
            agente.ResetPath();
        }

        // —— Actualizar animación de velocidad ——
        anim.SetFloat("Speed", agente.velocity.magnitude);

        // —— Ataque al jugador ——
        if (distancia <= distanciaAtaque && Time.time - ultimoAtaque > tiempoEntreAtaques)
        {
            Atacar();
        }
        else
        {
            anim.SetBool("IsAttacking", false);
        }
    }

    void Atacar()
    {
        ultimoAtaque = Time.time;
        anim.SetBool("IsAttacking", true);

        // Obtener la vida del jugador
        VidaJugador vida = player.GetComponent<VidaJugador>();

        if (vida != null)
        {
            vida.RecibirDaño(daño);
        }
    }

    // —— Raycast para detección realista ——
    bool PuedeVerAlJugador()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        Ray ray = new Ray(transform.position + Vector3.up, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaDeteccion))
        {
            return hit.transform == player;
        }

        return false;
    }
}
