using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Networking;

public class SignInHandler : MonoBehaviourSingleton<SignInHandler>
{
	[ReadOnly] public string loggedUserMail;
	[ReadOnly] public bool isLoginSuccessful;
	[SerializeField] private TMP_InputField emailTextBox;
	[SerializeField] private TMP_InputField passwordTextBox;
	[SerializeField] private Button signinButton;
	[SerializeField] private TMP_Text emailErrorText;
	[SerializeField] private TMP_Text passwordErrorText;

	private string loggedMail, loggedPassword;
	private EventHandler _eventHandler;

	public string GetMailAddress()
	{
		if (!string.IsNullOrEmpty(loggedUserMail))
		{
			return loggedUserMail;
		}
		else
		{
			var mail = PlayerPrefs.GetString("UserMail");
			return mail;
		}
	}
	
	void Start()
	{
		signinButton.onClick.AddListener(TryToSignIn);
		if (PlayerPrefs.HasKey("UserMail"))
		{
			loggedMail = PlayerPrefs.GetString("UserMail");
			loggedPassword = PlayerPrefs.GetString("UserPassword");
		}
        //Run.After(0.5f, ()=> SignInCaller("https://fugogames.com/api/UserLogin/login?email=" + loggedMail + "&password=" + loggedPassword + "&rememberMe=true",""));
    }

    void TryToSignIn()
	{
		var mail = emailTextBox.text;
		var password = passwordTextBox.text;
		loggedUserMail = mail;
		SignInCaller("https://fugogames.com/api/UserLogin/login?email="+mail + "&password="+ password +"&rememberMe=true","");
		loggedUserMail = mail;
	}


	void DisableUI()
	{
		emailTextBox.DeactivateInputField();
		passwordTextBox.DeactivateInputField();
		signinButton.interactable = false;
		emailErrorText.enabled = false;
		passwordErrorText.enabled = false;
	}

	void EnableUI()
	{
		emailTextBox.ActivateInputField();
		passwordTextBox.ActivateInputField();
		signinButton.interactable = true;
	}

	public void SignInCaller(string url, string bodyJsonString)
	{
		StartCoroutine(SignIn(url, bodyJsonString));
	}
    
	IEnumerator SignIn(string url, string bodyJsonString)
	{
		var request = new UnityWebRequest(url, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
		request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
 
		yield return request.SendWebRequest();
 
		Debug.Log("Status Code: " + request.responseCode);
		if (request.responseCode == 401)
		{
			passwordErrorText.text = "Yanlış E-Mail veya Şifre Girdiniz!";
		}
		else if (request.isHttpError)
		{
			Debug.Log(request.error);
			Debug.Log(request.downloadHandler.text);
			passwordErrorText.text = "Bir hata oluştu!";
		}
		else if (request.isNetworkError)
		{
			Debug.Log(request.error);
			Debug.Log(request.downloadHandler.text);
			passwordErrorText.text = "İnternet Bağlantısı Yok!";
		}
		else
		{
			//Debug.Log("Form upload complete!");
			if (request.responseCode == 200)
			{
                string response = request.downloadHandler.text;
				FugoGamesAPI.jwtKey = response;
                //Debug.Log("API Response: " + ElitraWEBAPI.jwtKey);
				loggedMail = emailTextBox.text;
				loggedPassword = passwordTextBox.text;
				if (loggedMail.Length > 5)
				{
					PlayerPrefs.SetString("UserMail", loggedMail);
					PlayerPrefs.SetString("UserPassword", loggedPassword);
				}
				Debug.Log("Login Successfull");
			}
		}
	}


}
