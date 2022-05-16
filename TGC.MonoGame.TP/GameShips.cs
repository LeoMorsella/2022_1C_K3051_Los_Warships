using System;
using System.Collections.Generic;
using BepuPhysics.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TGC.MonoGame.Samples.Cameras;
using TGC.MonoGame.Samples.Geometries;
using TGC.MonoGame.Samples.Geometries.Textures;

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
        private Effect GenericEffect { get; set; }
        private Texture2D TerrainTexture { get; set; }
        private VertexBuffer TerrainVertexBuffer { get; set; }
        private IndexBuffer TerrainIndexBuffer { get; set; }
        private FreeCamera freeCamera { get; set; }

        private const float CameraFollowRadius = 250f;
        private const float CameraUpDistance = 80f;
        private TargetCamera targetCamera { get; set; }

        private Model Ship { get; set; }
        private Model Ship2 { get; set; }
        private Model Island1 { get; set; }
        private Model Island2 { get; set; }
        private Model Island3 { get; set; }

        // Matrices de Mundo
        private Matrix[] ShipWorldA { get; set; }
        private Matrix[] ShipWorldB { get; set; }
        private Matrix[] IslandWorld1 { get; set; }
        private Matrix[] IslandWorld2 { get; set; }
        private Matrix[] IslandWorld3 { get; set; }

        private Matrix PlayerWorld { get; set; }
        private Vector3 PlayerPosition { get; set; }
        private Matrix PlayerRotation { get; set; }

        // Matrices de Escala
        private Matrix ShipScaleA { get; set; }
        private Matrix ShipScaleB { get; set; }
        private Matrix IslandScale { get; set; }

        //Posiciones de Islands
        private Vector3[] IslandPosition1 { get; set; }
        private Vector3[] IslandPosition2 { get; set; }
        private Vector3[] IslandPosition3 { get; set; }

        // Prmitivas
        private CylinderPrimitive Palmera { get; set; }
        private Vector3[] PalmeraPosition { get; set; }

        private SpherePrimitive Piedra { get; set; }
        private Vector3[] PiedraPosition { get; set; }


        private QuadPrimitive Quad { get; set; }
        private Matrix FloorWorld { get; set; }
        private Texture2D SeaTexture { get; set; }
        private Effect TilingEffect { get; set; }

        // Listas de texturas
        private List<Texture2D> TexturesShipA { get; set; }
        private List<Texture2D> TexturesShipB { get; set; }
        private List<Texture2D> TexturesIsland1 { get; set; }
        private List<Texture2D> TexturesIsland2 { get; set; }
        private List<Texture2D> TexturesIsland3 { get; set; }

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

            //freeCamera = new FreeCamera(aspectRatio, new Vector3(0,300f,200f) , new Point(width/2,height/2));
            freeCamera = new FreeCamera(aspectRatio, new Vector3(1000f, 800f, 3000f), new Point(width / 2, height / 2));
            var nearPlane = 1f;
            var farPlane = 8000f;
            var fieldOfView = MathHelper.ToRadians(60f);
            freeCamera.BuildProjection(aspectRatio, nearPlane, farPlane, fieldOfView);
            freeCamera.MovementSpeed = 500f;

            var position = Vector3.One * 100f;
            var targetPosition = Vector3.Zero;
            targetCamera = new TargetCamera(aspectRatio, position, targetPosition, nearPlane, farPlane);

            ShipScaleA = Matrix.CreateScale(0.01f);
            ShipScaleB = Matrix.CreateScale(0.1f);
            IslandScale = Matrix.CreateScale(0.04f);

            PlayerPosition = new Vector3(1200f, 200f, -3500f);
            PlayerRotation = Matrix.CreateRotationY(-MathHelper.PiOver2);
            PlayerWorld = ShipScaleA * PlayerRotation * Matrix.CreateTranslation(PlayerPosition);

            ShipWorldA = new Matrix[]
            {
                ShipScaleA * Matrix.CreateTranslation(Vector3.UnitY * 200f),
                ShipScaleA * Matrix.CreateTranslation(new Vector3(0, 200f, -200f)),
                ShipScaleA * Matrix.CreateTranslation(new Vector3(200f, 200f, 200f)),
                ShipScaleA * Matrix.CreateTranslation(new Vector3(-400f, 200f, -200f)),
                ShipScaleA * Matrix.CreateTranslation(new Vector3(600f, 200f, 300f)),
                ShipScaleA * Matrix.CreateTranslation(new Vector3(300f, 200f, 1000f)),
                ShipScaleA * Matrix.CreateTranslation(new Vector3(400f, 200f, 1500f)),
                ShipScaleA * Matrix.CreateTranslation(new Vector3(-500f, 200f, 1200f)),
                ShipScaleA * Matrix.CreateTranslation(new Vector3(-300f, 200f, 500f)),
                ShipScaleA * Matrix.CreateTranslation(new Vector3(100f, 200f, 700f)),
            };

            var scaleRotShipB = ShipScaleB * Matrix.CreateRotationY(MathHelper.PiOver2);
            ShipWorldB = new Matrix[]
            {
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(2000f, 200f, 0f)),
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(2000f, 200f, -200f)),
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(2200f, 200f, 200f)),
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(1600f, 200f, -200f)),
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(2600f, 200f, 300f)),
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(2300f, 200f, 1000f)),
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(2400f, 200f, 1500f)),
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(1500f, 200f, 1200f)),
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(1700f, 200f, 500f)),
                scaleRotShipB * Matrix.CreateTranslation(new Vector3(2100f, 200f, 700f)),
            };

            IslandPosition1 = new Vector3[]
            {
                new Vector3(800f, 200f, 800f),
                new Vector3(-1100f, 200f, 400f),
                new Vector3(-1000f, 200f, -910f),
                new Vector3(700f, 195f, 3500f),
                new Vector3(-800f, 195f, 2900f),
                new Vector3(-800f, 185f, 2500f),
                new Vector3(-300f, 195f, -1800f),
                new Vector3(-2100f, 200f, -3000f),
                new Vector3(-1400f, 190f, -4100f),
            };

            IslandWorld1 = new Matrix[IslandPosition1.Length];
            for (int i = 0; i < IslandPosition1.Length; i++)
                IslandWorld1[i] = IslandScale * Matrix.CreateTranslation(IslandPosition1[i]);

            IslandPosition2 = new Vector3[]
            {
                new Vector3(-800f, 200f, 1000f),
                new Vector3(1200f, 200f, 300f),
                new Vector3(-1000f, 200f, -900f),
                new Vector3(500f, 200f, 3000f),
                new Vector3(-1000f, 200f, -3500f),
            };

            IslandWorld2 = new Matrix[IslandPosition2.Length];
            for (int i = 0; i < IslandPosition2.Length; i++)
                IslandWorld2[i] = IslandScale * Matrix.CreateTranslation(IslandPosition2[i]);

            IslandPosition3 = new Vector3[]
            {
                new Vector3(800f, 200f, -800f),
                new Vector3(-1000f, 200f, -920f),
                new Vector3(100f, 200f, 2000f),
                new Vector3(-1100f, 200f, -2500f),
                new Vector3(-2800f, 200f, -2200f),
                new Vector3(100f, 200f, -4500f),
            };

            IslandWorld3 = new Matrix[IslandPosition3.Length];
            for (int i = 0; i < IslandPosition3.Length; i++)
                IslandWorld3[i] = IslandScale * Matrix.CreateTranslation(IslandPosition3[i]);

            /* offset para set de palmeras y piedras para ubicar en funcion a un punto */
            var islandPositionSet1 = IslandPosition1[0];
            var islandPositionSet2 = IslandPosition1[3];
            var islandPositionSet3 = IslandPosition1[6];
            var islandPositionSet4 = IslandPosition3[4];
            var islandPositionSet5 = IslandPosition1[4];
            var offsetPalmera1 = new Vector3(0, 0, 100f);
            var offsetPalmera2 = new Vector3(100f, 0, 50f);
            var offsetPalmera3 = new Vector3(-50f, 0, -50f);
            var offsetPalmera4 = new Vector3(30f, 0, -10f);
            var offsetPiedra1 = new Vector3(0f, 10f, 20f);
            var offsetPiedra2 = new Vector3(50f, 10f, 0f);
            var offsetPiedra3 = new Vector3(0f, 10f, -50f);
            var offsetPiedra4 = new Vector3(-20f, 10f, 0f);
            var offsetPiedra5 = new Vector3(-50f, 10f, 50f);
            var offsetPiedra6 = new Vector3(50f, 10f, 50f);

            Palmera = new CylinderPrimitive(GraphicsDevice, 100, 4, 32);

            PalmeraPosition = new Vector3[]
            {
                new Vector3(-750f, 200f, 800f),
                new Vector3(-600f, 200f, 900f),
                new Vector3(-900f, 200f, 700f),
                new Vector3(-600f, 200f, 700f),
                new Vector3(1250f, 200f, 350f),
                new Vector3(-1400f, 200f, -4100f),
                new Vector3(-1300f, 200f, -3500f),
                new Vector3(-2100f, 200f, -3000f),
                islandPositionSet1 + offsetPalmera1,
                islandPositionSet1 + offsetPalmera2,
                islandPositionSet1 + offsetPalmera3,
                islandPositionSet1 + offsetPalmera4,
                islandPositionSet2 + offsetPalmera1,
                islandPositionSet2 + offsetPalmera2,
                islandPositionSet2 + offsetPalmera3,
                islandPositionSet2 + offsetPalmera4,
                islandPositionSet3 + offsetPalmera1,
                islandPositionSet3 + offsetPalmera2,
                islandPositionSet3 + offsetPalmera3,
                islandPositionSet3 + offsetPalmera4,
                islandPositionSet4 + offsetPalmera1,
                islandPositionSet4 + offsetPalmera2,
                islandPositionSet4 + offsetPalmera3,
                islandPositionSet4 + offsetPalmera4,
                islandPositionSet5 + offsetPalmera1,
                islandPositionSet5 + offsetPalmera2,
                islandPositionSet5 + offsetPalmera3,
                islandPositionSet5 + offsetPalmera4,
            };

            Piedra = new SpherePrimitive(GraphicsDevice, 10, 32);

            PiedraPosition = new Vector3[]
            {
                new Vector3(-700f, 200f, 850f),
                new Vector3(-700f, 200f, 750f),
                new Vector3(1225f, 200f, 325f),
                new Vector3(1275f, 200f, 310f),
                new Vector3(100f, 200f, -4500f),
                new Vector3(-720f, 200f, -3600f),
                new Vector3(-710f, 200f, -3620f),
                new Vector3(-720f, 200f, -3640f),
                islandPositionSet1 + offsetPiedra1,
                islandPositionSet1 + offsetPiedra2,
                islandPositionSet1 + offsetPiedra3,
                islandPositionSet1 + offsetPiedra4,
                islandPositionSet1 + offsetPiedra5,
                islandPositionSet1 + offsetPiedra6,
                islandPositionSet2 + offsetPiedra1,
                islandPositionSet2 + offsetPiedra2,
                islandPositionSet2 + offsetPiedra3,
                islandPositionSet2 + offsetPiedra4,
                islandPositionSet2 + offsetPiedra5,
                islandPositionSet2 + offsetPiedra6,
                islandPositionSet3 + offsetPiedra1,
                islandPositionSet3 + offsetPiedra2,
                islandPositionSet3 + offsetPiedra3,
                islandPositionSet3 + offsetPiedra4,
                islandPositionSet3 + offsetPiedra5,
                islandPositionSet3 + offsetPiedra6,
                islandPositionSet4 + offsetPiedra1,
                islandPositionSet4 + offsetPiedra2,
                islandPositionSet4 + offsetPiedra3,
                islandPositionSet4 + offsetPiedra4,
                islandPositionSet4 + offsetPiedra5,
                islandPositionSet4 + offsetPiedra6,
                islandPositionSet5 + offsetPiedra1,
                islandPositionSet5 + offsetPiedra2,
                islandPositionSet5 + offsetPiedra3,
                islandPositionSet5 + offsetPiedra4,
                islandPositionSet5 + offsetPiedra5,
                islandPositionSet5 + offsetPiedra6,
            };

            FloorWorld = Matrix.CreateScale(10000f, 0.1f, 10000f) * Matrix.CreateTranslation(0, 185f, 0);

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
            Ship2 = Content.Load<Model>(ContentFolder3D + "ShipB/Ship");

            Island1 = Content.Load<Model>(ContentFolder3D + "Island1/Island1");
            Island2 = Content.Load<Model>(ContentFolder3D + "Island2/Island2");
            Island3 = Content.Load<Model>(ContentFolder3D + "Island3/Island3");

            
            TilingEffect = Content.Load<Effect>(ContentFolderEffects + "TextureTiling");
            GenericEffect = Content.Load<Effect>(ContentFolderEffects + "BasicShader");

            TexturesShipA = new List<Texture2D>();
            TexturesShipB = new List<Texture2D>();
            TexturesIsland1 = new List<Texture2D>();
            TexturesIsland2 = new List<Texture2D>();
            TexturesIsland3 = new List<Texture2D>();
            // Asigno el efecto que cargue a cada parte del mesh.
            // Un modelo puede tener mas de 1 mesh internamente.
            foreach (var mesh in Ship.Meshes)
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts)
                {
                    var basicEffect = ((BasicEffect)meshPart.Effect);
                    TexturesShipA.Add(basicEffect.Texture);
                    meshPart.Effect = GenericEffect;
                }

            // Asigno el efecto que cargue a cada parte del mesh.
            // Un modelo puede tener mas de 1 mesh internamente.
            foreach (var mesh in Ship2.Meshes)
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts)
                {
                    var basicEffect = ((BasicEffect)meshPart.Effect);
                    if (basicEffect.Texture != null)
                    {
                        TexturesShipB.Add(basicEffect.Texture);
                    }
                    meshPart.Effect = GenericEffect;
                }

            // Asigno el efecto que cargue a cada parte del mesh.
            // Un modelo puede tener mas de 1 mesh internamente.
            foreach (var mesh in Island1.Meshes)
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts)
                {
                    var basicEffect = ((BasicEffect)meshPart.Effect);
                    if (basicEffect.Texture != null)
                    {
                        TexturesIsland1.Add(basicEffect.Texture);
                    }
                    meshPart.Effect = GenericEffect;
                }

            // Asigno el efecto que cargue a cada parte del mesh.
            // Un modelo puede tener mas de 1 mesh internamente.
            foreach (var mesh in Island2.Meshes)
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts)
                {
                    var basicEffect = ((BasicEffect)meshPart.Effect);
                    if (basicEffect.Texture != null)
                    {
                        TexturesIsland2.Add(basicEffect.Texture);
                    }
                    meshPart.Effect = GenericEffect;
                }

            // Asigno el efecto que cargue a cada parte del mesh.
            // Un modelo puede tener mas de 1 mesh internamente.
            foreach (var mesh in Island3.Meshes)
                // Un mesh puede tener mas de 1 mesh part (cada 1 puede tener su propio efecto).
                foreach (var meshPart in mesh.MeshParts)
                {
                    var basicEffect = ((BasicEffect)meshPart.Effect);
                    if (basicEffect.Texture != null)
                    {
                        TexturesIsland3.Add(basicEffect.Texture);
                    }
                    meshPart.Effect = GenericEffect;
                }

            TilingEffect.Parameters["Tiling"].SetValue(new Vector2(80f, 80f));
            SeaTexture = Content.Load<Texture2D>(ContentFolderTextures + "sea-texture");
            Quad = new QuadPrimitive(GraphicsDevice);

            UpdateCamera();

            base.LoadContent();
        }

        /// <summary>
        ///     Updates the internal values of the Camera
        /// </summary>
        private void UpdateCamera()
        {
            // Create a normalized vector that points to the back of the Robot
            var shipBackDirection = Vector3.Transform(Vector3.Left, PlayerRotation);
            // Then scale the vector by a radius, to set an horizontal distance between the Camera and the Robot
            var orbitalPosition = shipBackDirection * CameraFollowRadius;


            // We will move the Camera in the Y axis by a given distance, relative to the Robot
            var upDistance = Vector3.Up * CameraUpDistance;

            // Calculate the new Camera Position by using the Robot Position, then adding the vector orbitalPosition that sends 
            // the camera further in the back of the Robot, and then we move it up by a given distance
            targetCamera.Position = PlayerPosition + orbitalPosition + upDistance;

            // Set the Target as the Robot, the Camera needs to be always pointing to it
            targetCamera.TargetPosition = PlayerPosition;

            // Build the View matrix from the Position and TargetPosition
            targetCamera.BuildView();
        }

        /// <inheritdoc />
        protected override void Update(GameTime gameTime)
        {
            //freeCamera.Update(gameTime);

            // Update the Camera accordingly, as it follows the Robot
            UpdateCamera();


            base.Update(gameTime);
        }

        /// <inheritdoc />
        protected override void Draw(GameTime gameTime)
        {
            //var activeCamera = freeCamera;
            var activeCamera = targetCamera;
            
            var viewProjection = activeCamera.View * activeCamera.Projection;


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

            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            GenericEffect.Parameters["View"].SetValue(activeCamera.View);
            GenericEffect.Parameters["Projection"].SetValue(activeCamera.Projection);
            //GenericEffect.Parameters["DiffuseColor"].SetValue(Color.DarkBlue.ToVector3());

            var index = 0;
            //Aplico el efecto basico para el modelo ShipA
            foreach (var mesh in Ship.Meshes)
            {
                foreach (var meshPart in mesh.MeshParts)
                {
                    if (TexturesShipA[index] != null)
                    {
                        GenericEffect.Parameters["ModelTexture"].SetValue(TexturesShipA[index]);
                    }

                    foreach (var shipWorld in ShipWorldA)
                    {
                        var world = mesh.ParentBone.Transform * shipWorld;
                        GenericEffect.Parameters["World"].SetValue(world);
                        mesh.Draw();
                    }

                    index++;
                }

                var pWorld = mesh.ParentBone.Transform * PlayerWorld;
                GenericEffect.Parameters["World"].SetValue(pWorld);
                mesh.Draw();
            }
            index = 0;
            //Aplico el efecto basico para el modelo ShipB
            foreach (var mesh in Ship2.Meshes)
            {
                if (TexturesShipB[index] != null)
                {
                    GenericEffect.Parameters["ModelTexture"].SetValue(TexturesShipB[index]);
                }

                foreach (var shipWorld in ShipWorldB) 
                {
                    var world = mesh.ParentBone.Transform * shipWorld;
                    GenericEffect.Parameters["World"].SetValue(world);
                    mesh.Draw();
                }
                
                index++;
            }

            index = 0;
            //Aplico el efecto basico para el modelo Island 1
            foreach (var mesh in Island1.Meshes)
            {
                if (TexturesIsland1[index] != null)
                {
                    GenericEffect.Parameters["ModelTexture"].SetValue(TexturesIsland1[index]);
                }

                foreach (var islandWorld in IslandWorld1) 
                {
                    var world = mesh.ParentBone.Transform * islandWorld;
                    GenericEffect.Parameters["World"].SetValue(world);
                    mesh.Draw();
                }

                index++;
            }
            index = 0;
            
            //Aplico el efecto basico para el modelo Island2 
            foreach (var mesh in Island2.Meshes)
            {
                if (TexturesIsland2[index] != null)
                {
                    GenericEffect.Parameters["ModelTexture"].SetValue(TexturesIsland2[index]);
                }
                
                foreach (var islandWorld in IslandWorld2) 
                {
                    var world = mesh.ParentBone.Transform * islandWorld;
                    GenericEffect.Parameters["World"].SetValue(world);
                    mesh.Draw();
                }

                index++;
            }
            index = 0;

            //Aplico el efecto basico para el modelo Island3 
            foreach (var mesh in Island3.Meshes)
            {
                if (TexturesIsland3[index] != null)
                {
                    GenericEffect.Parameters["ModelTexture"].SetValue(TexturesIsland3[index]);
                }

                foreach (var islandWorld in IslandWorld3)
                {
                    var world = mesh.ParentBone.Transform * islandWorld;
                    GenericEffect.Parameters["World"].SetValue(world);
                    mesh.Draw();
                }

                index++;
            }

            // Dibujamos primitivas
            foreach (var palmPos in PalmeraPosition)
                DrawGeometry(Palmera, palmPos, activeCamera.View, activeCamera.Projection);

            foreach (var piePos in PiedraPosition)
                DrawGeometry(Piedra, piePos, activeCamera.View, activeCamera.Projection);

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
                    textureCoordinates = new Vector2((float)x / heightMapWidth, (float)z / heightMapLength);
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

                    var a = (ushort)(x + bottom);
                    var b = (ushort)(right + bottom);
                    var c = (ushort)(right + top);
                    var d = (ushort)(x + top);

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
