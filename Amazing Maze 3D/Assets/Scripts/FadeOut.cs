using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour, IPooledObject
{
    [SerializeField] float startDelay = 10f, fadeRate = 0.05f, timeForOneFade = 0.5f;
    private float startAlpha;
    public Material material;

    private Material mat;
    private Renderer rend;
    public void OnObjectSpawn()
    {
        StartCoroutine("FadeOutCoroutine");
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
        rend = GetComponent<Renderer>();
        mat = Instantiate(material);
        rend.material = mat;
    }
}
