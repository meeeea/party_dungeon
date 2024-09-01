using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace party_dungeon;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private List<Texture2D> _sprites = new();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferWidth = 2560;
        _graphics.PreferredBackBufferHeight = 1600;
        _graphics.ApplyChanges();
        base.Initialize();

        StateMachine.Initialize(2560, 1600);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _sprites.Add(Content.Load<Texture2D>("hex"));

        StateMachine.Load(_sprites);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        StateMachine.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);

        List<KeyValuePair<Texture2D, Vector2>> dict = StateMachine.Draw();

        _spriteBatch.Begin();
        
        foreach (KeyValuePair<Texture2D, Vector2> s in dict) {
            _spriteBatch.Draw(s.Key, s.Value, Color.White);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
