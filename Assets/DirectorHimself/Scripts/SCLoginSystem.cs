using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.IO;
using System;

[ExecuteInEditMode]

public class SCLoginSystem : MonoBehaviour
{
    public enum CurrentWindow { Login, Register }
    public CurrentWindow currentWindow = CurrentWindow.Login;
    public GameObject LoginSystem; 
    public GameObject AllObject;
    public GameObject ReEntry_Men;
    
    public GameObject ReEntry_Women;

    public int textSize = 28;
    public Font textFont;
    public Color textColor = Color.white;

    string loginEmail = "";
    string loginPassword = "";
    string registerEmail = "";
    string registerPassword1 = "";
    string registerPassword2 = "";
    string registerUsername = "";
    string errorMessage = "";

    bool isWorking = false;
    bool registrationCompleted = false;
    bool isLoggedIn = false;

    //Данные авторизованного пользователя
    string userName = "";
    string userEmail = "";

    string rootURL = "http://m.mymafia.su/"; //Путь, по которому расположены файлы php

    private int NumberCar;

    void Start()// Старт вызывается перед обновлением первого кадра
    {
        //NumberCar = PlayerPrefs.GetInt("Car");
    }

    void OnGUI()
    {
        if (!isLoggedIn)
        {
            if (currentWindow == CurrentWindow.Login)
            {
                GUI.Window(0, new Rect(Screen.width / 2 - 260, Screen.height / 2 - 150, 520, 300), LoginWindow, "ФОРМА ВХОДА");
            }
            if (currentWindow == CurrentWindow.Register)
            {
                GUI.Window(0, new Rect(Screen.width / 2 - 260, Screen.height / 2 - 150, 520, 300), RegisterWindow, "ФОРМА РЕГИСТРАЦИИ");
            }
        }

        GUIStyle style2 = new GUIStyle();
        style2.fontSize = textSize;
        style2.richText = true;
        if (textFont) style2.font = textFont;
        style2.normal.textColor = textColor;
        GUI.Label(new Rect(170, 10, 500, 25), "Статус: " + (isLoggedIn ? "В игре.  Ваш Nick: " + userName + " Ваш Email: " + userEmail : "Не в игре"), style2);
        if (isLoggedIn)
        {
            LoginSystem.SetActive(false);
            NumberCar = PlayerPrefs.GetInt("Car");
            if (NumberCar == 1)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 2)
            {
                ReEntry_Women.SetActive(true);
            }
            else if (NumberCar == 3)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 4)
            {
                ReEntry_Women.SetActive(true);
            }
            else if (NumberCar == 5)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 6)
            {
                ReEntry_Women.SetActive(true);
            }
            else if (NumberCar == 7)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 8)
            {
                ReEntry_Women.SetActive(true);
            }
            else if (NumberCar == 9)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 10)
            {
                ReEntry_Women.SetActive(true);
            }
            else if (NumberCar == 11)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 12)
            {
                ReEntry_Women.SetActive(true);
            }
            else if (NumberCar == 13)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 14)
            {
                ReEntry_Women.SetActive(true);
            }
            else if (NumberCar == 15)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 16)
            {
                ReEntry_Women.SetActive(true);
            }
            else if (NumberCar == 17)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 18)
            {
                ReEntry_Women.SetActive(true);
            }
            else if (NumberCar == 19)
            {
                ReEntry_Men.SetActive(true);
            }
            else if (NumberCar == 20)
            {
                ReEntry_Women.SetActive(true);
            }
            else
            {
                AllObject.SetActive(true);
            }
          
            if (GUI.Button(new Rect(5, 5, 150, 25), "Сменить Авторизацию"))
            {
                isLoggedIn = false;
                userName = "";
                userEmail = "";
                currentWindow = CurrentWindow.Login;
                LoginSystem.SetActive(true);
                AllObject.SetActive(false);
            }
        }
    }

    void LoginWindow(int index)
    {
        if (isWorking)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }
        if (registrationCompleted)
        {
            GUI.color = Color.green;
            GUILayout.Label("Registration Completed!");
        }

        GUI.color = Color.white;
        GUILayout.Label("Email:");
        loginEmail = GUILayout.TextField(loginEmail);
        GUILayout.Label("Password:");
        loginPassword = GUILayout.PasswordField(loginPassword, '*');

        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(85)))
        {   
            StartCoroutine(LoginEnumerator());
        }

        GUILayout.FlexibleSpace();

        GUILayout.Label("Do not have account?");
        if (GUILayout.Button("Register", GUILayout.Width(125)))
        {
            ResetValues();
            currentWindow = CurrentWindow.Register;
        }
    }

    void RegisterWindow(int index)
    {
        if (isWorking)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }

        GUI.color = Color.white;
        GUILayout.Label("Email:");
        registerEmail = GUILayout.TextField(registerEmail, 254);
        GUILayout.Label("Username:");
        registerUsername = GUILayout.TextField(registerUsername, 20);
        GUILayout.Label("Password:");
        registerPassword1 = GUILayout.PasswordField(registerPassword1, '*', 19);
        GUILayout.Label("Password Again:");
        registerPassword2 = GUILayout.PasswordField(registerPassword2, '*', 19);

        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(85)))
        {
            StartCoroutine(RegisterEnumerator());
        }

        GUILayout.FlexibleSpace();

        GUILayout.Label("Already have an account?");
        if (GUILayout.Button("Login", GUILayout.Width(125)))
        {
            ResetValues();
            currentWindow = CurrentWindow.Login;
        }
    }

    IEnumerator RegisterEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("email", registerEmail);
        form.AddField("username", registerUsername);
        form.AddField("password1", registerPassword1);
        form.AddField("password2", registerPassword2);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    ResetValues();
                    registrationCompleted = true;
                    currentWindow = CurrentWindow.Login;
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }

    IEnumerator LoginEnumerator()
    {
        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("email", loginEmail);
        form.AddField("password", loginPassword);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    string[] dataChunks = responseText.Split('|');
                    userName = dataChunks[1];
                    userEmail = dataChunks[2];
                   
                    isLoggedIn = true;
                    Save();
                    ResetValues();
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }
    
    public void Save()
    {
        var player = new GetPlayer(userName, userEmail);
        player.Execute();
        var idPlayer = Convert.ToInt32(player.ParsingTableResult(0, 0));

        PlayerPrefs.SetString("nick", userName);
        PlayerPrefs.SetString("email", userEmail);
        PlayerPrefs.SetInt("IdPlayer", idPlayer);
        PlayerPrefs.Save();

        Debug.Log("Username: " + userName);

        ControllerScenes.LoadScene(1);
    }
    void ResetValues()
    {
        errorMessage = "";
        loginEmail = "";
        loginPassword = "";
        registerEmail = "";
        registerPassword1 = "";
        registerPassword2 = "";
        registerUsername = "";
        
    }
}