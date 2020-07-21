using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Monobehaviour com a logica para trocar e cenas
/// </summary>
public class ChanceScene : MonoBehaviour
{
    public void ChangeScene(int SceneBuildIndex)
    {
        SceneManager.LoadScene(SceneBuildIndex);
    }

    public void ChangeScene(string SceneBuildName)
    {
        SceneManager.LoadScene(SceneBuildName);
    }
}
