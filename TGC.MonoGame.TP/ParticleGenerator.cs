using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TGC.MonoGame.TP
{
    //Generador de gotas de lluvia en base al ancho de la pantalla y con una densidad
    class ParticleGenerator
    {
        Texture2D texture;

        float spawnWidth;
        float density;

        List<Raindrops> raindrops = new List<Raindrops>();

        float timer;

        Random rand1 = new Random();
        Random rand2 = new Random();

        public ParticleGenerator(Texture2D newTexture, float newSpawnWidth, float newDensity)
        {
            texture = newTexture;
            spawnWidth = newSpawnWidth;
            density = newDensity;
        }

        public void createParticle()
        {
            raindrops.Add(new Raindrops(texture, new Vector2(-50 + (float)rand1.NextDouble() * spawnWidth, 0), new Vector2(1, rand2.Next(5, 8))));

        }

        public void Update(float Time, GraphicsDevice graphics)
        {
            timer += Time;

            while (timer > 0)
            {
                timer -= 1f / density;
                createParticle();
            }
            for(int i = 0; i < raindrops.Count; i++)
            {
                raindrops[i].Update();

                if (raindrops[i].Position.Y>graphics.Viewport.Height)
                {
                    raindrops.RemoveAt(i);
                    i--;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Raindrops raindrop in raindrops)
                raindrop.Draw(spriteBatch);
        }


    }
}
