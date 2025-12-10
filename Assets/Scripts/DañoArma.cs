using UnityEngine;

public class DañoArma : MonoBehaviour
{
    public int daño = 10;

    private void OnTriggerEnter(Collider other)
    {
        Vida v = other.GetComponent<Vida>();

        if (v != null)
        {
            v.RecibirDaño(daño);
        }
    }
}