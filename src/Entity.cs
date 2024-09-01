using System.Collections.Generic;
using System.Net.Mime;
using System.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

abstract class Entity {
    public static ushort healthCap => 1000;

    private int _id;
    public int id => _id;
    
    private Texture2D _sprite;
    public Texture2D sprite => _sprite;

    private List<Equipment> equipment = new();
    public List<Equipment> Equipment => equipment;

    private ushort _health = 100;
    public ushort health => _health;
    private ushort _maxHealth = 100;
    public ushort maxHealth => _maxHealth;   

    public List<KeyValuePair<Texture2D, Vector2>> Draw() {
        return new List<KeyValuePair<Texture2D, Vector2>>()
        {
            new KeyValuePair<Texture2D, Vector2>(_sprite, new Vector2(0, 0))
        };
    }


}