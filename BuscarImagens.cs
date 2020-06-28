using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

public class BuscarImagens : MonoBehaviour
{
    string jsonString;

    public List<Peca> infoPecas;
    public GameObject[] pecas;

    // Start is called before the first frame update
    void Start()
    {
        string url = "http://renan.cc/unity/dados.json";
        pecas = GameObject.FindGameObjectsWithTag("peca");

        StartCoroutine(recuperarDados(url));      

    }

    IEnumerator recuperarDados(string urlDados)
    {
        UnityWebRequest www = UnityWebRequest.Get(urlDados);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            jsonString = www.downloadHandler.text;
            infoPecas = JsonConvert.DeserializeObject<List<Peca>>(jsonString);

            Peca gabarito = infoPecas.Find(x => x.tipo == "gabarito");
            StartCoroutine(carregarImagem(gabarito.url, GameObject.Find("gabarito")));
            infoPecas.RemoveAt(infoPecas.FindIndex(x => x.tipo == "gabarito"));

            int i = 0;
            foreach (var x in infoPecas)
            {
                StartCoroutine(carregarImagem(x.url, pecas[i]));
                pecas[i].GetComponent<Propriedades>().tipo = x.tipo;
                i++;                
            }          
        }
    }

    IEnumerator carregarImagem(string url, GameObject objeto)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(myTexture, new Rect(0, 0, 300, 300),new Vector2(0.5f,0.5f), 100.0f);
            objeto.GetComponent<SpriteRenderer>().sprite = sprite;            
        }
    }
}


public class Peca : MonoBehaviour
{
    public string url;
    public string tipo;
}