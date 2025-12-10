using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 100;
    int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(int daño)
    {
        vidaActual -= daño;
        Debug.Log("Vida del jugador: " + vidaActual);

        if (vidaActual <= 0)
        {
            Debug.Log("Jugador muerto");
            // Aquí puedes agregar animación de muerte o reiniciar escena
        }
    }
}
