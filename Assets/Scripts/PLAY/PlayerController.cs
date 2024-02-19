using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour {

	public float movementSpeed = 7f;
    float score = 0f;
	Rigidbody2D rb;

	float movement = 0f;
    public Text displayScore;
    public float fallBoundary = 10f;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 cameraPosition = Camera.main.transform.position;
        float cameraY = cameraPosition.y;
        if (transform.position.y > score)
        {
            score = transform.position.y;
            // displayScore.text = score.ToString();
        }
        int intScore = (int)score;
        displayScore.text ="SCORE: " + intScore.ToString();
        // Debug.Log(score.ToString() + " " + score);
        if (transform.position.y <= cameraY - fallBoundary && transform.position.y > 0)
        {
            string csvCurentUserFilePath = "Assets/Scripts/currentUser.csv"; // Path to your CSV file
            string line = File.ReadAllText(csvCurentUserFilePath);
            string[] values = line.Split(',');
            string usernameFromFile = values[0];
            string pointsFromFile = values[1];
            string currentUser = usernameFromFile + "," + score;
            File.WriteAllText(csvCurentUserFilePath, currentUser);
            // Debug.Log("Score:" + score);
            EndGame();
        }
        // Debug.Log(transform.position.y + " " + cameraY);
		movement = Input.GetAxis("Horizontal") * movementSpeed;
	}

	void FixedUpdate()
	{
		Vector2 velocity = rb.velocity;
		velocity.x = movement;
		rb.velocity = velocity;
	}
    void EndGame()
    {
        SceneManager.LoadScene("LEADERBOARD");
    }
    // void writeScore(string score) 
    // {
    //     private string csvCurentUserFilePath = "Assets/Scripts/currentUser.csv"; // Path to your CSV file
    //     string line = File.ReadAllText(csvCurentUserFilePath);
    //     string[] values = line.Split(',');
    //     string usernameFromFile = values[0];
    //     string pointsFromFile = values[1];
    //     string currentUser = usernameFromFile + "," + score;
    //     File.WriteAllText(csvCurentUserFilePath, currentUser);
    // }
}
