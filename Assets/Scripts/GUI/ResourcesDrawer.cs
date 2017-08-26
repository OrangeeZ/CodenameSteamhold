﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World.EntityFactory;
using UnityEditor;
using UnityEngine;

public class ResourcesDrawer : GuiDrawer
{
    private readonly BaseWorld _world;
    private bool _foldout;
    private Dictionary<ResourceType, string> _resources;

    public ResourcesDrawer(BaseWorld world)
    {
        _resources  =new Dictionary<ResourceType, string>();
        _world = world;
    }

    public override void Draw()
    {
        _foldout = EditorGUILayout.Foldout(_foldout,"Resources");
        if (!_foldout) return;
        GUILayout.BeginVertical();
        var blocks = _world.Stockpile.GetBlocks();
        for (int i = 0; i < blocks.Count; i++)
        {
            var block = blocks[i];
            var resources = block.Resources;
            for (int j = 0; j < resources.Length; j++)
            {
                GUILayout.BeginHorizontal();
                var amount = block[resources[j]];
                var resource = resources[j];
                GUILayout.Label($"{resource} Count : {amount}");
                if (!_resources.ContainsKey(resource))
                    _resources[resource] = string.Empty;
                _resources[resource] = 
                    GUILayout.TextField(_resources[resource], 20);
                if (GUILayout.Button(" + "))
                {
                    int value;
                    if (int.TryParse(_resources[resource], out value))
                    {
                        block.ChangeResource(resource,value);
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndVertical();
    }
}
