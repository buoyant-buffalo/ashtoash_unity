using UnityEngine;
using System.Collections;

public class Burn : MonoBehaviour {

    public float minDuration;

    private Material burnMaterial;
    private Color burnColor;

    void Start()
    {
        burnMaterial = GetComponent<Renderer>().material;
        StartCoroutine(burnTexture());
    }

    void Update()
    {
        burnMaterial.SetColor("_Color", burnColor);
    }

    public IEnumerator burnTexture()
    {
        float increment = 0, t = 0;
        while(t < 1) //While t is less than 1 (meaning the affected is not toast yet), lerp the color towards burned toast!
        {
            burnColor = Color.Lerp(Color.white, Color.black, t);
            increment = Time.deltaTime / minDuration;
            t += Time.deltaTime / minDuration;
            yield return new WaitForSeconds(increment);
        }
    }
}
