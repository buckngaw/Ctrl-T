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

public class firebaseServiceForLogin : MonoBehaviour {


    Firebase firebase;
    Firebase users;
    public InputField username;
    public InputField password;
    public Text _notification;

    public static usersData user = null;

    //for Decrypt password
    static string PasswordHash = "P@@Sw0rd";
    static string SaltKey = "S@LT&KEY";
    static string VIKey = "@1B2c3D4e5F6g7H8";

    // Use this for initialization
    void Start()
    {
        firebase = Firebase.CreateNew("ctrlplust-33926.firebaseio.com");
        firebase.OnGetSuccess += GetOkHandler;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetOkHandler(Firebase firebasse, DataSnapshot snapshot)
    {
        Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
        List<string> keys = snapshot.Keys;

        if (keys != null)
            foreach (string key in keys)
            {
                Dictionary<string, object> child = dict[key] as Dictionary<string, object>;
                usersData myuser = new usersData();       
                myuser.username = child["username"] as string;
                myuser.password = child["password"] as string;
                myuser.save = child["save"] as string;
                myuser.key = key as string;

                string decryptPassword = myuser.password;
                decryptPassword = Decrypt(decryptPassword);
                if (username.text == myuser.username && password.text == decryptPassword)
                {
                    globalUser.thisUser = myuser;
                    user = myuser;
                    SceneManager.LoadScene("newSelcetEpisode");
                }else if(username.text == myuser.username && password.text != decryptPassword)
                {
                    notification(3);
                }
                else
                {
                    notification(1);
                }


            }
    }

    public void OnLoginSubmit()
    {
        if (username.text != "" && password.text != "" )
        {
            users = firebase.Child("users", true);
            users.GetValue();
        }
        else
        {
            notification(2);
        }
    }

    public static string Decrypt(string encryptedText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
        byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
        var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

        var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
        var memoryStream = new MemoryStream(cipherTextBytes);
        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
    }

    public void notification(int typeNoti)
    {
        if (typeNoti == 1)
        {
            _notification.text = "Don't have an account? Please Register";
        } else if (typeNoti == 2)
        {
            _notification.text = "Not Complete Form!!";
        } else if(typeNoti == 3)
        {
            _notification.text = "Wrong Password, Please try again!!";
        }
    }
}
