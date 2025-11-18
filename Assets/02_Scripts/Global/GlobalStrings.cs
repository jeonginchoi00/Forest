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

    public static class SceneInfo
    {
        public const string MAIN_NAME = "마을";
        public const string GAME_NAME = "슬라임의 숲";
        public const string BOSSGAME_NAME = "슬라임의 동굴";
    }
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

public static class Layer
{
    public const string ENEMY = "ENEMY";
}

public static class UserInfoKey
{
    public const string USER_COIN = "USER_COIN";
    public const string USER_LEVEL = "USER_LEVEL";
    public const string USER_MAXHP = "USER_MAXHP";
    public const string USER_CURRENTHP = "USER_CURRENTHP";
    public const string USER_MAXEXP = "USER_MAXEXP";
    public const string USER_CURRENTEXP = "USER_CURRENTEXP";
    public const string USER_BOW = "USER_BOW";
}

public static class PopupString
{
    // NPC
    public const string NPC_HP = "치유해줄까?";
    public const string NPC_WEAPON = "활이 필요하니?";
    public const string NPC_HASWEAPON = "활은 잘 사용하고 있니?\n슬라임을 더 편하게 잡을 수 있겠구나!";
}

public static class ToastString
{
    // NPC
    public const string NPC_HP_O = "치유되었습니다!";
    public const string NPC_HP_X = "돈이 부족합니다.";
    public const string NPC_WEAPON_O = "활을 구매하였습니다!";
    public const string NPC_WEAPON_X_COIN = "돈이 부족합니다.";
    public const string NPC_WEAPON_X_LEVEL = "레벨이 낮습니다.";
}

public static class Quest
{
    public const string QUEST_BOW = "톨비에게 활을 구매하세요.";
    public const string QUEST_HP = "아나에게 HP를 회복하세요.";
}
