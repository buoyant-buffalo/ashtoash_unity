using UnityEngine;
using System.Collections;

public class Burn : MonoBehaviour {

    public float burnDuration = 5f;
    public float destroyDelay = 2f;
    public GameObject firePrefab;

    private Material burnMaterial;
    private Color burnColor;

    void Start()
    {
        firePrefab.SetActive(true);
        burnMaterial = GetComponent<Renderer>().material;
        StartCoroutine(burnTexture());
    }

    void Update()
    {
        burnMaterial.SetColor("_Color", burnColor);
    }

    public IEnumerator burnTexture()
    {
        float increment = 0;
        float t = 0;

        while(t < 1) //While t is less than 1 (meaning the affected is not toast yet), lerp the color towards burned toast!
        {
            burnColor = Color.Lerp(Color.white, Color.black, t);
            increment = Time.deltaTime / burnDuration;
            t += Time.deltaTime / burnDuration;
            yield return new WaitForSeconds(increment);
        }

        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
