using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject levelFinishParent;
    private bool levelFinish = false;
    private Target playerHealth;

    public bool GetlevelFinish
    {
        get { return levelFinish; }
    }

    

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        int enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount <= 0 || playerHealth.GetHealth <= 0)
        {
            levelFinishParent.gameObject.SetActive(true);
            levelFinish = true;
        }
        else
        {
            levelFinishParent.gameObject.SetActive(false);
            levelFinish = false;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
