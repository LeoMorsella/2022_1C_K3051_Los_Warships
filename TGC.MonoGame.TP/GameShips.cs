using BepuPhysics.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.Samples.Cameras;
using TGC.MonoGame.Samples.Geometries;
using TGC.MonoGame.Samples.Geometries.Textures;

namespace TGC.MonoGame.Samples.Samples.Heightmaps;

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
        private Model Ship2 { get; set; } // Este modelo carga mal
        private Model Island1 { get; set; }
        private Model Island2  { get; set; }
        private Model Island3 { get; set; }
        
        // Matrices
        private Matrix ShipWorld { get; set; }
        private Matrix ShipWorld2 { get; set; }
        private Matrix ShipWorld3 { get; set; }
        private Matrix ShipWorld4 { get; set; }
        
        private Matrix ShipWorld5 { get; set; }
        private Matrix ShipWorld6 { get; set; }
        private Matrix ShipWorld7 { get; set; }
        private Matrix ShipWorld8 { get; set; }
        private Matrix IslandWorld1 { get; set; }
        private Matrix IslandWorld2  { get; set; }
        private Matrix IslandWorld3 { get; set; }
        
        private Matrix IslandWorld4 { get; set; }
        private Matrix IslandWorld5 { get; set; }
        private Matrix IslandWorld6 { get; set; }
        private Matrix IslandWorld7 { get; set; }
        private Matrix IslandWorld8 { get; set; }
        private Matrix IslandWorld9 { get; set; }
        private Matrix IslandWorld10 { get; set; }
        private Matrix IslandWorld11 { get; set; }
        private Matrix IslandWorld12 { get; set; }
        private Matrix IslandWorld13 { get; set; }
        private Matrix IslandWorld14 { get; set; }
        private Matrix IslandWorld15 { get; set; }
        private Matrix IslandWorld16 { get; set; }
        private Matrix IslandWorld17 { get; set; }
        private Matrix IslandWorld18 { get; set; }
        private Matrix IslandWorld19 { get; set; }
        private Matrix IslandWorld20 { get; set; }
        



    // Matrices
    private Matrix ShipScale { get; set; }
        private Matrix IslandScale { get; set; }
        
        // Positions
        private Vector3 ShipPosition { get; set; }
        private Vector3 ShipPosition2 { get; set; }
        private Vector3 ShipPosition3 { get; set; }
        private Vector3 ShipPosition4 { get; set; }
        
        private Vector3 ShipPosition5 { get; set; }
        private Vector3 ShipPosition6 { get; set; }
        private Vector3 ShipPosition7 { get; set; }
        private Vector3 ShipPosition8 { get; set; }

        private Vector3 IslandPosition1 { get; set; }
        private Vector3 IslandPosition2 { get; set; }
        private Vector3 IslandPosition3 { get; set; }
        
        private Vector3 IslandPosition4 { get; set; }
        private Vector3 IslandPosition5 { get; set; }
        private Vector3 IslandPosition6 { get; set; }
        private Vector3 IslandPosition7 { get; set; }
        private Vector3 IslandPosition8 { get; set; }
        private Vector3 IslandPosition9 { get; set; }
        private Vector3 IslandPosition10 { get; set; }
        private Vector3 IslandPosition11 { get; set; }
        private Vector3 IslandPosition12 { get; set; }
        private Vector3 IslandPosition13 { get; set; }
        private Vector3 IslandPosition14 { get; set; }
        private Vector3 IslandPosition15 { get; set; }
        private Vector3 IslandPosition16 { get; set; }
        private Vector3 IslandPosition17 { get; set; }
        private Vector3 IslandPosition18 { get; set; }
        private Vector3 IslandPosition19 { get; set; }
        private Vector3 IslandPosition20 { get; set; }
    // Prmitivas
    private CylinderPrimitive Cilindro { get; set; }
        private Vector3 CilindroPosition1 { get; set; }
        private CylinderPrimitive Cilindro2 { get; set; }
        private Vector3 CilindroPosition2 { get; set; }
        private CylinderPrimitive Cilindro3 { get; set; }
        private Vector3 CilindroPosition3 { get; set; }
        private CylinderPrimitive Cilindro4 { get; set; }
        private Vector3 CilindroPosition4 { get; set; }
        
        private CylinderPrimitive Cilindro5 { get; set; }
        private Vector3 CilindroPosition5 { get; set; }
        private CylinderPrimitive Cilindro6 { get; set; }
        private Vector3 CilindroPosition6 { get; set; }
        private CylinderPrimitive Cilindro7 { get; set; }
        private Vector3 CilindroPosition7 { get; set; }
        private CylinderPrimitive Cilindro8 { get; set; }
        private Vector3 CilindroPosition8 { get; set; }



    private SpherePrimitive Sphere { get; set; }
        private Vector3 SpherePosition { get; set; }
        private SpherePrimitive Sphere2 { get; set; }
        private Vector3 SpherePosition2 { get; set; }
        
        private SpherePrimitive Sphere3 { get; set; }
        private Vector3 SpherePosition3 { get; set; }
        
        private SpherePrimitive Sphere4 { get; set; }
        private Vector3 SpherePosition4 { get; set; }
        private SpherePrimitive Sphere5 { get; set; }
        private Vector3 SpherePosition5 { get; set; }
        private SpherePrimitive Sphere6 { get; set; }
        private Vector3 SpherePosition6 { get; set; }
        private SpherePrimitive Sphere7 { get; set; }
        private Vector3 SpherePosition7 { get; set; }
        private SpherePrimitive Sphere8 { get; set; }
        private Vector3 SpherePosition8 { get; set; }
        private QuadPrimitive Quad { get; set; }
        private Matrix FloorWorld { get; set; }
        private Texture2D SeaTexture { get; set; }
        private Effect TilingEffect { get; set; }


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
            Camera.FarPlane = 100000000000f;
            Camera.FieldOfView = MathHelper.ToRadians(60f);
            Camera.MovementSpeed = 500f;
            
            
            ShipWorld = Matrix.Identity;
            ShipWorld2 = Matrix.Identity;
            ShipWorld3 = Matrix.Identity;
            ShipWorld4 = Matrix.Identity;
            ShipWorld5 = Matrix.Identity;
            ShipWorld6 = Matrix.Identity;
            ShipWorld7 = Matrix.Identity;
            ShipWorld8 = Matrix.Identity;
            IslandWorld1 = Matrix.Identity;
            IslandWorld2 = Matrix.Identity;
            IslandWorld3 = Matrix.Identity;
            IslandWorld4 = Matrix.Identity;
            IslandWorld5 = Matrix.Identity;
            IslandWorld6 = Matrix.Identity;
            IslandWorld7 = Matrix.Identity;
            IslandWorld8 = Matrix.Identity;
            IslandWorld9 = Matrix.Identity;
            IslandWorld10 = Matrix.Identity;
            IslandWorld11 = Matrix.Identity;
            IslandWorld12 = Matrix.Identity;
            IslandWorld13 = Matrix.Identity;
            IslandWorld14 = Matrix.Identity;
            IslandWorld15 = Matrix.Identity;
            IslandWorld16 = Matrix.Identity;
            IslandWorld17 = Matrix.Identity;
            IslandWorld18 = Matrix.Identity;
            IslandWorld19 = Matrix.Identity;
            IslandWorld20 = Matrix.Identity;

        ShipScale = Matrix.CreateScale(0.01f);
        IslandScale = Matrix.CreateScale(0.04f);

            ShipPosition = Vector3.UnitY * 200f;
            ShipPosition2 = new Vector3(0,200f,-200f);
            ShipPosition3 = new Vector3(200f, 200f, 200f);
            ShipPosition4 = new Vector3(-400f, 200f, -200f);
            ShipPosition5 = new Vector3(600f, 200f, 300f);
            ShipPosition6 = new Vector3(300f, 200f, 1000f);
            ShipPosition7 = new Vector3(400f, 200f, 1500f);
            ShipPosition8 = new Vector3(-500f, 200f, 1200f);
            IslandPosition1 = new Vector3(800f, 200f, 800f);
            IslandPosition2 = new Vector3(-800f, 200f, 1000f);
            IslandPosition3 = new Vector3(800f, 200f, -800f);
            IslandPosition4 = new Vector3(1200f, 200f, 300f);
            IslandPosition5 = new Vector3(-1100f, 200f, 400f);
            IslandPosition6 = new Vector3(-1000f, 200f, -900f);
            IslandPosition7 = new Vector3(-1000f, 200f, -910f);
            IslandPosition8 = new Vector3(-1000f, 200f, -920f);
            IslandPosition9 = new Vector3(100f, 200f, 2000f);
            IslandPosition10 = new Vector3(500f, 200f, 3000f);
            IslandPosition11 = new Vector3(700f, 195f, 3500f);
            IslandPosition12 = new Vector3(-800f, 195f, 2900f);
        IslandPosition13 = new Vector3(-800f, 185f, 2500f);
        IslandPosition14 = new Vector3(-300f, 195f, -1800f);
        IslandPosition15 = new Vector3(-1100f,200f, -2500f);
        IslandPosition16 = new Vector3(-2100f, 200f, -3000f);
        IslandPosition17 = new Vector3(-2800f, 200f, -2200f);
        IslandPosition18 = new Vector3(-1000f, 200f, -3500f);
        IslandPosition19 = new Vector3(-1400f, 190f, -4100f);
        IslandPosition20 = new Vector3(100f, 200f, -4500f);


        Cilindro = new CylinderPrimitive(GraphicsDevice, 100, 4, 32);
        CilindroPosition1 = new Vector3(-750f, 200f, 800f);
        Cilindro2 = new CylinderPrimitive(GraphicsDevice, 25, 4, 32);
        CilindroPosition2 = new Vector3(-600f, 200f, 900f);
        Cilindro3 = new CylinderPrimitive(GraphicsDevice, 25, 4, 32);
        CilindroPosition3 = new Vector3(-900f, 200f, 700f);
        Cilindro4 = new CylinderPrimitive(GraphicsDevice, 25, 4, 32);
        CilindroPosition4 = new Vector3(-600f, 200f, 700f);
        Cilindro5 = new CylinderPrimitive(GraphicsDevice, 25, 4, 32);
        CilindroPosition5 = new Vector3(1250f, 200f, 350f);
        Cilindro6 = new CylinderPrimitive(GraphicsDevice, 25, 4, 32);
        CilindroPosition6 = new Vector3(-1400f, 200f, -4100f);
        Cilindro7 = new CylinderPrimitive(GraphicsDevice, 90, 4, 32);
        CilindroPosition7 = new Vector3(-1300f, 200f, -3500f);
        Cilindro8 = new CylinderPrimitive(GraphicsDevice, 25, 4, 32);
        CilindroPosition8 = new Vector3(-2100f, 200f, -3000f);


        Sphere = new SpherePrimitive(GraphicsDevice, 10, 32);
            SpherePosition = new Vector3(-700f, 200f, 850f);
            Sphere2 = new SpherePrimitive(GraphicsDevice, 10, 32);
            SpherePosition2 = new Vector3(-700f, 200f, 750f);

            Sphere3 = new SpherePrimitive(GraphicsDevice, 10, 32);
            SpherePosition3 = new Vector3(1225f, 200f, 325f);

            Sphere4 = new SpherePrimitive(GraphicsDevice, 10, 32);
            SpherePosition4 = new Vector3(1275f, 200f, 310f);

        Sphere5= new SpherePrimitive(GraphicsDevice, 10, 32);
        SpherePosition5 = new Vector3(100f, 200f, -4500f);

        Sphere6 = new SpherePrimitive(GraphicsDevice, 10, 32);
        SpherePosition6 = new Vector3(-720f, 200f, -3600f);

        Sphere7 = new SpherePrimitive(GraphicsDevice, 10, 32);
        SpherePosition7 = new Vector3(-710f, 200f, -3620f);

        Sphere8 = new SpherePrimitive(GraphicsDevice, 10, 32);
        SpherePosition8 = new Vector3(-720f, 200f, -3640f);

        FloorWorld = Matrix.CreateScale(10000f, 0.1f, 10000f) * Matrix.CreateTranslation(0,185f,0);

        base.Initialize();
    }

        /// <inheritdoc />
        protected override void LoadContent()
        {
            // Heightmap texture of the terrain.
            var currentHeightmap = Content.Load<Texture2D>(ContentFolderTextures + "heightmaps/heightmap-3");

            
            // Cambia el tamaño del mapa IMPORTANTE
            var scaleXZ = 500f;
            var scaleY = 0.4f;
            CreateHeightMapMesh(currentHeightmap, scaleXZ, scaleY);

        // Terrain texture.
        TerrainTexture = Content.Load<Texture2D>(ContentFolderTextures + "heightmaps/terrain-texture-3");

           

            Ship = Content.Load<Model>(ContentFolder3D + "ShipA/Ship");
       
            Island1 = Content.Load<Model>(ContentFolder3D + "Island1/Island1");
            Island2 = Content.Load<Model>(ContentFolder3D + "Island2/Island2");
            Island3 = Content.Load<Model>(ContentFolder3D + "Island3/Island3");
            


            ShipWorld = ShipScale * Matrix.CreateTranslation(ShipPosition);
            ShipWorld2 = ShipScale * Matrix.CreateTranslation(ShipPosition2);
            ShipWorld3 = ShipScale * Matrix.CreateTranslation(ShipPosition3);
            ShipWorld4 = ShipScale * Matrix.CreateTranslation(ShipPosition4);
            ShipWorld5 = ShipScale * Matrix.CreateTranslation(ShipPosition5);
            ShipWorld6 = ShipScale * Matrix.CreateTranslation(ShipPosition6);
            ShipWorld7 = ShipScale * Matrix.CreateTranslation(ShipPosition7);
            ShipWorld8 = ShipScale * Matrix.CreateTranslation(ShipPosition8);
            IslandWorld1 = IslandScale * Matrix.CreateTranslation(IslandPosition1);
            IslandWorld2 = IslandScale * Matrix.CreateTranslation(IslandPosition2);
            IslandWorld3 = IslandScale * Matrix.CreateTranslation(IslandPosition3);
            IslandWorld4 = IslandScale * Matrix.CreateTranslation(IslandPosition4);
            IslandWorld5 = IslandScale * Matrix.CreateTranslation(IslandPosition5);
            IslandWorld6 = IslandScale * Matrix.CreateTranslation(IslandPosition6);
            IslandWorld7 = IslandScale * Matrix.CreateTranslation(IslandPosition7);
            IslandWorld8 = IslandScale * Matrix.CreateTranslation(IslandPosition8);
            IslandWorld9 = IslandScale * Matrix.CreateTranslation(IslandPosition9);
            IslandWorld10 = IslandScale * Matrix.CreateTranslation(IslandPosition10);
            IslandWorld11 = IslandScale * Matrix.CreateTranslation(IslandPosition11);
            IslandWorld12 = IslandScale * Matrix.CreateTranslation(IslandPosition12);
            IslandWorld13 = IslandScale * Matrix.CreateTranslation(IslandPosition13);
            IslandWorld14 = IslandScale * Matrix.CreateTranslation(IslandPosition14);
            IslandWorld15 = IslandScale * Matrix.CreateTranslation(IslandPosition15);
            IslandWorld16 = IslandScale * Matrix.CreateTranslation(IslandPosition16);
        IslandWorld17 = IslandScale * Matrix.CreateTranslation(IslandPosition17);
        IslandWorld18 = IslandScale * Matrix.CreateTranslation(IslandPosition18);
            IslandWorld19 = IslandScale * Matrix.CreateTranslation(IslandPosition19);
            IslandWorld20 = IslandScale * Matrix.CreateTranslation(IslandPosition20); 


        TilingEffect = Content.Load<Effect>(ContentFolderEffects + "TextureTiling");
        TilingEffect.Parameters["Tiling"].SetValue(new Vector2(100f, 100f));
        SeaTexture = Content.Load<Texture2D>(ContentFolderTextures + "sea-texture");
        Quad = new QuadPrimitive(GraphicsDevice);

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
        var viewProjection = Camera.View * Camera.Projection;


        GraphicsDevice.Clear(Color.CornflowerBlue);
        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        GraphicsDevice.SetVertexBuffer(TerrainVertexBuffer);
        GraphicsDevice.Indices = TerrainIndexBuffer;

        // Dibujamos Piso
        TilingEffect.CurrentTechnique = TilingEffect.Techniques["BaseTiling"];
        //TilingEffect.Parameters["Tiling"].SetValue(new Vector2(10f, 10f));
        TilingEffect.Parameters["WorldViewProjection"].SetValue(FloorWorld * viewProjection);
        TilingEffect.Parameters["Texture"].SetValue(SeaTexture);
        Quad.Draw(TilingEffect);

        //Dibujamos modelos
        Ship.Draw(ShipWorld, Camera.View, Camera.Projection);
            Ship.Draw(ShipWorld2, Camera.View, Camera.Projection);
            Ship.Draw(ShipWorld3, Camera.View, Camera.Projection);
            Ship.Draw(ShipWorld4, Camera.View, Camera.Projection);
            Ship.Draw(ShipWorld5,Camera.View,Camera.Projection);
            Ship.Draw(ShipWorld6, Camera.View, Camera.Projection);
            Ship.Draw(ShipWorld7, Camera.View, Camera.Projection);
            Ship.Draw(ShipWorld8, Camera.View, Camera.Projection);
            Island1.Draw(IslandWorld1,Camera.View, Camera.Projection);
            Island2.Draw(IslandWorld2, Camera.View, Camera.Projection);
            Island3.Draw(IslandWorld3, Camera.View, Camera.Projection);
            Island2.Draw(IslandWorld4,Camera.View,Camera.Projection);
            Island1.Draw(IslandWorld5, Camera.View, Camera.Projection);
            Island2.Draw(IslandWorld6, Camera.View, Camera.Projection);
            Island1.Draw(IslandWorld7, Camera.View, Camera.Projection);
            Island3.Draw(IslandWorld8, Camera.View, Camera.Projection);
            Island3.Draw(IslandWorld9, Camera.View, Camera.Projection);
            Island2.Draw(IslandWorld10, Camera.View, Camera.Projection);
            Island1.Draw(IslandWorld11, Camera.View, Camera.Projection);
            Island1.Draw(IslandWorld12, Camera.View, Camera.Projection);
            Island1.Draw(IslandWorld13, Camera.View, Camera.Projection);
            Island1.Draw(IslandWorld14, Camera.View, Camera.Projection);
            Island3.Draw(IslandWorld15, Camera.View, Camera.Projection);
            Island1.Draw(IslandWorld16, Camera.View, Camera.Projection);
            Island3.Draw(IslandWorld17, Camera.View, Camera.Projection);
            Island2.Draw(IslandWorld18, Camera.View, Camera.Projection);
            Island1.Draw(IslandWorld19, Camera.View, Camera.Projection);
            Island3.Draw(IslandWorld20, Camera.View, Camera.Projection);
            




        // Render terrain.
        //Effect.View = Camera.View;
        //Effect.Projection = Camera.Projection;


        //foreach (var pass in Effect.CurrentTechnique.Passes)
        //{
        //    pass.Apply();
        //    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, PrimitiveCount);
        //}

        // Dibujamos primitivas
        DrawGeometry(Cilindro, CilindroPosition1, Camera.View, Camera.Projection);
            DrawGeometry(Cilindro2, CilindroPosition2, Camera.View, Camera.Projection);
            DrawGeometry(Cilindro3, CilindroPosition3, Camera.View, Camera.Projection);
            DrawGeometry(Cilindro4, CilindroPosition4, Camera.View, Camera.Projection);
            DrawGeometry(Cilindro5,CilindroPosition5,Camera.View,Camera.Projection);
            DrawGeometry(Cilindro6,CilindroPosition6,Camera.View,Camera.Projection);
            DrawGeometry(Cilindro7,CilindroPosition7,Camera.View,Camera.Projection);
            DrawGeometry(Cilindro8,CilindroPosition8,Camera.View,Camera.Projection);
            DrawGeometry(Sphere, SpherePosition, Camera.View, Camera.Projection);
            DrawGeometry(Sphere2, SpherePosition2, Camera.View, Camera.Projection);
            DrawGeometry(Sphere3,SpherePosition3,Camera.View,Camera.Projection);
            DrawGeometry(Sphere4,SpherePosition4,Camera.View,Camera.Projection);
            DrawGeometry(Sphere5, SpherePosition5, Camera.View, Camera.Projection);
            DrawGeometry(Sphere6,SpherePosition6,Camera.View,Camera.Projection);
            DrawGeometry(Sphere7,SpherePosition7,Camera.View,Camera.Projection);
            DrawGeometry(Sphere8, SpherePosition8, Camera.View, Camera.Projection);



        base.Draw(gameTime);
    }

    private void DrawGeometry(GeometricPrimitive geometry, Vector3 position, Matrix View, Matrix Projection)
    {
        var effect = geometry.Effect;

        effect.World = Matrix.Identity * Matrix.CreateTranslation(position);
        effect.View = View;
        effect.Projection = Projection;

        geometry.Draw(effect);
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
