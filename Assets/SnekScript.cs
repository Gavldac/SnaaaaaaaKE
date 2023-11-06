using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing a Snake with a body that follows the head
/// 
/// author Edwin Casady
/// </summary>
public class SnekScript : MonoBehaviour
{
    [SerializeField] //allows Unity Inspector to show value of private field
    private ScoreManagerScript scoreManager;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private AudioSource beep;
    private bool isGameOver = false; //Using this to know if gameplay has ended
    private Vector2 direction;
    private List<Transform> bodyParts;
    public Transform snakePart;
    public int snakeStartSize = 20;





    public void Start()
    {
        Time.timeScale = speed;
        scoreManager = FindObjectOfType<ScoreManagerScript>();
        StartCoroutine(UpdateScoreInterval(1.0f));

        //Create a new list of bodyParts. 
        //New list is created everygame.
        bodyParts = new List<Transform>();

        //Add the head of the snake to the list
        bodyParts.Add(this.transform);

        //Set the starting position of the snake's head
        this.transform.position = Vector3.zero;

        //Dynamicly add the rest of the snake
        SnakeMaker(snakeStartSize);
        //Start the snake moving in a direction
        direction = Vector2.right;

    }


    //Update is automaticly run by Unity
    // if-else statments check for key presses
    // Snake is not allowed to backwards into itself
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (direction != Vector2.down)
                direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (direction != Vector2.up)
                direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (direction != Vector2.right)
                direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) ||
                    Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (direction != Vector2.left)
                direction = Vector2.right;
        }

        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
            StartNewGame();

    }
    //Builtin function that is called at a set interval by Unity
    //Function controls the speed of the snake/game.
    private void FixedUpdate()
    {
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].position = bodyParts[i - 1].position;
        }

        //Vector3 is the recommended way to to place new game objects
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f);
    }

    private void EndGame()
    {
        beep.Play();
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverScreen.SetActive(isGameOver);
    }

    public void StartNewGame()
    {
        isGameOver = false;
        gameOverScreen.SetActive(isGameOver);
        Time.timeScale = speed;
        UnityEngine.SceneManagement.SceneManager.LoadScene("SnekScene");
                
    }

    /// <summary>
    /// SnakeMaker adds to the List of bodyParts to create a 
    /// snake of the desired length.
    /// Snake always has a head; this adds to the head for a total size
    /// 
    /// author Edwin Casady
    /// </summary>
    private void SnakeMaker(int size)
    {
        for (int i = 1; i < size; i++)
        {
            Transform part = Instantiate(this.snakePart);

            //Create a body segment one position to the left of the last segment
            part.position = new Vector3(
                bodyParts[bodyParts.Count - 1].position.x - 1,
                bodyParts[bodyParts.Count - 1].position.y,
                bodyParts[bodyParts.Count - 1].position.z
            );
            bodyParts.Add(part);
        }
    }

    //Collision trigger. Tags are set in the Unity editor and 
    // compared to determine result of collision.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collidable")
        {
            EndGame();
        }
    }

    private IEnumerator UpdateScoreInterval(float f)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            scoreManager.UpdateScore();
        }
    }

}



