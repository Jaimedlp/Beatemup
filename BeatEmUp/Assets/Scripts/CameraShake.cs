using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Transform camTransform;

    public float DuracionShake = 0f;

    public float CantidadShake = 0.7f;
    public float Decremento = 1.0f;

    public GameObject PJ;
    Player scriptPJ;
    Vector3 posicionOriginal;
	// Use this for initialization
	void Start () {
        
        scriptPJ = PJ.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        posicionOriginal = camTransform.localPosition;
            if (/*DuracionShake^ > 0 &&*/ scriptPJ.Attack)
            {
                camTransform.localPosition = posicionOriginal + Random.insideUnitSphere * CantidadShake;
                DuracionShake -= Time.unscaledDeltaTime * Decremento;
            }
            else {
                DuracionShake = 0f;
                camTransform.localPosition = posicionOriginal;
            }
        }
}
