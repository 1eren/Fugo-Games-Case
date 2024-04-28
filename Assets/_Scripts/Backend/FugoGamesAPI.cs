using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FugoGamesAPI : MonoBehaviourSingleton<FugoGamesAPI> 
{

    public static string jwtKey;

    IEnumerator GetUserInfo()
    {
        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get("https://fugogames.com/api/UserLogin/UserInfo");
            www.SetRequestHeader("Authorization", "Bearer " + jwtKey);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string jsonString = www.downloadHandler.text;
                JObject dataArray = JObject.Parse(jsonString);
                FugoUserModel user = dataArray.ToObject<FugoUserModel>();
               
                break;
            }

            yield return new WaitForSeconds(5);
        }
    }
}
