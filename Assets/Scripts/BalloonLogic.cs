using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class BalloonLogic : MonoBehaviour
{
    [Header("Inputs")]

    public float Speed;

    [SerializeField] TMP_Text ScoreText;

    float duration = 2f;
    int score = 0;

    [SerializeField] GameObject LostUI;

    float LeftRandomX = -8.25f;
    float RightRandomX = 8.25f;

    float YaxisMaxRange = 6.8f;

    int SceneIndex = 1;

    public AudioSource MenuSource;

    float YaxisRange = -6.2f;
    AudioSource audiosource;

    //rivate bool Islost = false;
    private bool UIStatus = true;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
     if(transform.position.y > YaxisMaxRange) //Islost == false)
        {
            //Show Lost UI...
            LostUI.SetActive(UIStatus);  //True.

            //Freeze the time.
            Time.timeScale = 0f;

            //Restart the Level...
            //print("You Lost!");
            //Islost = true;
        }   
    }

    private void Start()
    {
        Time.timeScale = 1f;
        LostUI.SetActive(false);
        ResetPosition();
    }

    private void FixedUpdate()
    {
        transform.Translate(0, Speed, 0);
    }

    private void OnMouseDown()
    {
        //Increment the Score
        score = score + 1;
        ScoreText.text = score.ToString();
        ScoreText.color = Color.white;   //Score Text color changes to white upon increment.

        //Play the AudioSource.
        audiosource.Play();
        ResetPosition();
    }

    public void ResetPosition()
    {
        //Get a Random Range.
        float RandomValue = Random.Range(LeftRandomX, RightRandomX);
        transform.position = new Vector2(RandomValue, YaxisRange);
    }

    public void BeginGame()
    {
        //Start the Coroutine.
        StartCoroutine(VolumeFade(duration, SceneIndex));
    }

    public void RestartLevel()
    {
        //UnFreeze the time.
        Time.timeScale = 1f;

        //Reload the same Level.
        SceneManager.LoadScene("BalloonGame");
    }

    public void ReturnToMenu()
    {
        //Navigate to menu (Load Menu Scene)
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator VolumeFade(float duration, int SceneIndex)
    {
        //Gradually Decreasing the Volume.
        float initialVolume = MenuSource.volume;

        while (MenuSource.volume > 0)
        {
            MenuSource.volume -= initialVolume * Time.deltaTime / duration;
            yield return null;
        }
        MenuSource.Stop();

        //Load the GameScene
        SceneManager.LoadScene(SceneIndex);
        MenuSource.volume = initialVolume;
    }
}
