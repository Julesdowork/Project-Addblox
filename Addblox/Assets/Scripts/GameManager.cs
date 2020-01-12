using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int highScore;
    public int highLevel;

    private GameData gameData;
    private string dataPath = "/GameData.dat";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);

        InitializeGameData();
    }

    private void InitializeGameData()
    {
        LoadGameData();

        if (gameData == null)
        {
            // We're running our game for the first time
            // Setup initial values
            highScore = 0;
            highLevel = 1;

            gameData = new GameData();
            gameData.HighLevel = highLevel;
            gameData.HighScore = highScore;

            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + dataPath);
            
            if (gameData != null)
            {
                gameData.HighLevel = highLevel;
                gameData.HighScore = highScore;

                bf.Serialize(file, gameData);
            }
        }
        catch (Exception e) { }
        finally
        {
            if (file != null)
                file.Close();
        }
    }

    private void LoadGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + dataPath, FileMode.Open);
            gameData = (GameData)bf.Deserialize(file);

            if (gameData != null)
            {
                highLevel = gameData.HighLevel;
                highScore = gameData.HighScore;
            }
        }
        catch (Exception e) { }
        finally
        {
            if (file != null)
                file.Close();
        }
    }
}
