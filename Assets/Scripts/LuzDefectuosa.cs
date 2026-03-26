using UnityEngine;

public class LuzDefectuosa : MonoBehaviour
{
    public Light luz;
    public float tiempoMin = 0.05f;
    public float tiempoMax = 0.3f;

    void Start()
    {
        if (luz == null)
            luz = GetComponent<Light>();

        StartCoroutine(Flicker());
    }

    System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            luz.enabled = !luz.enabled;
            yield return new WaitForSeconds(Random.Range(tiempoMin, tiempoMax));
        }
    }
}