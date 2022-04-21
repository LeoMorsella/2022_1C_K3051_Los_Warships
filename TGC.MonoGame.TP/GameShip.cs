using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.TP;

public class GameShip:Game
{
     public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";
        
        private GraphicsDeviceManager Graphics { get; }
        
        private Model ShipModel { get; set; }
        private Matrix ShipWorld { get; set; }
        private FollowCamera FollowCamera { get; set; }
        
        private Matrix ShipScale { get; set; }
        
        
       
        
        /// <summary>
        ///     Constructor del juego
        /// </summary>
        public GameShip()
        {
            // Se encarga de la configuracion y administracion del Graphics Device
            Graphics = new GraphicsDeviceManager(this);

            // Carpeta donde estan los recursos que vamos a usar
            Content.RootDirectory = "Content";

            // Hace que el mouse sea visible
            IsMouseVisible = true;
        }

 
        protected override void Initialize()
        {
            // Enciendo Back-Face culling
            // Configuro Blend State a Opaco
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
            GraphicsDevice.RasterizerState = rasterizerState;
            GraphicsDevice.BlendState = BlendState.Opaque;

            // Configuro el tamaño de la pantalla
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            Graphics.ApplyChanges();

            // Creo una camaar para seguir a nuestro auto
            FollowCamera = new FollowCamera(GraphicsDevice.Viewport.AspectRatio);

            //Posicion del auto y matriz
            // Configuro la matriz de mundo del auto
            ShipWorld = Matrix.Identity;

            ShipScale = Matrix.CreateScale(0.05f);
        
        
            
            base.Initialize();
        }

    
        protected override void LoadContent()
        {
            

            // La carga de contenido debe ser realizada aca
            ShipModel = Content.Load<Model>(ContentFolder3D + "ShipA/Ship");
            
           
            


            base.LoadContent();
        }

        /// <summary>
        ///     Es llamada N veces por segundo. Generalmente 60 veces pero puede ser configurado.
        ///     La logica general debe ser escrita aca, junto al procesamiento de mouse/teclas
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
        
            // Caputo el estado del teclado
            var keyboardState = Keyboard.GetState();
            
            var deltaTime = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            if (keyboardState.IsKeyDown(Keys.Escape))
                // Salgo del juego
                Exit();


            ShipWorld = ShipScale;


            // Actualizo la camara, enviandole la matriz de mundo del auto
            FollowCamera.Update(gameTime, ShipWorld);

            base.Update(gameTime);
        }
        
        
        /// <summary>
        ///     Llamada para cada frame
        ///     La logica de dibujo debe ir aca.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            // Limpio la pantalla
            GraphicsDevice.Clear(Color.CornflowerBlue);
            

            // El dibujo del auto debe ir aca
            
            
            ShipModel.Draw(ShipWorld,FollowCamera.View,FollowCamera.Projection);

            base.Draw(gameTime);
        }
        
        
        

        /// <summary>
        ///     Libero los recursos cargados
        /// </summary>
        protected override void UnloadContent()
        {
            // Libero los recursos cargados dessde Content Manager
            Content.Unload();

            base.UnloadContent();
        }
}