using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadLevelDelay());

        Destroy(collision.gameObject);
    }

     IEnumerator LoadLevelDelay()
    {       
        if (ScoreScript.scorevalue == 20)
        {
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            ScoreScript.scorevalue += 1;

            audioSource.PlayOneShot(clip);
        }

    }

}
