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

        // Game

        // BossGame
    }
    #endregion

    #region Sound
    public enum SoundType
    {
        NONE = 0,

    }
    #endregion
}