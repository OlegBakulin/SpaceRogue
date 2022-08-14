using UnityEngine;
using Abstracts;
using System;

public abstract class AbstractDestroyPlayerView : MonoBehaviour, IDestroyPlayerView
{
    public abstract void Init(Action actionDestroyPlayer);
}
