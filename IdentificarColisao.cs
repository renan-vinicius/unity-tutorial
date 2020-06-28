using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class IdentificarColisao : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D outraPeca) {
        print(outraPeca.GetComponent<Propriedades>().tipo);
        if (outraPeca.GetComponent<Propriedades>().tipo=="correto")
        {            
            Debug.Log("Acertou!");
            Destroy(this.gameObject);
        }
        else {
            Destroy(outraPeca.gameObject);
        }
    }
}
