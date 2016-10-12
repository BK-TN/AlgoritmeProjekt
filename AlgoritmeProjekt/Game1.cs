﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlgoritmeProjekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private const int WORLD_WIDTH = 10;
        private const int WORLD_HEIGHT = 10;
        private const int WORLD_TILESIZE = 48;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Texture2D grass;
        private Texture2D sand;

        private World world;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            world = new World(Content, WORLD_WIDTH, WORLD_HEIGHT, WORLD_TILESIZE);

            Pathfinder wizPathfinder = new DepthFirst(world.CollisionGrid);

            world.AddEntity(new Portal() { Position = world.GridPosToVector(0, 8) });
            world.AddEntity(new Wizard(wizPathfinder) { Position = world.GridPosToVector(1, 8) });

            world.AddEntity(new Tower(TowerType.StormTower) { Position = world.GridPosToVector(2, 4) });
            world.AddEntity(new Tower(TowerType.IceTower) { Position = world.GridPosToVector(8, 7) });

            world.AddEntity(new Key(TowerType.StormTower) { Position = world.GridPosToVector(0, 0) });
            world.AddEntity(new Key(TowerType.IceTower) { Position = world.GridPosToVector(9, 9) });

            for (int y = 1; y <= 6; y++)
            {
                for (int x = 4; x <= 6; x++)
                {
                    world.AddEntity(new Wall() { Position = world.GridPosToVector(x, y) });
                }
            }

            for (int x = 2; x < 7; x++)
            {
                world.AddEntity(new Tree() { Position = world.GridPosToVector(x, 7) });
                world.AddEntity(new Tree() { Position = world.GridPosToVector(x, 9) });
            }

            world.AddEntity(new Monster() { Position = world.GridPosToVector(5, 8) });
            world.AddEntity(new Monster() { Position = world.GridPosToVector(6, 8) });
            world.AddEntity(new Monster() { Position = world.GridPosToVector(7, 8) });

            IsMouseVisible = true;
            this.graphics.PreferredBackBufferWidth = WORLD_WIDTH * WORLD_TILESIZE;
            this.graphics.PreferredBackBufferHeight = WORLD_HEIGHT * WORLD_TILESIZE;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            grass = Content.Load<Texture2D>("grass");
            sand = Content.Load<Texture2D>("sand");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            world.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    spriteBatch.Draw(grass, world.GridPosToVector(x, y), Color.White);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                spriteBatch.Draw(sand, world.GridPosToVector(2 + i, 8), Color.White);
            }
            world.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
