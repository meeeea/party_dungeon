using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class StateMachine {
    private static List<State> _states = new(){new PlayState()};
    private static List<Texture2D> _sprites = new();
    private static int _currentState = 0;

    private static int screenWidth;
    private static int screenHight;

    public static void SwichState(int targetStateId) {
        _states[_currentState].Close();
        _currentState = targetStateId;
        Load(_sprites);
    }

    public static void Initialize(int width, int hight) {
        screenWidth = width;
        screenHight = hight;
    }

    public static void Load(List<Texture2D> sprites) {
        _states[_currentState].Load(sprites, 2560, 1600);
        _sprites = sprites;
    }

    public static void Update() {
        _states[_currentState].Update();
    }

    public static List<KeyValuePair<Texture2D, Vector2>> Draw() {
        return _states[_currentState].Draw();
    }
}