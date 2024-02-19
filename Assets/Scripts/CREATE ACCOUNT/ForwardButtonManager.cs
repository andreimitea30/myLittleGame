// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// public class forward : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     public void forwardButton()
//     {
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//     }
// }

using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CredentialsVerifierAuth : MonoBehaviour
{
    public InputField passwordInput;
    public InputField usernameInput;
    public InputField confirmPasswordInput;

    private string csvFilePath = "Assets/Scripts/credentials.csv"; // Path to your CSV file
    private string csvCurentUserFilePath = "Assets/Scripts/currentUser.csv"; // Path to your CSV file


    public void OnLoginButtonPressedAuth()
    {   
        if (usernameInput == null || passwordInput == null || confirmPasswordInput == null)
        {
            Debug.Log("Username or password is empty");
            return;
        }
        string username = usernameInput.text;
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;
        if (username != null && password != null && confirmPassword != null)
        {
            // Debug.Log("Username: " + username + " Password: " + password);
        } else {
            Debug.Log("Username or password is empty");
        }
        if (password != confirmPassword)
        {
            Debug.Log("Passwords do not match");
            return;
        }
        

        if (!CheckCredentialsAuth(username, password))
        {
            WriteCredentials(username, password);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("Username already exists");
        }
    }

    private bool CheckCredentialsAuth(string inputUsername, string inputPassword)
    {
        bool found = false;

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvFilePath);

        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            if (values.Length >= 2)
            {
                string usernameFromFile = values[0];
                string passwordFromFile = values[1];

                // Check if the input credentials match any entry in the CSV file
                if (inputUsername == usernameFromFile && inputPassword == passwordFromFile)
                {
                    found = true;
                    break;
                }
            }
        }

        return found;
    }
    private void WriteCredentials(string inputUsername, string inputPassword)
{   

    List<string> lines = new List<string>(File.ReadAllLines(csvFilePath));
    string newLine = inputUsername + "," + inputPassword;
    lines.Add(newLine);
    File.WriteAllLines(csvFilePath, lines.ToArray());
    string currentUser = inputUsername + ",0";
    File.WriteAllText(csvCurentUserFilePath, currentUser);
}
}

