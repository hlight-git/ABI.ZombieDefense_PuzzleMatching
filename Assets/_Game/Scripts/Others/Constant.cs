using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public const int MU_SLOT = 5;
    public const int SELECT_MU_SLOT = MU_SLOT - 1;
    public class Input
    {
        public const float MIN_DRAG_OFFSET_VALUE_TO_MOVE = 300f;
    }
    public class MatchUnit
    {
        public const float UNIT_FILL_SLIDE_SPEED = 5f;
        public const float UNIT_COLLECT_SLIDE_SPEED = 10f;

        public const float MAX_ANGLE_OFFSET_ALLOWED_TO_MOVE = 20;
    }
    public class Tile
    {
        public const float SIZE = 1;
    }
    public class Tag
    {
        public const string MATCH_UNIT = "MatchUnit";
    }
    public class Layer
    {
        public const string MATCH_UNIT = "MatchUnit";
    }
}
