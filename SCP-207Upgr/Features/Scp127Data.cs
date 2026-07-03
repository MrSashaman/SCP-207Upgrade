using System.Collections.Generic;
using LabApi.Features.Wrappers;
using MEC;

namespace SCP_207Upgr
{
    internal class Scp127Data
    {
        public Item Weapon;

        public Player Holder;

        public CoroutineHandle Hint;

        public CoroutineHandle Chat;

        public List<IScp127Upgrade> Upgrades { get; } = new();
    }
}