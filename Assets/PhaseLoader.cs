using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseLoader : MonoBehaviour
{
    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
            LoadNextPhase();
    }

    public void LoadNextPhase() {
        StartCoroutine(LoadPhase(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadPhase(int phaseIndex) {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(phaseIndex);
    }
}
