using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    [SerializeField] private Global _global;

    void Start()
    {
        Instantiate(_global.BackgroundScrollerPrefab);
        Instantiate(_global.MiddleScrollerPrefab);
        Instantiate(_global.FrontScrollerPrefab);

        Instantiate(_global.MainCharacterPrefab);

        Instantiate(_global.CameraPrefab);
    }
}
