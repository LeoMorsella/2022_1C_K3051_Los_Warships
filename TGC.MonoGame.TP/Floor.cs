using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TGC.MonoGame.Samples.Cameras;
using TGC.MonoGame.Samples.Geometries.Textures;

namespace TGC.MonoGame.TP;

public class QuadFloor:Game
{
        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";


        /// <summary>
        ///     Constructor del juego
        /// </summary>
        public QuadFloor()
        {
            // Se encarga de la configuracion y administracion del Graphics Device
            Graphics = new GraphicsDeviceManager(this);

            // Carpeta donde estan los recursos que vamos a usar
            Content.RootDirectory = "Content";

            // Hace que el mouse sea visible
            IsMouseVisible = true;
        }

        private Camera Camera;
      private GraphicsDeviceManager Graphics { get; }
      private SpriteBatch SpriteBatch { get; set; }
      private Model Model { get; set; }
      private Effect Effect { get; set; }
      private float Rotation { get; set; }
      private Matrix World { get; set; }
      private Matrix View { get; set; }
      private Matrix Projection { get; set; }
        
      private VertexBuffer VertexBuffer { get; set; }
      private IndexBuffer IndexBuffer { get; set; }
 
        protected override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            Graphics.ApplyChanges();
            // Apago el backface culling.
            // Esto se hace por un problema en el diseno del modelo del logo de la materia.
            // Una vez que empiecen su juego, esto no es mas necesario y lo pueden sacar.
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;
            // Seria hasta aca.

            // Configuramos nuestras matrices de la escena.
            World = Matrix.Identity;
            View = Matrix.CreateLookAt(Vector3.UnitZ * 20, Vector3.Zero, Vector3.Up);
            Projection =
                Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsDevice.Viewport.AspectRatio, 1, 250);

            var size = GraphicsDevice.Viewport.Bounds.Size;
            size.X /= 2;
            size.Y /= 2;
            Camera = new FreeCamera(GraphicsDevice.Viewport.AspectRatio, new Vector3(0, 40, 200), size);

            base.Initialize();
        }

    
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Cargo el modelo del logo.
            // Model = Content.Load<Model>(ContentFolder3D + "tgc-logo/tgc-logo");

            // Cargo un efecto basico propio declarado en el Content pipeline.
            // En el juego no pueden usar BasicEffect de MG, deben usar siempre efectos propios.
            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            var vertices = new VertexPositionColorTexture[]
            {
                new VertexPositionColorTexture(new Vector3(-1f,-1f,0f),Color.Red,Vector2.Zero),
                new VertexPositionColorTexture(new Vector3(-1f,1f,0f),Color.Blue,Vector2.UnitY),
                new VertexPositionColorTexture(new Vector3(1f,1f,0f),Color.Green,Vector2.One),
                new VertexPositionColorTexture(new Vector3(1f,-1f,0f),Color.Yellow,Vector2.UnitX),
            };

            VertexBuffer = new VertexBuffer(GraphicsDevice, VertexPositionColorTexture.VertexDeclaration, 4,
                BufferUsage.None);
            VertexBuffer.SetData(vertices);

            //var v = new VertexPositionColorTexture[4];
            //VertexBuffer.GetData(v);

            var indices = new ushort[]
            {
                1, 3, 0,
                1, 2, 3,
            };

            IndexBuffer = new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, 6, BufferUsage.None);
            IndexBuffer.SetData(indices);

            Effect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

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
            
           

            
            
            Camera.Update(gameTime);
            base.Update(gameTime);
        }
        
        /// <summary>
        ///     Llamada para cada frame
        ///     La logica de dibujo debe ir aca.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            // Limpio la pantalla
            GraphicsDevice.Clear(Color.Black);
            
            Effect.Parameters["World"].SetValue(World);
            Effect.Parameters["View"].SetValue(View);
            Effect.Parameters["Projection"].SetValue(Projection);
            Effect.Parameters["DiffuseColor"].SetValue(Color.DarkBlue.ToVector3());
            
            GraphicsDevice.SetVertexBuffer(VertexBuffer);
            GraphicsDevice.Indices = IndexBuffer;

            foreach (var effectPass in Effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,0,0,2);
            }


           
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