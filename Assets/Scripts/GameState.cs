// Variables of the game state.
public static class GameState
{
    public static GameMode GameMode = GameMode.MainMenu;
    public static bool GameMusic = true;
    public static bool MenuMusic = true;
}

public enum GameMode
{
    MainMenu,
    NormalModeMenu,
    TestModeMenu,
    LoadGameMenu,
    SettingMenu,
    LoadGame,
    NormalMode,
    TestMode
}