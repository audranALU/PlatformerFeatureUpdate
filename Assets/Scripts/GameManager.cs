using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform player;
    public Text lifeText;
    public int lives = 3;

    private bool gameOver;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        UpdateLifeText();
    }

    public void TakeDamage()
    {
        if (gameOver) return;

        lives--;
        UpdateLifeText();

       if (lives <= 0){
        gameOver = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("EndScene");
    }
    }

    public void RespawnFromWater(Transform respawnPoint)
    {
        if (gameOver) return;

        TakeDamage();

        if (gameOver) return;

        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        body.linearVelocity = Vector2.zero;
        body.angularVelocity = 0f;
        player.position = respawnPoint.position;
    }

    void UpdateLifeText()
    {
        lifeText.text = "x" + lives;
    }
}