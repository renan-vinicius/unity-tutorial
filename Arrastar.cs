using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Arrastar : MonoBehaviour
{
    private bool arrastando = false;
    private Vector2 posicaoInicial;
    private float posInicialX;
    private float posInicialY;

    void Start()
    {
        posicaoInicial = this.transform.localPosition;
    }

    void Update() {
        if (arrastando==true){
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            this.gameObject.transform.localPosition = new Vector2(mousePos.x - posInicialX, mousePos.y - posInicialY);
        }
    }

    private void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            posInicialX = mousePos.x - this.transform.localPosition.x;
            posInicialY = mousePos.y - this.transform.localPosition.y;
            arrastando = true;
        }
    }

    /* Se soltar o objeto, faz ele voltar para a posição original */
    private void OnMouseUp()
    {
        arrastando = false;
        this.gameObject.transform.localPosition = posicaoInicial;
    }


}
