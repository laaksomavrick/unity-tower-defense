using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform startPoint;
    public Transform[] path;
    
    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<LevelManager>();
            }
            return _instance;
        }
    }
}