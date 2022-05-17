using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TGC.MonoGame.TP
{
    //Clase que define una gota de lluvia como un Sprite con textura, posicion y velocidad
    class Raindrops
    {
        Texture2D texture;

        Vector2 position;
        Vector2 velocity;

        public Vector2 Position
        {
            get { return position; }
        }

        public Raindrops (Texture2D newTexture, Vector2 newPosition, Vector2 newVelocity)
        {
            texture = newTexture;
            position = newPosition;
            velocity = newVelocity;
        }

        public void Update()
        {
            position += velocity;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
