using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 100;
    int vidaActual;

    public bool estaMuerto = false;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(int daño)
    {
        // Si ya está muerto, ignorar más daño
        if (estaMuerto) return;

        vidaActual -= daño;
        Debug.Log("Vida del jugador: " + vidaActual);

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            estaMuerto = true;
            Debug.Log("Jugador muerto");
        }
    }
}
