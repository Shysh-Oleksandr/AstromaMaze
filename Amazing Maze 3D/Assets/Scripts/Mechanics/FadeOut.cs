using System.Collections;
using UnityEngine;

public class FadeOut : MonoBehaviour, IPooledObject
{
    [SerializeField] float startDelay = 10f, fadeRate = 0.05f, timeForOneFade = 0.5f;
    private float startAlpha;
    public Material material;
    public SprayItem sprayItem;

    private Material mat;
    private Renderer rend;
    public void OnObjectSpawn()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(startDelay);

        Color c = mat.GetColor("_BaseColor");

        startAlpha = c.a;

        for (float f = startAlpha; f >= 0f; f -= fadeRate)
        {
            c.a = f;
            mat.SetColor("_BaseColor", c);
            yield return new WaitForSeconds(timeForOneFade);
        }

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        mat = Instantiate(material);
        if (gameObject.CompareTag("Brush"))
        {
            startDelay = sprayItem.baseLifetime;
            fadeRate = sprayItem.fadeRate;
            timeForOneFade = sprayItem.fadeDelay;
            mat.SetColor("_BaseColor", sprayItem.paintColor);
        }
        rend = GetComponent<Renderer>();
        rend.material = mat;
    }
}
