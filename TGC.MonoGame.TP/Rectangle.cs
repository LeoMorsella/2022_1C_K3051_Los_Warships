using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Cameras;

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
        
        private Camera Camera { get; set; }
    
        private VertexBuffer Vertices { get; set; }
    
        private IndexBuffer Indices { get; set; }
    
        private BasicEffect Effect { get; set; }

        private Matrix BoxWorld
        {
            get;
            set;

        } = Matrix.Identity;
        

        
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
            
            
            Camera = new TargetCamera(GraphicsDevice.Viewport.AspectRatio, new Vector3(0, 20, 60), Vector3.Zero);

            Effect = new BasicEffect(GraphicsDevice);
            Effect.VertexColorEnabled = true;
        
            CreateVertexBuffer(Vector3.One * 25, Vector3.Zero, Color.Cyan, Color.Cyan, Color.Cyan, Color.Cyan,
                Color.Cyan, Color.Cyan, Color.Cyan, Color.Cyan);
            CreateIndexBuffer(GraphicsDevice);

            

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

           
            // Press Directional Keys to rotate cube
            if (keyboardState.IsKeyDown(Keys.Up)) BoxWorld *= Matrix.CreateRotationX(-0.05f);

            if (keyboardState.IsKeyDown(Keys.Down)) BoxWorld *= Matrix.CreateRotationX(0.05f);

            if (keyboardState.IsKeyDown(Keys.Left)) BoxWorld *= Matrix.CreateRotationY(-0.05f);

            if (keyboardState.IsKeyDown(Keys.Right)) BoxWorld *= Matrix.CreateRotationY(0.05f);
            
            
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

            GraphicsDevice.SetVertexBuffer(Vertices);
            GraphicsDevice.Indices = Indices;

            Effect.World = BoxWorld;
            Effect.View = Camera.View;
            Effect.Projection = Camera.Projection;

            foreach (var pass in Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, Indices.IndexCount / 3);
            }
            base.Draw(gameTime);
        }

        /// <summary>
        ///     Libero los recursos cargados
        /// </summary>
        ///
        private void CreateVertexBuffer(Vector3 size, Vector3 center, Color color1, Color color2, Color color3,
            Color color4, Color color5, Color color6, Color color7, Color color8)
        {
            var x = size.X / 2;
            var y = size.Y / 2;
            var z = size.Z / 2;

            var cubeVertices = new[]
            {
                // Bottom-Left Front.
                new VertexPositionColor(new Vector3(-x + center.X, -y + center.Y, -z + center.Z), color1),
                // Bottom-Left Back.
                new VertexPositionColor(new Vector3(-x + center.X, -y + center.Y, z + center.Z), color2),
                // Bottom-Right Back.
                new VertexPositionColor(new Vector3(x + center.X, -y + center.Y, z + center.Z), color3),
                // Bottom-Right Front.
                new VertexPositionColor(new Vector3(x + center.X, -y + center.Y, -z + center.Z), color4),
                // Top-Left Front.
                new VertexPositionColor(new Vector3(-x + center.X, y + center.Y, -z + center.Z), color5),
                // Top-Left Back.
                new VertexPositionColor(new Vector3(-x + center.X, y + center.Y, z + center.Z), color6),
                // Top-Right Back.
                new VertexPositionColor(new Vector3(x + center.X, y + center.Y, z + center.Z), color7),
                // Top-Right Front.
                new VertexPositionColor(new Vector3(x + center.X, y + center.Y, -z + center.Z), color8)
            };

            Vertices = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, cubeVertices.Length,
                BufferUsage.WriteOnly);
            Vertices.SetData(cubeVertices);
        }

        /// <summary>
        ///     Create an index buffer for the vertex buffer that the figure has.
        /// </summary>
        /// <param name="device">The GraphicsDevice object to associate with the index buffer.</param>
        private void CreateIndexBuffer(GraphicsDevice device)
        {
            var cubeIndices = new ushort[]
            {
                // Bottom face.
                0, 2, 3, 0, 1, 2,
                // Top face.
               4, 6, 5, 4, 7, 6,
               
                // Front face.
                5, 2, 1, 5, 6, 2,
                // Back face.
               0, 7, 4, 0, 3, 7,
               
               
               // Left face.
                0, 4, 1, 1, 4, 5,
                // Right face.
                2, 6, 3, 3, 6, 7
            };

            Indices = new IndexBuffer(device, IndexElementSize.SixteenBits, cubeIndices.Length, BufferUsage.WriteOnly);
            Indices.SetData(cubeIndices);
        }
        protected override void UnloadContent()
        {
            // Libero los recursos cargados dessde Content Manager
            Content.Unload();

            base.UnloadContent();
        }
    }
