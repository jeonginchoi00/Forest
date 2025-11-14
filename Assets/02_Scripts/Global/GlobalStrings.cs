using UnityEngine;

namespace Globals
{
    public static class SceneName
    {
        public const string TITLE = "Title";
        public const string MAIN = "Main";
        public const string GAME = "Game";
        public const string BOSSGAME = "BossGame";
    }

    public static class InputType
    {
        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL = "Vertical";
    }

    public static class AnimKey
    {
        // Player
        public const string AXISX = "AxisX";
        public const string AXISY = "AxisY";
        public const string ISMOVE = "IsMove";
        public const string ATTACK = "Attack";
        public const string ATTACK_BOW = "Attack_Bow";

        // Enemy
        public const string JUMP_ATTACK = "JumpAttack";
        public const string HURT = "Hurt";
        public const string DEATH = "Death";
    }

    public static class Tag
    {
        public const string DOOR_NEXT = "DOOR_NEXT";
        public const string DOOR_PRE = "DOOR_PRE";
        public const string ARROW = "ARROW";
        public const string PLAYER = "PLAYER";
    }

    public static class UserInfoKey
    {
        public const string USER_COIN = "USER_COIN";
    }
}