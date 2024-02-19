using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class CredentialsVerifier : MonoBehaviour
{
    public InputField passwordInput;
    public InputField usernameInput;

    private string csvFilePath = "Assets/Scripts/credentials.csv"; // Path to your CSV file
    private string csvCurentUserFilePath = "Assets/Scripts/currentUser.csv"; // Path to your CSV file

    public void OnLoginButtonPressed()
    {   
        if (usernameInput == null || passwordInput == null)
        {
            Debug.Log("Username or password is empty");
            return;
        }
        string username = usernameInput.text;
        string password = passwordInput.text;
        if (username != null && password != null)
        {
            Debug.Log("Username: " + username + " Password: " + password);
        } else {
            Debug.Log("Username or password is empty");
        }
        

        if (CheckCredentials(username, password))
        {
            string currentUser = username + ",0";
            File.WriteAllText(csvCurentUserFilePath, currentUser);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    private bool CheckCredentials(string inputUsername, string inputPassword)
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
}
