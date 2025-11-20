using UnityEngine;

namespace Globals
{
    #region Game
    public enum InteractionType
    {
        NONE = 0,
        ATTACK = 1,
        ATTACK_BOW = 2,
        ENTER_NEXT = 3,
        ENTER_PRE = 4,
        NPC_HP = 5,
        NPC_WEAPON = 6,
    }

    public enum QuestType
    {
        NONE = 0,
        BOW = 1,
        HP = 2,
        BOSS = 3,
    }

    public enum PlayerState
    {
        DIE = 0,
        LIVE = 1,
    }
    #endregion

    #region UI
    public enum PageType
    {
        NONE = 0,

        // Title
        TITLE = 100,

        // Main
        HUD = 200,
        QUEST = 201,

        // Game

        // BossGame

    }

    public enum PopupType
    {
        NONE = 0,

        // Title

        // Main
        NPC = 200,
        TOAST = 201,
        DIE = 202,
        SETTING = 203,

        // Game

        // BossGame
    }
    #endregion

    #region Sound
    public enum SoundType
    {
        NONE = 0,

        // BGM
        BGM_TITLE = 100,
        BGM_MAIN = 101,
        BGM_GAME = 102,
        BGM_BOSSGAME = 103,

        // SFX
        SFX_ATTACK = 200,
        SFX_ATTACK_BOW = 201,
        SFX_BUY_BOW = 202,
        SFX_BUY_HEAL = 203,
        SFX_CLICK = 204,
        SFX_COIN = 205,
        SFX_LEVELUP = 206,
        SFX_WALK = 207,
        SFX_NO = 208,
    }
    #endregion
}