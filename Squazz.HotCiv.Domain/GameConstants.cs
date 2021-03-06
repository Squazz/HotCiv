﻿using System;

namespace Squazz.HotCiv
{
    public class GameConstants {
        // The size of the world is set permanently to a 16x16 grid 
        public static readonly int Worldsize = 16;
        // Valid unit types
        public const String Archer = "archer";
        public const String Legion = "legion";
        public const String Settler = "settler";
        // Valid terrain types
        public static readonly String Plains = "plains";
        public static readonly String Ocean = "ocean";
        public static readonly String Forest = "forest";
        public static readonly String Hills = "hills";
        public static readonly String Mountains = "mountain";
        // Valid production balance types
        public static readonly String ProductionFocus = "hammer";
        public static readonly String FoodFocus = "apple";
    }
}
