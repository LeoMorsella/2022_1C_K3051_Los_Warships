using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.Samples.Cameras;

namespace TGC.MonoGame.Samples.Samples.Heightmaps
{
    /// <summary>
    ///     Create Basic Heightmap:
    ///     Creates a terrain based on a Heightmap texture.
    ///     Apply a texture to color (DiffuseMap) on the ground.
    ///     The texture is parsed and a VertexBuffer is created based on the different heights of the image.
    ///     Author: Matias Leone, Leandro Barbagallo.
    /// </summary>
    public class GameShips : Game
    {

        public const string ContentFolder3D = "Models/";
        public const string ContentFolderEffects = "Effects/";
        public const string ContentFolderMusic = "Music/";
        public const string ContentFolderSounds = "Sounds/";
        public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public const string ContentFolderTextures = "Textures/";

    
        private GraphicsDeviceManager Graphics { get; }
        private BasicEffect Effect { get; set; }
        private Texture2D TerrainTexture { get; set; }
        private VertexBuffer TerrainVertexBuffer { get; set; }
        private IndexBuffer TerrainIndexBuffer { get; set; }
        private FreeCamera Camera { get; set; }
        
        private Model Ship { get; set; }
        private Model Ship2 { get; set; }
        private Model Island1 { get; set; }
        private Model Island2  { get; set; }
        private Model Island3 { get; set; }
        
        private Matrix ShipWorld { get; set; }
        private Matrix ShipWorld2 { get; set; }
        private Matrix IslandWorld1 { get; set; }
        private Matrix IslandWorld2  { get; set; }
        private Matrix IslandWorld3 { get; set; }

        private Matrix ShipScale { get; set; }
        private Matrix IslandScale { get; set; }
        // Positions
        private Vector3 ShipPosition { get; set; }
        private Vector3 ShipPosition2 { get; set; }
        private Vector3 IslandPosition1 { get; set; }


        // Triangle count in this case
        private int PrimitiveCount { get; set; }

        public GameShips()
        {
            // Se encarga de la configuracion y administracion del Graphics Device
            Graphics = new GraphicsDeviceManager(this);

            // Carpeta donde estan los recursos que vamos a usar
            Content.RootDirectory = "Content";

            // Hace que el mouse sea visible
            IsMouseVisible = true;
        }
        /// <inheritdoc />
        protected override void Initialize()
        {
            // Configuro el tamaño de la pantalla
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 100;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100;
            Graphics.ApplyChanges();

            var aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            var width = GraphicsDevice.Viewport.Width;
            var height = GraphicsDevice.Viewport.Height;

            Camera = new FreeCamera(aspectRatio, new Vector3(0,300f,200f) , new Point(width/2,height/2));
            Camera.NearPlane = 1f;
            Camera.FarPlane = 100000f;
            Camera.FieldOfView = 400f;
            Camera.UpDirection = Vector3.UnitY;
            Camera.MovementSpeed = 500f;
            
            
            ShipWorld = Matrix.Identity;
            ShipWorld2 = Matrix.Identity;
            IslandWorld1 = Matrix.Identity;
            IslandWorld2 = Matrix.Identity;
            IslandWorld3 = Matrix.Identity;

            ShipScale = Matrix.CreateScale(0.01f);
            IslandScale = Matrix.CreateScale(0.02f);

            ShipPosition = Vector3.UnitY * 200f;
            ShipPosition2 = new Vector3(200f,200f,-200f);
            IslandPosition1 = new Vector3(400, 200f, 400f);

            base.Initialize();
        }

        /// <inheritdoc />
        protected override void LoadContent()
        {
            // Heightmap texture of the terrain.
            var currentHeightmap = Content.Load<Texture2D>(ContentFolderTextures + "heightmaps/heightmap");

            
            // Cambia el tamaño del mapa IMPORTANTE
            var scaleXZ = 500f;
            var scaleY = 0.4f;
            CreateHeightMapMesh(currentHeightmap, scaleXZ, scaleY);

            // Terrain texture.
            TerrainTexture = Content.Load<Texture2D>(ContentFolderTextures + "heightmaps/terrain-texture-3");

            Effect = new BasicEffect(GraphicsDevice)
            {
                World = Matrix.Identity,
                TextureEnabled = true,
                Texture = TerrainTexture
            };
            Effect.EnableDefaultLighting();

            Ship = Content.Load<Model>(ContentFolder3D + "ShipA/Ship");
       
            Island1 = Content.Load<Model>(ContentFolder3D + "Island1/Island1");
            Island2 = Content.Load<Model>(ContentFolder3D + "Island2/Island2");
            Island3 = Content.Load<Model>(ContentFolder3D + "Island3/Island3");


            ShipWorld = ShipScale * Matrix.CreateTranslation(ShipPosition);
            ShipWorld2 = ShipScale * Matrix.CreateTranslation(ShipPosition2);
            IslandWorld1 = IslandScale * Matrix.CreateTranslation(IslandPosition1);

            base.LoadContent();
        }

