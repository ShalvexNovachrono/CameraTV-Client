using UnityEngine;

public class Base64Decoder : MonoBehaviour
{
    public Texture2D DecodeBase64ToTexture(string base64String)
    {
        byte[] imageBytes = System.Convert.FromBase64String(base64String);
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(imageBytes);
        return texture;
    }
}
