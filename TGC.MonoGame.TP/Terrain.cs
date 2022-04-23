using System;
using BepuPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Cameras;
using TGC.MonoGame.Samples.Geometries.Textures;

namespace TGC.MonoGame.TP;

public class Terrain:Game
{
    public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";

    
        private GraphicsDeviceManager Graphics { get; }
        
        
        private QuadPrimitive Floor { get; set; }

        private Matrix FloorWorld
        {
            get; set;
        }
        
        private Matrix World { get; set; }
        private Matrix View { get; set; }
        private Matrix Projection { get; set; }
        
        private Model Ship { get; set; }
        
        private Effect TilingEffect { get; set; }
        
        private FreeCamera Camera { get; set; }
        
        
        public Terrain()
        {
            // Se encarga de la configuracion y administracion del Graphics Device
            Graphics = new GraphicsDeviceManager(this);

            // Carpeta donde estan los recursos que vamos a usar
            Content.RootDirectory = "Content";

            // Hace que el mouse sea visible
            IsMouseVisible = true;
        }

        /// <summary>
        ///     Llamada una vez en la inicializacion de la aplicacion.
        ///     Escribir aca todo el codigo de inicializacion: Todo lo que debe estar precalculado para la aplicacion.
        /// </summary>
        protected override void Initialize()
        {
            // Enciendo Back-Face culling
            // Configuro Blend State a Opaco
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
            GraphicsDevice.RasterizerState = rasterizerState;
            GraphicsDevice.BlendState = BlendState.Opaque;

            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.ApplyChanges();
            
            World = Matrix.Identity;
            View = Matrix.CreateLookAt(Vector3.UnitZ * 20, Vector3.Zero, Vector3.Up);
            Projection =
                Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 250);
            

            base.Initialize();
        }

        /// <summary>
        ///     Llamada una sola vez durante la inicializacion de la aplicacion, luego de Initialize, y una vez que fue configurado GraphicsDevice.
        ///     Debe ser usada para cargar los recursos y otros elementos del contenido.
        /// </summary>
        protected override void LoadContent()
        {
            var floorTexture = Content.Load<Texture2D>(ContentFolderTextures + "stones");
            Floor = new QuadPrimitive(GraphicsDevice);

            TilingEffect = Content.Load<Effect>(ContentFolderEffects + "TextureTiling");
            TilingEffect.Parameters["Texture"].SetValue(floorTexture);
            TilingEffect.Parameters["Tiling"].SetValue(Vector2.One * 50f);

            FloorWorld = Matrix.CreateScale(400f) * Matrix.CreateTranslation(new Vector3(75, 0, -150));
           


            base.LoadContent();
        }

        /// <summary>
        ///     Es llamada N veces por segundo. Generalmente 60 veces pero puede ser configurado.
        ///     La logica general debe ser escrita aca, junto al procesamiento de mouse/teclas
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            //Camera.Update(gameTime);
            // Caputo el estado del teclado
            var keyboardState = Keyboard.GetState();
            
            
            if (keyboardState.IsKeyDown(Keys.Escape))
                // Salgo del juego
                Exit();

            base.Update(gameTime);
        }
        
        
        /// <summary>
        ///     Llamada para cada frame
        ///     La logica de dibujo debe ir aca.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            //TilingEffect.Parameters["World"].SetValue(World);
            //TilingEffect.Parameters["View"].SetValue(View);
            //TilingEffect.Parameters["Projection"].SetValue(Projection);
            TilingEffect.Parameters["WorldViewProjection"].SetValue(FloorWorld*Camera.View*Camera.Projection);
            Floor.Draw(TilingEffect);
           
            base.Draw(gameTime);
        }

        
        protected override void UnloadContent()
        {
            // Libero los recursos cargados dessde Content Manager
            Content.Unload();

            base.UnloadContent();
        }
    }