        /// <inheritdoc />
        protected override void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);

           
            base.Update(gameTime);
        }

        /// <inheritdoc />
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SetVertexBuffer(TerrainVertexBuffer);
            GraphicsDevice.Indices = TerrainIndexBuffer;

            Ship.Draw(ShipWorld, Camera.View, Camera.Projection);
            Ship.Draw(ShipWorld2, Camera.View, Camera.Projection);
            Island1.Draw(IslandWorld1,Camera.View, Camera.Projection);

            // Render terrain.
            Effect.View = Camera.View;
            Effect.Projection = Camera.Projection;
            
            
            foreach (var pass in Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, PrimitiveCount);
            }

            base.Draw(gameTime);
        }

        /// <summary>
        ///     Create and load the VertexBuffer based on a Heightmap texture.
        /// </summary>
        /// <param name="texture">The Heightmap texture.</param>
        /// <param name="scaleXZ">ScaleXZ is the distance between the vertices in the XZ plane, where the terrain does not rise.</param>
        /// <param name="scaleY">ScaleY is the distance by variability of gray in the heightmap.</param>
        private void CreateHeightMapMesh(Texture2D texture, float scaleXZ, float scaleY)
        {
            // Parse bitmap and load height matrix.
            var heightMap = LoadHeightMap(texture);

            CreateVertexBuffer(heightMap, scaleXZ, scaleY);

            var heightMapWidthMinusOne = heightMap.GetLength(0) - 1;
            var heightMapLengthMinusOne = heightMap.GetLength(1) - 1;

            PrimitiveCount = 2 * heightMapWidthMinusOne * heightMapLengthMinusOne;

            CreateIndexBuffer(heightMapWidthMinusOne, heightMapLengthMinusOne);
        }

        /// <summary>
        ///     Load Bitmap and get the grayscale value of Y for each coordinate (x, z).
        /// </summary>
        /// <param name="texture">The Heightmap texture.</param>
        /// <returns>The height of each vertex from zero to one.</returns>
        private float[,] LoadHeightMap(Texture2D texture)
        {
            var texels = new Color[texture.Width * texture.Height];

            // Obtains each texel color from the texture, note that this is an expensive operation
            texture.GetData(texels);

            var heightmap = new float[texture.Width, texture.Height];

            for (var x = 0; x < texture.Width; x++)
            for (var y = 0; y < texture.Height; y++)
            {
                // Get the color.
                // (j, i) inverted to sweep rows first and then columns.
                var texel = texels[y * texture.Width + x];
                heightmap[x, y] = texel.R;
            }

            return heightmap;
        }

        /// <summary>
        ///     Create a Vertex Buffer from a HeightMap
        /// </summary>
        /// <param name="heightMap">The Heightmap which specifies height for each vertex</param>
        /// <param name="scaleXZ">The distance between the vertices in both the X and Z axis</param>
        /// <param name="scaleY">The scale in the Y axis for the vertices of the HeightMap</param>
        private void CreateVertexBuffer(float[,] heightMap, float scaleXZ, float scaleY)
        {
            var heightMapWidth = heightMap.GetLength(0);
            var heightMapLength = heightMap.GetLength(1);

            var offsetX = heightMapWidth * scaleXZ * 0.5f;
            var offsetZ = heightMapLength * scaleXZ * 0.5f;

            // Amount of subdivisions in X times amount of subdivisions in Z.
            var vertexCount = heightMapWidth * heightMapLength;

            // Create temporary array of vertices.
            var vertices = new VertexPositionTexture[vertexCount];

            var index = 0;
            Vector3 position;
            Vector2 textureCoordinates;

            for (var x = 0; x < heightMapWidth; x++)
            for (var z = 0; z < heightMapLength; z++)
            {
                position = new Vector3(x * scaleXZ - offsetX, heightMap[x, z] * scaleY, z * scaleXZ - offsetZ);
                textureCoordinates = new Vector2((float) x / heightMapWidth, (float) z / heightMapLength);
                vertices[index] = new VertexPositionTexture(position, textureCoordinates);
                index++;
            }

            // Create the actual vertex buffer
            TerrainVertexBuffer = new VertexBuffer(GraphicsDevice, VertexPositionTexture.VertexDeclaration, vertexCount,
                BufferUsage.None);
            TerrainVertexBuffer.SetData(vertices);
        }


        /// <summary>
        ///     Create an Index Buffer for a tesselated plane
        /// </summary>
        /// <param name="quadsInX">The amount of quads in the X axis</param>
        /// <param name="quadsInZ">The amount of quads in the Z axis</param>
        private void CreateIndexBuffer(int quadsInX, int quadsInZ)
        {
            var indexCount = 3 * 2 * quadsInX * quadsInZ;

            var indices = new ushort[indexCount];
            var index = 0;

            int right;
            int top;
            int bottom;

            var vertexCountX = quadsInX + 1;
            for (var x = 0; x < quadsInX; x++)
            for (var z = 0; z < quadsInZ; z++)
            {
                right = x + 1;
                bottom = z * vertexCountX;
                top = (z + 1) * vertexCountX;

                //  d __ c  
                //   | /|
                //   |/_|
                //  a    b

                var a = (ushort) (x + bottom);
                var b = (ushort) (right + bottom);
                var c = (ushort) (right + top);
                var d = (ushort) (x + top);

                // ACB
                indices[index] = a;
                index++;
                indices[index] = c;
                index++;
                indices[index] = b;
                index++;

                // ADC
                indices[index] = a;
                index++;
                indices[index] = d;
                index++;
                indices[index] = c;
                index++;
            }

            TerrainIndexBuffer =
                new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, indexCount, BufferUsage.None);
            TerrainIndexBuffer.SetData(indices);
        }

        /// <inheritdoc />
        protected override void UnloadContent()
        {
            TerrainVertexBuffer.Dispose();

            base.UnloadContent();
        }
    }
}