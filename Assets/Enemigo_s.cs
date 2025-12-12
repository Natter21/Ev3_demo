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

    int indiceCapaAtaque; // AtaqueLayer

    VidaJugador vidaJugador; // VidaJugador
    void Start()
    {
        agente = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();

        // AtaqueLayer
        indiceCapaAtaque = anim.GetLayerIndex("AtaqueLayer");

        if (player != null)
        {
            vidaJugador = player.GetComponent<VidaJugador>();
        }
    }

    void Update()
    {
        if (player == null) return;

        if (vidaJugador != null && vidaJugador.estaMuerto) // para cuandoa el enemigo este muerto
        {
            agente.ResetPath();
            anim.SetFloat("Speed", 0f);
            return;
        }
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
        if (vidaJugador != null && vidaJugador.estaMuerto)
            return;


        ultimoAtaque = Time.time;

        //ANIMACION ATAQUE
        if (indiceCapaAtaque >= 0)
        {

            anim.Play("Zombie Attack", indiceCapaAtaque, 0f);
        }

        // daño jugador
        //VidaJugador vida = player.GetComponent<VidaJugador>();

        if (vidaJugador != null)
        {
            vidaJugador.RecibirDaño(daño);
        }
    }

    // Raycast
    bool PuedeVerAlJugador()
    {
        // Dirección 
        Vector3 origen = transform.position + Vector3.up * 1.5f;
        Vector3 direccion = (player.position - transform.position).normalized;

        Ray ray = new Ray(origen, direccion);
        RaycastHit hit;

        // rayo en la escena
        Debug.DrawRay(origen, direccion * distanciaDeteccion, Color.red);

        if (Physics.Raycast(ray, out hit, distanciaDeteccion))
        {
            // ve al player
            if (hit.transform == player)
            {
                return true;
            }
        }

        return false;
    }
}
