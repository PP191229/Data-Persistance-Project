using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class NameTracker : MonoBehaviour
{
    public static NameTracker Instance;
    public string theName, previousRecord;
    public Text previousName;
    public GameObject inputField;
    public int highScore;

    private void Awake()
    {
        LoadScore();


        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }


    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string theName;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.highScore = NameTracker.Instance.highScore;
        data.theName = NameTracker.Instance.theName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            previousRecord = data.theName;

            previousName.text = "Best Score " + previousRecord + " " + highScore;
        }
    }

}
