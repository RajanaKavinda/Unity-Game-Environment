using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest : MonoBehaviour
{
    private string givenBackend = "http://20.15.114.131:8080/api";
    private string ourBackend = "http://localhost:8080/energy-quest";
    public string result = null;

    // Method for sending HTTP requests
   public IEnumerator SendHttpRequest(string requestType, string url, string urlExtension, string jwtToken, string bodyJson)
    {
        UnityWebRequest request = null;
        

        // Select URL
        switch (url)
        {
            case "givenBackend":
                url = givenBackend + urlExtension;
                break;
            case "ourBackend":
                url = ourBackend + urlExtension;
                break;
            default:
                url = url + urlExtension;
                break;
        }

        // Create request based on request type
        switch (requestType)
        {
            case "get":
                request = UnityWebRequest.Get(url);
                break;
            case "post":
                request = UnityWebRequest.PostWwwForm(url, bodyJson);
                break;
            case "put":
                request = UnityWebRequest.Put(url, bodyJson);
                break;
            case "delete":
                request = UnityWebRequest.Delete(url);
                break;
            default:
                Debug.LogError("Invalid HTTP request type!");
                yield break;
        }

        // Set headers
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        // Send request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error sending HTTP request: " + request.error);
            result = "Error";
        }
        else
        {
            result = request.downloadHandler.text;
        }

        // Return the result of the request
        yield return result;
    }

}
