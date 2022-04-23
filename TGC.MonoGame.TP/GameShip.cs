using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Cameras;
using TGC.MonoGame.Samples.Geometries.Textures;

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
        private FreeCamera FreeCamera { get; set; } 
        
        public Matrix View { get; private set; }
        public Matrix Projection { get; private set; }
        private Matrix ShipScale { get; set; }
        
        private Vector3 ShipPosition { get; set; }
        private QuadPrimitive QuadPrimitive { get; set; }
        private BoxPrimitive BoxPrimitive { get; set; } 
        
        //Dibujar Mar
        private BasicEffect _basicEffect;
        private VertexPositionColor[] _vertices;
        private short[] _indices;
        private Matrix _world = Matrix.Identity;
        private Matrix _view;
        private Matrix _projection;
        private SpriteBatch _spriteBatch;        
        
        
        
        
       
        
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
            FreeCamera = new FreeCamera(GraphicsDevice.Viewport.AspectRatio, Vector3.UnitZ * 1000, new Point(GraphicsDevice.Viewport.Width/2,GraphicsDevice.Viewport.Height/2));
            FreeCamera.MovementSpeed = 1000f;

            ShipWorld = Matrix.Identity;

            QuadPrimitive = new QuadPrimitive(GraphicsDevice);
            
            

            //Posicion del auto y matriz
            // Configuro la matriz de mundo del auto
            
            ShipScale = Matrix.CreateScale(0.05f);

            // Mar
            
            _vertices = new VertexPositionColor[4];
            _vertices[0] = new VertexPositionColor
            {
                Color = Color.Blue,
                Position = new Vector3(-500f, 0,-500f)

            };
            _vertices[1] = new VertexPositionColor
            {
                Color = Color.DarkBlue,
                Position = new Vector3(-500f,0,500f)
            };
            _vertices[2] = new VertexPositionColor
            {
                Color = Color.Blue,
                Position = new Vector3(500f,0,500f)
            };
            _vertices[3] = new VertexPositionColor
            {
                Color = Color.Blue,
                Position = new Vector3(500f,0,-500f)
            };
            _indices = new short[6];
            _indices[0] = 0;
            _indices[1] = 1;
            _indices[2] = 2;
            _indices[3] = 2;
            _indices[4] = 3;
            _indices[5] = 0;
        
            base.Initialize();
        }

    
        protected override void LoadContent()
        {
            
            // La carga de contenido debe ser realizada aca
            ShipModel = Content.Load<Model>(ContentFolder3D + "ShipA/Ship");
 
            // Mar
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _basicEffect = new BasicEffect(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            _basicEffect.World = _world;
            _basicEffect.View = FreeCamera.View;
            _basicEffect.Projection = FreeCamera.Projection;
            _basicEffect.VertexColorEnabled = true;
          
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
            FreeCamera.Update(gameTime);

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
               
            ShipModel.Draw(ShipWorld,FreeCamera.View,FreeCamera.Projection);
            
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices,
                    0, 4, _indices, 0, 2);
            }

           //Quad.Draw(FloorWorld*viewProjection);
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