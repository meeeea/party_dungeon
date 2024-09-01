using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

class PlayState : State {
    private List<Entity> _entities = new();

    private List<Texture2D> _sprites = new();

    private int screenWidth;
    private int screenWidthCenter => screenWidth / 2;
    private int screenHight;
    private int screenHightCenter => screenHight / 2;

    public override void Close()
    {
        throw new System.NotImplementedException();
    }

    public override List<KeyValuePair<Texture2D, Vector2>> Draw() {
        List<KeyValuePair<Texture2D, Vector2>> spriteSet = [.. GenerateTiles()];
        foreach (Entity entity in _entities) {
            spriteSet.AddRange(entity.Draw());
        }
        return spriteSet;
    }

    public override void Load(List<Texture2D> sprites, int width, int hight) {
        _sprites = sprites;
        screenWidth = width;
        screenHight = hight;
        
    }

    public override void Update()
    {

    }

    private List<KeyValuePair<Texture2D, Vector2>> GenerateTiles(int size = 3) {
        int hexWidth = _sprites[0].Width;
        int hexWidthCentered = screenWidthCenter - hexWidth / 2;
        int hexHeight = _sprites[0].Height;
        int hexHeightCentered = screenHightCenter - hexHeight / 2;
        Vector2 centeredHex = new Vector2(hexWidthCentered, hexHeightCentered);
        Vector2 hexRight = new Vector2(hexWidth, 0);
        Vector2 hexUpLeft = new Vector2(- hexWidth / 2, - (int) (hexWidth * 0.86602540378));
        Vector2 hexDownLeft = new Vector2(- hexWidth / 2, (int) (hexWidth * 0.86602540378));

        Vector2 HelperVectorAdder(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        Vector2 HelperVectorSub(Vector2 v1, Vector2 v2) {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        List<KeyValuePair<Texture2D, Vector2>> GenerateRight(int size, int step, Vector2 current) {
            Vector2 newCurrent = HelperVectorAdder(current, hexRight);
            List<KeyValuePair<Texture2D, Vector2>> tiles = [
                new KeyValuePair<Texture2D, Vector2>(_sprites[0], HelperVectorAdder(centeredHex, newCurrent)),
                .. GenerateUpLeft(size, newCurrent),
                .. GenerateDownLeft(size, newCurrent)
            ];
            
            if (size > step) {
                tiles.AddRange(GenerateRight(size, step + 1, newCurrent));
            }
            return tiles;
        }
        
        List<KeyValuePair<Texture2D, Vector2>> GenerateLeft(int size, Vector2 current) {
            Vector2 newCurrent = HelperVectorSub(current, hexRight);
            List<KeyValuePair<Texture2D, Vector2>> tiles = [
                new KeyValuePair<Texture2D, Vector2>(_sprites[0], HelperVectorAdder(centeredHex, newCurrent))
            ];
            if (size > 0) {
                tiles.AddRange(GenerateLeft(size - 1, newCurrent));
                tiles.AddRange(GenerateUpLeft(size - 1, newCurrent));
                tiles.AddRange(GenerateDownLeft(size - 1, newCurrent));
            }
            return tiles;
        }

        List<KeyValuePair<Texture2D, Vector2>> GenerateUpLeft(int size, Vector2 current) {
            Vector2 newCurrent = HelperVectorAdder(current, hexUpLeft);
            List<KeyValuePair<Texture2D, Vector2>> tiles = [
                new KeyValuePair<Texture2D, Vector2>(_sprites[0], HelperVectorAdder(centeredHex, newCurrent)),
            ];
            if (size > 0) {
                tiles.AddRange(GenerateUpLeft(size - 1, newCurrent));
            }

            return tiles;
        }

        List<KeyValuePair<Texture2D, Vector2>> GenerateDownLeft (int size, Vector2 current) {
            Vector2 newCurrent = HelperVectorAdder(current, hexDownLeft);
            List<KeyValuePair<Texture2D, Vector2>> tiles = [
                new KeyValuePair<Texture2D, Vector2>(_sprites[0], HelperVectorAdder(centeredHex, newCurrent)),
            ];
            if (size > 0) {
                tiles.AddRange(GenerateDownLeft(size - 1, newCurrent));
            }

            return tiles;
        }

        List<KeyValuePair<Texture2D, Vector2>> tiles = [
            new KeyValuePair<Texture2D, Vector2>(_sprites[0], centeredHex),
            .. GenerateLeft(size, new Vector2(0, 0)),
            .. GenerateRight(size, 0, new Vector2(0, 0)),
            .. GenerateDownLeft(size, new Vector2(0, 0)),
            .. GenerateUpLeft(size, new Vector2(0, 0))
        ];

        return tiles;
    }
}