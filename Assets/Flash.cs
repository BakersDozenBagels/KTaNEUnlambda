using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour {
    public Material flashMat;
    public Material defaultMat;
    public GameObject haloTemplate;
    public Transform Override;
	
    public void FlashForSeconds(float timeIn)
    {
        StartCoroutine(FlashCo(timeIn));
    }

    private IEnumerator FlashCo(float timeIn)
    {
        GameObject temp = Instantiate(haloTemplate);
        if (Override == null) Override = transform;
        temp.transform.parent = Override;
        temp.transform.localPosition = new Vector3(0f, 0f, 0f);
        temp.GetComponentInChildren<Light>().range *= Override.localScale.x;
        gameObject.GetComponent<MeshRenderer>().material = flashMat;
        yield return new WaitForSeconds(timeIn);
        gameObject.GetComponent<MeshRenderer>().material = defaultMat;
        Destroy(temp);
    }
}
