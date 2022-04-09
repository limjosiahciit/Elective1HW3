using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public PlayerSave TestSave;
    public TMP_Text status;

    public void SaveDataOnFile()
    {
        string name = player.GetComponent<Character>().PlayerName;
        float posX = player.transform.position.x;
        float posY = player.transform.position.y;
        float posZ = player.transform.position.z;

        PlayerSave save = new PlayerSave();
        save.name = name;
        save.position = player.transform.position;
        save.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        string json = JsonUtility.ToJson(save);

        string filePath = Application.dataPath + "/" + name + ".json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        StreamWriter writer = new StreamWriter(filePath, true);
        writer.WriteLine(json);
        writer.Close();

        status.text = "Data Saved";
        Debug.Log("Save file as JSON! Check your editor directory");
    }

    public void LoadDataOnFile()
    {
        string name = player.GetComponent<Character>().PlayerName;
        string filePath = Application.dataPath + "/" + name + ".json";
        if (File.Exists(filePath))
        {
            StreamReader reader = new StreamReader(filePath, true);
            string json = reader.ReadToEnd();
            reader.Close();
            Debug.Log("Loader file from JSON!");

            PlayerSave save = JsonUtility.FromJson<PlayerSave>(json);
            Debug.Log("Loaded data from JSON!");
            Debug.Log("Name: "+ save.name + "\nPosition " + save.position + "\nScene Index " + save.sceneIndex);
            TestSave = save;

            SceneManager.LoadScene(save.sceneIndex);
            GameData.instance.lastPosition = save.position;
            status.text = "Data Load";
        }
        else
        {
            Debug.LogError("File does not exist!");
            status.text = "No Load File Found";
        }
    }

}
