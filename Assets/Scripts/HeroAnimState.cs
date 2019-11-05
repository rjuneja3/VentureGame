////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: HeroAnimState.cs
//FileType: Visual C# Source file
//Author : Rohan Juneja - 300987725
//Created On : 11/02/2019
//Last Modified On : 11/04/2019
//Copy Rights : Artificial Order
//Description : Class for defining Hero Animations
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    [System.Serializable]
    public enum HeroAnimState
    {
        IDLE,
        WALK,
        JUMP
    }
}
