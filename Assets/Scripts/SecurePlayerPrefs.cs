using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SecurePlayerPrefs : MonoBehaviour
{
    // health = Deathwing
    // stemina = Ysera
    // mapLevel = Alexstrasza
    // maxDepth = Neltharion
    // coin = Nefarian
    // reward = Onyxia
    // dieCount = Malygos

    public static void SetString(string _key, string _value, byte[] _secret)
    {
        // Hide '_key' string.  
        MD5 md5Hash = MD5.Create();
        byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
        string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

        // Encrypt '_value' into a byte array  
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(_value);

        // Eecrypt '_value' with 3DES.  
        TripleDES des = new TripleDESCryptoServiceProvider();
        des.Key = _secret;
        des.Mode = CipherMode.ECB;
        ICryptoTransform xform = des.CreateEncryptor();
        byte[] encrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

        // Convert encrypted array into a readable string.  
        string encryptedString = Convert.ToBase64String(encrypted);

        // Set the ( key, encrypted value ) pair in regular PlayerPrefs.  
        PlayerPrefs.SetString(hashKey, encryptedString);

        Debug.Log("SetString hashKey: " + hashKey + " Encrypted Data: " + encryptedString);
    }

    public static string GetString(string _key, byte[] _secret, int defaultValue)
    {
        string decryptedString;

        try
        {
            // Hide '_key' string.  
            MD5 md5Hash = MD5.Create();
            byte[] hashData = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(_key));
            string hashKey = System.Text.Encoding.UTF8.GetString(hashData);

            // Retrieve encrypted '_value' and Base64 decode it.  
            string _value = PlayerPrefs.GetString(hashKey);
            byte[] bytes = Convert.FromBase64String(_value);

            // Decrypt '_value' with 3DES.  
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = _secret;
            des.Mode = CipherMode.ECB;
            ICryptoTransform xform = des.CreateDecryptor();
            byte[] decrypted = xform.TransformFinalBlock(bytes, 0, bytes.Length);

            // decrypte_value as a proper string.  
            decryptedString = System.Text.Encoding.UTF8.GetString(decrypted);

            Debug.Log("GetString hashKey: " + hashKey + " GetData: " + _value + " Decrypted Data: " + decryptedString);
        }
        catch(Exception e)
        {
            // 값이 비어있거나 오류가 났을 때

            Debug.Log(e);

            return defaultValue.ToString();
        }

        return decryptedString;
    }

    public static byte[] GetData(string code)
    {
        MD5 md5Hash = new MD5CryptoServiceProvider();
        byte[] secret = md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(code));

        return secret;
    }

    //출처: https://seungngil.tistory.com/entry/unity3d안드로이드에서-암호화-팁-PlayerPref-암호화 [To be a programmer...]
}
