using UnityEngine;
using System;
using System.Collections;

public class CameraStreamer : MonoBehaviour
{
    public Camera cameraToCapture;

    public WebSocketWorker ws ;


    void Start()
    {
        ws = FindAnyObjectByType<WebSocketWorker>();
        if (cameraToCapture == null)
        {
            Debug.LogError("Camera to capture is not assigned!");
            return;
        }

        // Make sure camera is rendering to a RenderTexture
        RenderTexture renderTexture = cameraToCapture.targetTexture;
        AudioListener audioListener = cameraToCapture.GetComponent<AudioListener>();

        if (renderTexture == null)
        {
            Debug.LogError("Camera is not rendering to a RenderTexture!");
            return;
        }

        // Subscribe to camera's RenderTexture
        cameraToCapture.targetTexture = new RenderTexture(renderTexture.width, renderTexture.height, 0);
        cameraToCapture.targetTexture.name = "CameraOutput";
        cameraToCapture.targetTexture.Create(); 
        StartCoroutine(StreamCameraFootage());
    }


    IEnumerator StreamCameraFootage()
    {

        // Get the audio volume from the audio listener

        RenderTexture.active = cameraToCapture.targetTexture;

        // Read pixels from the camera render texture
        Texture2D tex = new Texture2D(cameraToCapture.targetTexture.width, cameraToCapture.targetTexture.height);
        tex.ReadPixels(new Rect(0, 0, cameraToCapture.targetTexture.width, cameraToCapture.targetTexture.height), 0, 0);
        tex.Apply();

        // Convert texture to PNG bytes
        byte[] pngBytes = tex.EncodeToPNG();

        // Send camera output 
        // Convert PNG bytes to Base64 string
        string base64String = Convert.ToBase64String(pngBytes);

        // Convert audio volume to string

        // Create JSON with camera video, audio, and volume
        string json = "{\"type\": \"image\", \"image\": \"" + base64String + "\" }";

        // Send JSON over WebSocket
        ws.Send(json);
        //Debug.Log(json);
        

        yield return new WaitForSeconds(0.03f);
        StartCoroutine(StreamCameraFootage());
    }
}
