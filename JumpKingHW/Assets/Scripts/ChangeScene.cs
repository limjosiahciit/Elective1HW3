using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, ISceneable<int>{
    public void LoadScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
}
