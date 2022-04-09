using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour{

    #region singleton
    public static GameData instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance found!");
            return;
        }

        instance = this;

        DontDestroyOnLoad(this);
    }
    #endregion

    public Vector3 lastPosition;
}
