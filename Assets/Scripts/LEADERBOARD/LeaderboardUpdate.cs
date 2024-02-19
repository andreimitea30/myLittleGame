using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LeaderboardUpdate : MonoBehaviour
{
    public Text displayFirst;
    public Text displaySecond;
    public Text displayThird;
    public Text displayFirstScore;
    public Text displaySecondScore;
    public Text displayThirdScore;
    private string csvCurentUserFilePath = "Assets/Scripts/currentUser.csv"; // Path to your CSV file
    private string csvLeaderboardFilePath = "Assets/Scripts/leaderboardCurrent.csv"; // Path to your CSV file
    // Start is called before the first frame update
    void Start()
    {
        // Read all lines from the CSV file
        string line = File.ReadAllText(csvCurentUserFilePath);
        string[] values = line.Split(',');
        string usernameFromFile = values[0];
        string pointsFromFile = values[1];
        string[] lines = File.ReadAllLines(csvLeaderboardFilePath);
        bool found = false;
        for (int k = 0; k < lines.Length; k++) {
            string[] data = lines[k].Split(',');
            int.TryParse(data[1], out int existingPoints);
            int.TryParse(pointsFromFile, out int newPoints);
            Debug.Log(data[0] + " " + data[1] + " " + pointsFromFile + " " + newPoints);
            if (data.Length >= 2 && data[0] == usernameFromFile && newPoints > existingPoints)
            {
                FileUpdater fileUpdater = new FileUpdater();
                fileUpdater.ReplaceLine(csvLeaderboardFilePath, k, usernameFromFile + "," + pointsFromFile);
                found = true;
                break;
            }
        }
        if (!found) {
            List<string> smth = new List<string>(File.ReadAllLines(csvLeaderboardFilePath));
            string newLine = usernameFromFile + "," + pointsFromFile;
            smth.Add(newLine);
            File.WriteAllLines(csvLeaderboardFilePath, smth.ToArray());
        }

        string first = "";
        string second = "";
        string third = "";
        string[] linesLeaderboard = File.ReadAllLines(csvLeaderboardFilePath);
        int i = 0;
        List<string> winnersList = new List<string>(linesLeaderboard.Length);
        foreach (string lineLeaderboard in linesLeaderboard) {
            winnersList.Add(lineLeaderboard);
            i++;
            // string[] data = lineLeaderboard.Split(',');
            // if (data.Length >= 2) {
            //     if (i == 0) {
            //         first = data[0];
            //         displayFirst.text = first;
            //         displayFirstScore.text = data[1];
            //     } else if (i == 1) {
            //         second = data[0];
            //         displaySecond.text = second;
            //         displaySecondScore.text = data[1];
            //     } else if (i == 2) {
            //         third = data[0];
            //         displayThird.text = third;
            //         displayThirdScore.text = data[1];
            //     }
            }
            Debug.Log(winnersList.Count);
        for (int j = 0; j < winnersList.Count; j++) {
            string[] data = winnersList[j].Split(',');
            for (int k = j + 1; k < winnersList.Count; k++) {
                string[] data2 = winnersList[k].Split(',');
                if (data[0].Equals(data2[0])) {
                    int.TryParse(data[1], out int points1);
                    int.TryParse(data2[1], out int points2);
                    if (points1 > points2) {

                    for (int l = k; l < winnersList.Count - 1; l++) {
                        winnersList[l] = winnersList[l + 1];
                    }
                        winnersList.RemoveAt(winnersList.Count - 1);                    
                    } else {
                        string temp = winnersList[j];
                        winnersList[j] = winnersList[k];
                        winnersList[k] = temp;
                        for (int l = k; l < winnersList.Count - 1; l++) {
                            winnersList[l] = winnersList[l + 1];
                        }
                        winnersList.RemoveAt(winnersList.Count - 1);
                    }
                    k--;
                }
            }
        }
        string[] winners = new string[winnersList.Count];
        winners = winnersList.ToArray();
        for (int j = 0; j < winners.Length - 1; j++) {
            string[] data = winners[j].Split(',');
            int.TryParse(data[1], out int points1);
            for (int k = j + 1; k < winners.Length; k++) {
                string[] data2 = winners[k].Split(',');
                int.TryParse(data2[1], out int points2);
                if (points1 < points2) {
                    string temp = winners[j];
                    winners[j] = winners[k];
                    winners[k] = temp;
                    Debug.Log(winners[j] + " " + winners[k]);
                }
            }
        }

        Debug.Log(winnersList.Count + "dupa eliminare");
        AppendToFile writeToFile = new AppendToFile();
        writeToFile.ClearFile(csvLeaderboardFilePath);

        for (int j = 0; j < winnersList.Count; j++) {
            writeToFile.WriteText(csvLeaderboardFilePath, winners[j] + "\n");
        }

        
        for (int j = 0; j < winners.Length; j++) {
            string[] data = winners[j].Split(',');
            if (j == 0) {
                first = data[0];
                displayFirst.text = first;
                displayFirstScore.text = data[1];
            } else if (j == 1) {
                second = data[0];
                displaySecond.text = second;
                displaySecondScore.text = data[1];
            } else if (j == 2) {
                third = data[0];
                displayThird.text = third;
                displayThirdScore.text = data[1];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class FileUpdater
{
    public void ReplaceLine(string filePath, int lineNumber, string newLineContent)
    {
        string[] lines = File.ReadAllLines(filePath);

        if (lineNumber >= 0 && lineNumber < lines.Length)
        {
            lines[lineNumber] = newLineContent;
            File.WriteAllLines(filePath, lines);
        }
    }
}

class AppendToFile
{
    public void WriteText(string filePath, string text)
    {
        File.AppendAllText(filePath, text);
    }

    public void ClearFile (string filePath)
    {
        File.WriteAllText(filePath, "");
    }
}