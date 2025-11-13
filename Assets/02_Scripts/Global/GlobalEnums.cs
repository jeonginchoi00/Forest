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

        // Game

        // BossGame

    }

    public enum PopupType
    {
        NONE = 0,

    }
    #endregion

    #region Sound
    public enum SoundType
    {
        NONE = 0,

    }
    #endregion
}