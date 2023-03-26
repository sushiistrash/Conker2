using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    private int _gemsCollected = 0;
    public int GemsCollected
    {
        get { return _gemsCollected; }
        set
        {
            if (value < 0)
                throw new System.ArgumentOutOfRangeException(value.ToString(), value, "The value must be greater or equal than 0");
            _gemsCollected = value;
        }
    }

    private void Start()
    {
        if (SaveSystem.Instance.HasLoaded)
        {
            _gemsCollected = SaveSystem.Instance.ActiveSave.Gems;
        }
        else
        {
            _gemsCollected = 0;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        SaveSystem.Instance.Load();
        _gemsCollected = SaveSystem.Instance.ActiveSave.Gems;
    }
}
