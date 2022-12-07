using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    private static PlayerBase _playerBase = null;

    public static PlayerBase PlayerBase
    {
        get
        {
            if (_playerBase is null)
            {
                _playerBase = GameObject.Find("Player").GetComponent<PlayerBase>();
            }

            return _playerBase;
        }
    }
}
