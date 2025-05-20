using System;
using UnityEngine;
using Zenject;

namespace Game.Upgrades
{
    [Serializable]
    public abstract class CharacterParameterGrades : ScriptableObject, IUpgrade
    {
        public abstract bool IsCompleted { get; }
        public abstract int Level { get; }
        public abstract Sprite Sprite { get; }
        public abstract string LocalizedDescription { get; }
        public abstract void  Init(SignalBus signalBus);
        public abstract void Upgrade();
    }
}