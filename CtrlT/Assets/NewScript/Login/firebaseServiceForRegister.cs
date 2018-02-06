using SimpleFirebaseUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class firebaseServiceForRegister : MonoBehaviour {

    Firebase firebase;
    public InputField username;
    public InputField password;
    public InputField confirmPassword;
    public Text _notification;

    //encrypt password md5
    static string PasswordHash = "P@@Sw0rd";
    static string SaltKey = "S@LT&KEY";
    static string VIKey = "@1B2c3D4e5F6g7H8";
    // Use this for initialization
    void Start () {
        firebase = Firebase.CreateNew("ctrlplust-33926.firebaseio.com");
        firebase.OnGetSuccess += GetOkHandler;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GetOkHandler(Firebase firebasse,DataSnapshot snapshot)
    {

    }

    public void OnRegisterSubmit()
    {
        // complete form
        // pass match confirmPass
        if(username.text != "" && password.text !="" && confirmPassword.text!="")
        {
            if(password.text == confirmPassword.text)
            {
                string encryotPassword = Encrypt(password.text);
                string json = "{ \"username\": \"" + username.text + "\", \"password\": \""
                            + encryotPassword 
                            + "\",\"save\":\"1\"}";

                Debug.Log(json);           
                firebase.Child("users").Push(json, true);
                SceneManager.LoadScene("Login");

            }
            else 
            {
                notification(1);
            }
        }
        else if(username.text == "" || password.text == "" && confirmPassword.text != "")
        {
            notification(2);
        }
    }

    public static string Encrypt(string plainText)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
        var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

        byte[] cipherTextBytes;

        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
                cryptoStream.Close();
            }
            memoryStream.Close();
        }
        return Convert.ToBase64String(cipherTextBytes);
    }

    public void notification(int typeNoti)
    {
        if (typeNoti == 1)
        {
            _notification.text = "The password don't match with confirm password";
        }
        else if (typeNoti == 2)
        {
            _notification.text = "Not Complete Form!!";
        }
    }


}
