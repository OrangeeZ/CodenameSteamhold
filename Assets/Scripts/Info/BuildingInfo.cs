﻿using Assets.Scripts.Actors;
using Assets.Scripts.World.EntityFactory;
using csv;
using UnityEngine;

public class BuildingInfo : ScriptableObject, ICsvConfigurable
{
    [RemoteProperty("Name")]
    public string Name;

    [RemoteProperty("ID")]
    public string Id;

    [RemoteProperty("HP")]
    public int Hp;

    [RemoteProperty("InputResource")]
    public string InputResource;

    [RemoteProperty("InputResourceQuantity")]
    public int InputResourceQuantity;

    [RemoteProperty("ProductionDuration")]
    public int ProductionDuration;

    //[RemoteProperty("OutputResource")]
    public ResourceType OutputResource;

    [RemoteProperty("OutputResourceQuantity")]
    public int OutputResourceQuantity;

    public ActorView Prefab;

    public EntityDisplayPanel DisplayPanelPrefab;

    public void Configure(Values values)
    {
        Prefab = values.GetPrefabWithComponent<ActorView>("Prefab", false);
        DisplayPanelPrefab = values.GetPrefabWithComponent<EntityDisplayPanel>("DisplayPanelPrefab", false);
        values.GetEnum("OutputResource",out OutputResource,ResourceType.None);
    }
}
