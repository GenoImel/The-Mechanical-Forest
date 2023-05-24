using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Classes who has data than needs to be saved between scenes or play session should implement this interface.
 */
public interface IDataPresistence
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
