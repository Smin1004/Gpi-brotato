using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;
    public Stat stat;

    void Awake()
    {
        _instance = this;
    }
}
