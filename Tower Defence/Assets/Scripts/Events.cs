using System.Collections;
using System.Collections.Generic;
using System;

public static class Events
{
    public static event Action<TowerData> OnTowerSelected;
    public static void SelectTower(TowerData data) => OnTowerSelected?.Invoke(data);
}
