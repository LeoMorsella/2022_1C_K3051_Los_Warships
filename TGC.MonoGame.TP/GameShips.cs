using System;
using System.Collections.Generic;
using BepuPhysics.Collidables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private FreeCamera Camera { get; set; }

        private Model Ship { get; set; }
        private Model Ship2 { get; set; } // Este modelo carga mal
        private Model Island1 { get; set; }
        private Model Island2 { get; set; }
        private Model Island3 { get; set; }

        // Matrices de Mundo
        private Matrix ShipWorld { get; set; } = Matrix.Identity;
        private Matrix ShipWorld2 { get; set; } = Matrix.Identity;
        private Matrix ShipWorld3 { get; set; } = Matrix.Identity;
        private Matrix ShipWorld4 { get; set; } = Matrix.Identity;
        private Matrix ShipWorld5 { get; set; } = Matrix.Identity;
        private Matrix ShipWorld6 { get; set; } = Matrix.Identity;
        private Matrix ShipWorld7 { get; set; } = Matrix.Identity;
        private Matrix ShipWorld8 { get; set; } = Matrix.Identity;
        private Matrix ShipWorld9 { get; set; } = Matrix.Identity;
        private Matrix ShipWorld10 { get; set; } = Matrix.Identity;

        private Matrix Ship2World { get; set; } = Matrix.Identity;
        private Matrix Ship2World2 { get; set; } = Matrix.Identity;
        private Matrix Ship2World3 { get; set; } = Matrix.Identity;
        private Matrix Ship2World4 { get; set; } = Matrix.Identity;
        private Matrix Ship2World5 { get; set; } = Matrix.Identity;
        private Matrix Ship2World6 { get; set; } = Matrix.Identity;
        private Matrix Ship2World7 { get; set; } = Matrix.Identity;
        private Matrix Ship2World8 { get; set; } = Matrix.Identity;
        private Matrix Ship2World9 { get; set; } = Matrix.Identity;
        private Matrix Ship2World10 { get; set; } = Matrix.Identity;

        private Matrix IslandWorld1 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld2 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld3 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld4 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld5 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld6 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld7 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld8 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld9 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld10 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld11 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld12 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld13 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld14 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld15 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld16 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld17 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld18 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld19 { get; set; } = Matrix.Identity;
        private Matrix IslandWorld20 { get; set; } = Matrix.Identity;


        // Matrices de Escala
        private Matrix ShipScale { get; set; }
        private Matrix Ship2Scale { get; set; }
        private Matrix IslandScale { get; set; }

        // Posiciones de Ships
        private Vector3 ShipPosition { get; set; }
        private Vector3 ShipPosition2 { get; set; }
        private Vector3 ShipPosition3 { get; set; }
        private Vector3 ShipPosition4 { get; set; }
        private Vector3 ShipPosition5 { get; set; }
        private Vector3 ShipPosition6 { get; set; }
        private Vector3 ShipPosition7 { get; set; }
        private Vector3 ShipPosition8 { get; set; }
        private Vector3 ShipPosition9 { get; set; }
        private Vector3 ShipPosition10 { get; set; }

        private Vector3 Ship2Position { get; set; }
        private Vector3 Ship2Position2 { get; set; }
        private Vector3 Ship2Position3 { get; set; }
        private Vector3 Ship2Position4 { get; set; }
        private Vector3 Ship2Position5 { get; set; }
        private Vector3 Ship2Position6 { get; set; }
        private Vector3 Ship2Position7 { get; set; }
        private Vector3 Ship2Position8 { get; set; }
        private Vector3 Ship2Position9 { get; set; }
        private Vector3 Ship2Position10 { get; set; }

        //Posiciones de Islands
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
        private CylinderPrimitive Palmera { get; set; }
        private Vector3 PalmeraPosition1 { get; set; }
        private Vector3 PalmeraPosition2 { get; set; }
        private Vector3 PalmeraPosition3 { get; set; }
        private Vector3 PalmeraPosition4 { get; set; }
        private Vector3 PalmeraPosition5 { get; set; }
        private Vector3 PalmeraPosition6 { get; set; }
        private Vector3 PalmeraPosition7 { get; set; }
        private Vector3 PalmeraPosition8 { get; set; }
        private Vector3 PalmeraPosition9 { get; set; }
        private Vector3 PalmeraPosition10 { get; set; }
        private Vector3 PalmeraPosition11 { get; set; }
        private Vector3 PalmeraPosition12 { get; set; }
        private Vector3 PalmeraPosition13 { get; set; }
        private Vector3 PalmeraPosition14 { get; set; }
        private Vector3 PalmeraPosition15 { get; set; }
        private Vector3 PalmeraPosition16 { get; set; }
        private Vector3 PalmeraPosition17 { get; set; }
        private Vector3 PalmeraPosition18 { get; set; }
        private Vector3 PalmeraPosition19 { get; set; }
        private Vector3 PalmeraPosition20 { get; set; }
        private Vector3 PalmeraPosition21 { get; set; }
        private Vector3 PalmeraPosition22 { get; set; }
        private Vector3 PalmeraPosition23 { get; set; }
        private Vector3 PalmeraPosition24 { get; set; }
        private Vector3 PalmeraPosition25 { get; set; }
        private Vector3 PalmeraPosition26 { get; set; }
        private Vector3 PalmeraPosition27 { get; set; }
        private Vector3 PalmeraPosition28 { get; set; }

        private SpherePrimitive Piedra { get; set; }
        private Vector3 PiedraPosition1 { get; set; }
        private Vector3 PiedraPosition2 { get; set; }
        private Vector3 PiedraPosition3 { get; set; }
        private Vector3 PiedraPosition4 { get; set; }
        private Vector3 PiedraPosition5 { get; set; }
        private Vector3 PiedraPosition6 { get; set; }
        private Vector3 PiedraPosition7 { get; set; }
        private Vector3 PiedraPosition8 { get; set; }
        private Vector3 PiedraPosition9 { get; set; }
        private Vector3 PiedraPosition10 { get; set; }
        private Vector3 PiedraPosition11 { get; set; }
        private Vector3 PiedraPosition12 { get; set; }
        private Vector3 PiedraPosition13 { get; set; }
        private Vector3 PiedraPosition14 { get; set; }
        private Vector3 PiedraPosition15 { get; set; }
        private Vector3 PiedraPosition16 { get; set; }
        private Vector3 PiedraPosition17 { get; set; }
        private Vector3 PiedraPosition18 { get; set; }
        private Vector3 PiedraPosition19 { get; set; }
        private Vector3 PiedraPosition20 { get; set; }
        private Vector3 PiedraPosition21 { get; set; }
        private Vector3 PiedraPosition22 { get; set; }
        private Vector3 PiedraPosition23 { get; set; }
        private Vector3 PiedraPosition24 { get; set; }
        private Vector3 PiedraPosition25 { get; set; }
        private Vector3 PiedraPosition26 { get; set; }
        private Vector3 PiedraPosition27 { get; set; }
        private Vector3 PiedraPosition28 { get; set; }
        private Vector3 PiedraPosition29 { get; set; }
        private Vector3 PiedraPosition30 { get; set; }
        private Vector3 PiedraPosition31 { get; set; }
        private Vector3 PiedraPosition32 { get; set; }
        private Vector3 PiedraPosition33 { get; set; }
        private Vector3 PiedraPosition34 { get; set; }
        private Vector3 PiedraPosition35 { get; set; }
        private Vector3 PiedraPosition36 { get; set; }
        private Vector3 PiedraPosition37 { get; set; }
        private Vector3 PiedraPosition38 { get; set; }


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

            //Camera = new FreeCamera(aspectRatio, new Vector3(0,300f,200f) , new Point(width/2,height/2));
            Camera = new FreeCamera(aspectRatio, new Vector3(1000f, 800f, 3000f), new Point(width / 2, height / 2));
            var nearPlane = 1f;
            var farPlane = 8000f;
            var fieldOfView = MathHelper.ToRadians(60f);
            Camera.BuildProjection(aspectRatio, nearPlane, farPlane, fieldOfView);
            Camera.MovementSpeed = 500f;

            ShipScale = Matrix.CreateScale(0.01f);
            Ship2Scale = Matrix.CreateScale(0.1f);
            IslandScale = Matrix.CreateScale(0.04f);

            ShipPosition = Vector3.UnitY * 200f;
            ShipPosition2 = new Vector3(0, 200f, -200f);
            ShipPosition3 = new Vector3(200f, 200f, 200f);
            ShipPosition4 = new Vector3(-400f, 200f, -200f);
            ShipPosition5 = new Vector3(600f, 200f, 300f);
            ShipPosition6 = new Vector3(300f, 200f, 1000f);
            ShipPosition7 = new Vector3(400f, 200f, 1500f);
            ShipPosition8 = new Vector3(-500f, 200f, 1200f);
            ShipPosition9 = new Vector3(-300f, 200f, 500f);
            ShipPosition10 = new Vector3(100f, 200f, 700f);
            Ship2Position = new Vector3(2000f, 200f, 0f);
            Ship2Position2 = new Vector3(2000f, 200f, -200f);
            Ship2Position3 = new Vector3(2200f, 200f, 200f);
            Ship2Position4 = new Vector3(1600f, 200f, -200f);
            Ship2Position5 = new Vector3(2600f, 200f, 300f);
            Ship2Position6 = new Vector3(2300f, 200f, 1000f);
            Ship2Position7 = new Vector3(2400f, 200f, 1500f);
            Ship2Position8 = new Vector3(1500f, 200f, 1200f);
            Ship2Position9 = new Vector3(1700f, 200f, 500f);
            Ship2Position10 = new Vector3(2100f, 200f, 700f);
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
            IslandPosition15 = new Vector3(-1100f, 200f, -2500f);
            IslandPosition16 = new Vector3(-2100f, 200f, -3000f);
            IslandPosition17 = new Vector3(-2800f, 200f, -2200f);
            IslandPosition18 = new Vector3(-1000f, 200f, -3500f);
            IslandPosition19 = new Vector3(-1400f, 190f, -4100f);
            IslandPosition20 = new Vector3(100f, 200f, -4500f);

            /* offset para set de palmeras y piedras para ubicar en funcion a un punto */
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
            PalmeraPosition1 = new Vector3(-750f, 200f, 800f);
            PalmeraPosition2 = new Vector3(-600f, 200f, 900f);
            PalmeraPosition3 = new Vector3(-900f, 200f, 700f);
            PalmeraPosition4 = new Vector3(-600f, 200f, 700f);
            PalmeraPosition5 = new Vector3(1250f, 200f, 350f);
            PalmeraPosition6 = new Vector3(-1400f, 200f, -4100f);
            PalmeraPosition7 = new Vector3(-1300f, 200f, -3500f);
            PalmeraPosition8 = new Vector3(-2100f, 200f, -3000f);

            PalmeraPosition9 = IslandPosition1 + offsetPalmera1;
            PalmeraPosition10 = IslandPosition1 + offsetPalmera2;
            PalmeraPosition11 = IslandPosition1 + offsetPalmera3;
            PalmeraPosition12 = IslandPosition1 + offsetPalmera4;
            PalmeraPosition13 = IslandPosition11 + offsetPalmera1;
            PalmeraPosition14 = IslandPosition11 + offsetPalmera2;
            PalmeraPosition15 = IslandPosition11 + offsetPalmera3;
            PalmeraPosition16 = IslandPosition11 + offsetPalmera4;
            PalmeraPosition17 = IslandPosition14 + offsetPalmera1;
            PalmeraPosition18 = IslandPosition14 + offsetPalmera2;
            PalmeraPosition19 = IslandPosition14 + offsetPalmera3;
            PalmeraPosition20 = IslandPosition14 + offsetPalmera4;
            PalmeraPosition21 = IslandPosition17 + offsetPalmera1;
            PalmeraPosition22 = IslandPosition17 + offsetPalmera2;
            PalmeraPosition23 = IslandPosition17 + offsetPalmera3;
            PalmeraPosition24 = IslandPosition17 + offsetPalmera4;
            PalmeraPosition25 = IslandPosition12 + offsetPalmera1;
            PalmeraPosition26 = IslandPosition12 + offsetPalmera2;
            PalmeraPosition27 = IslandPosition12 + offsetPalmera3;
            PalmeraPosition28 = IslandPosition12 + offsetPalmera4;


            Piedra = new SpherePrimitive(GraphicsDevice, 10, 32);
            PiedraPosition1 = new Vector3(-700f, 200f, 850f);
            PiedraPosition2 = new Vector3(-700f, 200f, 750f);
            PiedraPosition3 = new Vector3(1225f, 200f, 325f);
            PiedraPosition4 = new Vector3(1275f, 200f, 310f);
            PiedraPosition5 = new Vector3(100f, 200f, -4500f);
            PiedraPosition6 = new Vector3(-720f, 200f, -3600f);
            PiedraPosition7 = new Vector3(-710f, 200f, -3620f);
            PiedraPosition8 = new Vector3(-720f, 200f, -3640f);

            PiedraPosition9 = IslandPosition1 + offsetPiedra1;
            PiedraPosition10 = IslandPosition1 + offsetPiedra2;
            PiedraPosition11 = IslandPosition1 + offsetPiedra3;
            PiedraPosition12 = IslandPosition1 + offsetPiedra4;
            PiedraPosition13 = IslandPosition1 + offsetPiedra5;
            PiedraPosition14 = IslandPosition1 + offsetPiedra6;
            PiedraPosition15 = IslandPosition11 + offsetPiedra1;
            PiedraPosition16 = IslandPosition11 + offsetPiedra2;
            PiedraPosition17 = IslandPosition11 + offsetPiedra3;
            PiedraPosition18 = IslandPosition11 + offsetPiedra4;
            PiedraPosition19 = IslandPosition11 + offsetPiedra5;
            PiedraPosition20 = IslandPosition11 + offsetPiedra6;
            PiedraPosition21 = IslandPosition14 + offsetPiedra1;
            PiedraPosition22 = IslandPosition14 + offsetPiedra2;
            PiedraPosition23 = IslandPosition14 + offsetPiedra3;
            PiedraPosition24 = IslandPosition14 + offsetPiedra4;
            PiedraPosition25 = IslandPosition14 + offsetPiedra5;
            PiedraPosition26 = IslandPosition14 + offsetPiedra6;
            PiedraPosition27 = IslandPosition17 + offsetPiedra1;
            PiedraPosition28 = IslandPosition17 + offsetPiedra2;
            PiedraPosition29 = IslandPosition17 + offsetPiedra3;
            PiedraPosition30 = IslandPosition17 + offsetPiedra4;
            PiedraPosition31 = IslandPosition17 + offsetPiedra5;
            PiedraPosition32 = IslandPosition17 + offsetPiedra6;
            PiedraPosition33 = IslandPosition12 + offsetPiedra1;
            PiedraPosition34 = IslandPosition12 + offsetPiedra2;
            PiedraPosition35 = IslandPosition12 + offsetPiedra3;
            PiedraPosition36 = IslandPosition12 + offsetPiedra4;
            PiedraPosition37 = IslandPosition12 + offsetPiedra5;
            PiedraPosition38 = IslandPosition12 + offsetPiedra6;

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

            base.LoadContent();
        }
        private float velocidadShip { get; set; } = 0f;
        /// <inheritdoc />
        protected override void Update(GameTime gameTime)
        {
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            bool press = false;
            // Caputo el estado del teclado
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
            {
                velocidadShip += elapsedTime;
                press = true;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                velocidadShip -= elapsedTime;
                press = true;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                ShipWorld *= Matrix.CreateRotationY(-0.05f);
            }
            //ShipWorld *= Matrix.CreateRotationY(-0.05f) * Matrix.CreateTranslation(ShipPosition);
            ShipWorld *= Matrix.CreateRotationX(-0.05f);
            if (keyboardState.IsKeyDown(Keys.D))
            {
                ShipWorld *= Matrix.CreateRotationY(-0.05f);
            }

            if (velocidadShip!=0 && !press)
            {
                if (velocidadShip > 0 && velocidadShip < 1)
                {
                    velocidadShip = 0f;

                }
                else if (velocidadShip < 0)
                {
                    velocidadShip += elapsedTime;
                }
                else if (velocidadShip>0)
                {
                    velocidadShip -= elapsedTime;
                }
            }

            AcelerarShip(Vector3.UnitX * velocidadShip);

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

            // Para dibujar le modelo necesitamos pasarle informacion que el efecto esta esperando.
            GenericEffect.Parameters["View"].SetValue(Camera.View);
            GenericEffect.Parameters["Projection"].SetValue(Camera.Projection);
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

                    ShipWorld = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld);
                    mesh.Draw();
                    ShipWorld2 = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition2);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld2);
                    mesh.Draw();
                    ShipWorld3 = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition3);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld3);
                    mesh.Draw();
                    ShipWorld4 = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition4);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld4);
                    mesh.Draw();
                    ShipWorld5 = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition5);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld5);
                    mesh.Draw();
                    ShipWorld6 = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition6);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld6);
                    mesh.Draw();
                    ShipWorld7 = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition7);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld7);
                    mesh.Draw();
                    ShipWorld8 = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition8);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld8);
                    mesh.Draw();
                    ShipWorld9 = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition9);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld9);
                    mesh.Draw();
                    ShipWorld10 = mesh.ParentBone.Transform * ShipScale * Matrix.CreateTranslation(ShipPosition10);
                    GenericEffect.Parameters["World"].SetValue(ShipWorld10);
                    mesh.Draw();
                    index++;
                }
            }
            index = 0;
            //Aplico el efecto basico para el modelo ShipB
            foreach (var mesh in Ship2.Meshes)
            {
                if (TexturesShipB[index] != null)
                {
                    GenericEffect.Parameters["ModelTexture"].SetValue(TexturesShipB[index]);
                }
                Ship2World = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position);
                GenericEffect.Parameters["World"].SetValue(Ship2World);
                mesh.Draw();
                Ship2World2 = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position2);
                GenericEffect.Parameters["World"].SetValue(Ship2World2);
                mesh.Draw();
                Ship2World3 = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position3);
                GenericEffect.Parameters["World"].SetValue(Ship2World3);
                mesh.Draw();
                Ship2World4 = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position4);
                GenericEffect.Parameters["World"].SetValue(Ship2World4);
                mesh.Draw();
                Ship2World5 = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position5);
                GenericEffect.Parameters["World"].SetValue(Ship2World5);
                mesh.Draw();
                Ship2World6 = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position6);
                GenericEffect.Parameters["World"].SetValue(Ship2World6);
                mesh.Draw();
                Ship2World7 = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position7);
                GenericEffect.Parameters["World"].SetValue(Ship2World7);
                mesh.Draw();
                Ship2World8 = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position8);
                GenericEffect.Parameters["World"].SetValue(Ship2World8);
                mesh.Draw();
                Ship2World9 = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position9);
                GenericEffect.Parameters["World"].SetValue(Ship2World9);
                mesh.Draw();
                Ship2World10 = mesh.ParentBone.Transform * Ship2Scale * Matrix.CreateRotationY(MathHelper.PiOver2) * Matrix.CreateTranslation(Ship2Position10);
                GenericEffect.Parameters["World"].SetValue(Ship2World10);
                mesh.Draw();
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
                IslandWorld1 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition1);
                GenericEffect.Parameters["World"].SetValue(IslandWorld1);
                mesh.Draw();
                IslandWorld5 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition5);
                GenericEffect.Parameters["World"].SetValue(IslandWorld5);
                mesh.Draw();
                IslandWorld7 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition7);
                GenericEffect.Parameters["World"].SetValue(IslandWorld7);
                mesh.Draw();
                IslandWorld11 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition11);
                GenericEffect.Parameters["World"].SetValue(IslandWorld11);
                mesh.Draw();
                IslandWorld12 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition12);
                GenericEffect.Parameters["World"].SetValue(IslandWorld12);
                mesh.Draw();
                IslandWorld13 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition13);
                GenericEffect.Parameters["World"].SetValue(IslandWorld13);
                mesh.Draw();
                IslandWorld14 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition14);
                GenericEffect.Parameters["World"].SetValue(IslandWorld14);
                mesh.Draw();
                IslandWorld16 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition16);
                GenericEffect.Parameters["World"].SetValue(IslandWorld16);
                mesh.Draw();
                IslandWorld19 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition19);
                GenericEffect.Parameters["World"].SetValue(IslandWorld19);
                mesh.Draw();
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
                IslandWorld2 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition2);
                GenericEffect.Parameters["World"].SetValue(IslandWorld2);
                mesh.Draw();
                IslandWorld4 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition4);
                GenericEffect.Parameters["World"].SetValue(IslandWorld4);
                mesh.Draw();
                IslandWorld6 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition6);
                GenericEffect.Parameters["World"].SetValue(IslandWorld6);
                mesh.Draw();
                IslandWorld10 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition10);
                GenericEffect.Parameters["World"].SetValue(IslandWorld10);
                mesh.Draw();
                IslandWorld18 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition18);
                GenericEffect.Parameters["World"].SetValue(IslandWorld18);
                mesh.Draw();
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
                IslandWorld3 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition3);
                GenericEffect.Parameters["World"].SetValue(IslandWorld3);
                mesh.Draw();
                IslandWorld8 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition8);
                GenericEffect.Parameters["World"].SetValue(IslandWorld8);
                mesh.Draw();
                IslandWorld9 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition9);
                GenericEffect.Parameters["World"].SetValue(IslandWorld9);
                mesh.Draw();
                IslandWorld15 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition15);
                GenericEffect.Parameters["World"].SetValue(IslandWorld15);
                mesh.Draw();
                IslandWorld17 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition17);
                GenericEffect.Parameters["World"].SetValue(IslandWorld17);
                mesh.Draw();
                IslandWorld20 = mesh.ParentBone.Transform * IslandScale * Matrix.CreateTranslation(IslandPosition20);
                GenericEffect.Parameters["World"].SetValue(IslandWorld20);
                mesh.Draw();
                index++;
            }

            // Dibujamos primitivas
            DrawGeometry(Palmera, PalmeraPosition1, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition2, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition3, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition4, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition5, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition6, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition7, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition8, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition9, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition10, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition11, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition12, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition13, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition14, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition15, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition16, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition17, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition18, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition19, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition20, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition21, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition22, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition23, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition24, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition25, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition26, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition27, Camera.View, Camera.Projection);
            DrawGeometry(Palmera, PalmeraPosition28, Camera.View, Camera.Projection);

            DrawGeometry(Piedra, PiedraPosition1, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition2, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition3, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition4, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition5, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition6, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition7, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition8, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition9, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition10, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition11, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition12, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition13, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition14, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition15, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition16, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition17, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition18, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition19, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition20, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition21, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition22, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition23, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition24, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition25, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition26, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition27, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition28, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition29, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition30, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition31, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition32, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition33, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition34, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition35, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition36, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition37, Camera.View, Camera.Projection);
            DrawGeometry(Piedra, PiedraPosition38, Camera.View, Camera.Projection);

            base.Draw(gameTime);
        }

        private void AcelerarShip(Vector3 incremento)
        {
            ShipPosition += incremento;
            ShipWorld *= Matrix.CreateTranslation(ShipPosition);
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
