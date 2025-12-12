using UnityEngine;
using System.Collections;
public class Enemigo_s : MonoBehaviour
{
    public Transform player;
    public float distanciaDeteccion = 10f;
    public float distanciaAtaque = 2f;
    public int daño = 10;
    public float tiempoEntreAtaques = 1.5f;

    UnityEngine.AI.NavMeshAgent agente;
    Animator anim;
    float ultimoAtaque;

    int indiceCapaAtaque; // índice de la capa "AtaqueLayer"

    void Start()
    {
        agente = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Busca el índice de la capa por su nombre
        indiceCapaAtaque = anim.GetLayerIndex("AtaqueLayer");
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector3.Distance(transform.position, player.position);

        // Movimiento  NavMeshAgent 
        if (distancia <= distanciaDeteccion && PuedeVerAlJugador())
        {
            agente.SetDestination(player.position);
        }
        else
        {
            agente.ResetPath();
        }

        // caminar
        anim.SetFloat("Speed", agente.velocity.magnitude);

        // Ataque 
        if (distancia <= distanciaAtaque && Time.time - ultimoAtaque > tiempoEntreAtaques)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        ultimoAtaque = Time.time;

        // Reproducir animación "Attack" en la capa de ataque
        if (indiceCapaAtaque >= 0)
        {
            // El nombre del estado debe ser EXACTO al que aparece en la capa (por ejemplo "Attack")
            anim.Play("Zombie Attack", indiceCapaAtaque, 0f);
        }

        // Hacer daño al jugador
        VidaJugador vida = player.GetComponent<VidaJugador>();
        if (vida != null)
        {
            vida.RecibirDaño(daño);
        }
    }

    // --- Detección del jugador con Raycast ---
    bool PuedeVerAlJugador()
    {
        // Dirección desde el enemigo hacia el jugador
        Vector3 origen = transform.position + Vector3.up * 1.5f; // un poco arriba del suelo
        Vector3 direccion = (player.position - transform.position).normalized;

        Ray ray = new Ray(origen, direccion);
        RaycastHit hit;

        // Solo para debug: dibuja el rayo en la escena
        Debug.DrawRay(origen, direccion * distanciaDeteccion, Color.red);

        if (Physics.Raycast(ray, out hit, distanciaDeteccion))
        {
            // Si lo primero que golpea el rayo es el player → lo ve
            if (hit.transform == player)
            {
                return true;
            }
        }

        return false;
    }
}
