using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TGC.MonoGame.TP;

public class Rectangle:Game
{
    public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";

       /* private const float CarVelocity = 2f;
        private const float CarRotatingVelocity = 0.06f;
         */
       private const float CarVelocity = 2f;
       private const float CarRotatingVelocity = 0.06f;
       private const float CarJumpSpeed = 150f;
       private const float Gravity = 350f;
       

        private GraphicsDeviceManager Graphics { get; }
        
        
        private FollowCamera FollowCamera { get; set; }
       
        private BasicEffect _basicEffect;
        private VertexPositionColor[] _vertices;
        private short[] _indices;
        private Matrix _world = Matrix.Identity;
        private Matrix _view;
        private Matrix _projection;
        private SpriteBatch _spriteBatch;
        

        
        public Rectangle()
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
            
            // Creo una camaar para seguir a nuestro auto
            FollowCamera = new FollowCamera(GraphicsDevice.Viewport.AspectRatio);
            
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.ApplyChanges();
// TODO: Add your initialization logic here
            _vertices = new VertexPositionColor[4];
            _vertices[0] = new VertexPositionColor
            {
                Color = Color.Blue,
                Position = new Vector3(-2, -2, 0)

            };
            _vertices[1] = new VertexPositionColor
            {
                Color = Color.DarkBlue,
                Position = new Vector3(-2, 2, 0)
            };
            _vertices[2] = new VertexPositionColor
            {
                Color = Color.Blue,
                Position = new Vector3(2, 2, 0)
            };
            _vertices[3] = new VertexPositionColor
            {
                Color = Color.Blue,
                Position = new Vector3(2, -2, 0)
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

        /// <summary>
        ///     Llamada una sola vez durante la inicializacion de la aplicacion, luego de Initialize, y una vez que fue configurado GraphicsDevice.
        ///     Debe ser usada para cargar los recursos y otros elementos del contenido.
        /// </summary>
        protected override void LoadContent()
        {
           
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _basicEffect = new BasicEffect(GraphicsDevice);
// TODO: use this.Content to load your game content here
            _view = Matrix.CreateLookAt(new Vector3(0, 0, 8), new Vector3(0, 0, 0), new
                Vector3(0, 1, 0));
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 1920f /
                1080f, 0.01f, 100f);
            _basicEffect.World = _world;
            _basicEffect.View = _view;
            _basicEffect.Projection = _projection;
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

            base.Update(gameTime);
        }
        
        
        /// <summary>
        ///     Llamada para cada frame
        ///     La logica de dibujo debe ir aca.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
// TODO: Add your drawing code here
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices,
                    0, 4, _indices, 0, 2);
            }
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
