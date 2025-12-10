using UnityEngine;
using UnityEngine.Events;

public class ObjetivoGolpeable : MonoBehaviour
{
    public UnityEvent<float> alGolpear;
    public UnityEvent alTerminoGolpear;

    public void TestGolpe()
    {
        Debug.Log("ME GOLPEARON! 🤕");
    }
}
