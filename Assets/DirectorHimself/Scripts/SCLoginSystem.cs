using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.IO;

[ExecuteInEditMode]

public class SCLoginSystem : MonoBehaviour
{
    public enum CurrentWindow { Login, Register }
    public CurrentWindow currentWindow = CurrentWindow.Login;
    public GameObject LoginSystem; 
    public GameObject AllObject;

    public int textSize = 28;
    public Font textFont;
    public Color textColor = Color.white;

    string loginlogin = "";
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
    string username = "";
    string userlogin = "";
    string user_money = "";
    string namegroup = "";
    string idgroup = "";

    string rootURL = "https://m.mymafia.su/"; //Путь, по которому расположены файлы php

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
        GUI.Label(new Rect(5, 35, 250, 325), " Ваш Статус: " + (isLoggedIn ? " В игре. \n Данные авторизации: " + userlogin + "\n Nick: " + username + "\n Баланс:" + user_money + "\n Клан:" + namegroup + idgroup: "Не в игре"), style2);
        if (isLoggedIn)
        {
            LoginSystem.SetActive(false);
            AllObject.SetActive(true);
           
            if (GUI.Button(new Rect(5, 5, 250, 25), "Сменить Авторизацию"))
            {
                isLoggedIn = false;
                username = "";
                userlogin = "";
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
        GUILayout.Label("login:");
        loginlogin = GUILayout.TextField(loginlogin);
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
        var addPlayerLogin = new AddPlayerLogin(1, "Test");
        addPlayerLogin.Execute();
        //var parts = addPlayerLogin.ParsingTableResult(0, 0);

        isWorking = true;
        registrationCompleted = false;
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("login", loginlogin);
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
                    username = dataChunks[1];
                    userlogin = dataChunks[2];
                    user_money = dataChunks[3];
                    namegroup = dataChunks[4];
                    idgroup = dataChunks[5];
                    
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
        //string key = "Nick";
        //PlayerPrefs.SetString(key, username);
        PlayerPrefs.SetString("UserName", username);
        string moneykey = "Money";
        PlayerPrefs.SetString(moneykey, user_money);
        string groupkey = "Group";
        PlayerPrefs.SetString(groupkey, namegroup);
       
        PlayerPrefs.Save();
        Debug.Log("Username: " + username + "Баланс" + user_money + "Клан:" + namegroup );
    }

    void ResetValues()
    {
        errorMessage = "";
        loginlogin = "";
        loginPassword = "";
        registerEmail = "";
        registerPassword1 = "";
        registerPassword2 = "";
        registerUsername = "";
        
    }
}