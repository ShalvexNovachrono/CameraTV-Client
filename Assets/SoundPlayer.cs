using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SoundPlayer : MonoBehaviour
{
    public string audioURL;
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component to the GameObject if it doesn't exist
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Call the method to play the sound from URL
        PlaySoundFromURL(audioURL);
    }

    void PlaySoundFromURL(string url)
    {
        // Start a coroutine to download the audio file from the URL
        StartCoroutine(DownloadAudioClip(url));
    }

    IEnumerator DownloadAudioClip(string url)
    {
        using (var www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error downloading audio: " + www.error);
            }
            else
            {
                // Get the downloaded audio clip
                AudioClip audioClip = DownloadHandlerAudioClip.GetContent(www);

                // Play the audio clip
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }
    }
}
