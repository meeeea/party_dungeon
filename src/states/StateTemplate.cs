using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

abstract class State {
    public abstract void Close();

    public abstract void Load(List<Texture2D> sprites, int width, int hight);

    public abstract void Update();

    public abstract List<KeyValuePair<Texture2D, Vector2>> Draw();
}