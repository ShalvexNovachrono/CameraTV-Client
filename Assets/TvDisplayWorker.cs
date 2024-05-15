using System;
using System.Linq;
using System.Text;
using UnityEngine;

public class TvDisplayWorker : MonoBehaviour
{

    private Texture2D texture;

    public WebSocketWorker worker;


    void Start()
    {
        worker = FindAnyObjectByType<WebSocketWorker>();
    }

    void Update()
    {
        // Decode the Base64 string
        byte[] imageBytes = Convert.FromBase64String(worker.dataHolder.data);

        // Remove the header (optional, depending on how the Base64 string was generated)
        if (imageBytes.Length > 23 && Encoding.ASCII.GetString(imageBytes, 0, 23) == "data:image/png;base64,")
        {
            imageBytes = imageBytes.Skip(23).ToArray();
        }

        // Load the image data into a Texture2D
        texture = new Texture2D(1, 1);
        texture.LoadImage(imageBytes);

        transform.GetComponent<Renderer>().material.mainTexture = texture;
        
    }

    /*void OnGUI()
    {
        if (texture != null)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width * 0.5f, Screen.height * 0.5f), texture);
        }
    }*/
}