using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace SpaceCraft
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>

    public struct HUD
    {
        public int score;
        public double time;
        public int ammo;
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboard1;
        Texture2D back;
        Rectangle backrect;
        Texture2D playbut;
        Rectangle playbutrect;
        Texture2D exitbut;
        Rectangle exitbutrect;
        AnimatedTexture SpriteTexture;
        AnimatedTexture SpriteTexture1;
        Texture2D fireTexture;
        Texture2D fire1Texture;
        Texture2D fire2Texture;
        Texture2D stoneTexture;
        Texture2D ammoTexture;
        Texture2D speedTexture;
        Vector2 spritePos;
        SpriteFont arial;
        SpriteFont lotus;
        SpriteFont nina;
        SoundEffect backmusic;
        SoundEffectInstance backins;
        ScrollingBackground myBackground;
        Rectangle smallStone; // one shot
        bool smallFlag;
        Rectangle middleStone; // two shot
        bool middleFlag;
        Rectangle bigStone; // three shot
        bool bigFlag;
        Rectangle fire;
        Rectangle fire1;
        Rectangle fire2;
        bool isFire;
        bool isFire1;
        bool isFire2;
        bool menu;
        HUD hud;
        Random randGenerator;
        int randNumber;
        bool bang;
        bool gameOver;
        bool reset;
        int counter;
        bool levelFlag;
        Rectangle ammo;
        Rectangle speed;
        bool ammoFlag;
        bool ammoFlag1;
        bool speedFlag;
        bool speedFlag1;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SpriteTexture = new AnimatedTexture(Vector2.Zero, 0, 2.0f, 0.5f);
            SpriteTexture1 = new AnimatedTexture(Vector2.Zero, 0, 2.0f, 0.5f);
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 350;
            graphics.ApplyChanges();
            backrect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            playbutrect = new Rectangle((GraphicsDevice.Viewport.Width / 2) - 85, (GraphicsDevice.Viewport.Height / 2) - 35, 171, 70);
            exitbutrect = new Rectangle((GraphicsDevice.Viewport.Width / 2) - 85, (GraphicsDevice.Viewport.Height / 2) + 50, 171, 70);
            menu = true;
            spritePos = new Vector2((GraphicsDevice.Viewport.Width / 2), (GraphicsDevice.Viewport.Height / 2));
            hud = new HUD();
            hud.score = 0;
            hud.time = 0;
            hud.ammo = 100;
            randGenerator = new Random();
            randNumber = (int)(randGenerator.NextDouble() * (GraphicsDevice.Viewport.Width - 50) + 50);
            smallStone = new Rectangle(randNumber, -100, 20, 20);
            smallFlag = false;
            middleStone = new Rectangle(randNumber, -100, 30, 30);
            middleFlag = false;
            levelFlag = false;
            bigStone = new Rectangle(randNumber, -100, 50, 50);
            bigFlag = false;
            ammo = new Rectangle(randNumber, -100, 71, 23);
            speed = new Rectangle(randNumber, -100, 71, 23);
            fire = new Rectangle((int)(spritePos.X + 60), (int)(spritePos.Y + 20), 10, 10);
            fire1 = new Rectangle((int)(spritePos.X + 25), (int)(spritePos.Y + 20), 10, 10);
            fire2 = new Rectangle((int)(spritePos.X + 95), (int)(spritePos.Y + 20), 10, 10);
            isFire = false;
            isFire1 = false;
            isFire2 = false;
            bang = false;
            speedFlag = false;
            speedFlag1 = false;
            ammoFlag = false;
            ammoFlag1 = false;
            gameOver = false;
            reset = false;
            counter = 0;
            base.Initialize();
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
            back = this.Content.Load<Texture2D>("space1");
            playbut = this.Content.Load<Texture2D>("play");
            exitbut = this.Content.Load<Texture2D>("exit");
            fireTexture = this.Content.Load<Texture2D>("fire");
            fire1Texture = this.Content.Load<Texture2D>("fire");
            fire2Texture = this.Content.Load<Texture2D>("fire");
            stoneTexture = this.Content.Load<Texture2D>("stone");
            ammoTexture = this.Content.Load<Texture2D>("Amoo");
            speedTexture = this.Content.Load<Texture2D>("Speed");
            arial = this.Content.Load<SpriteFont>("Arial");
            lotus = this.Content.Load<SpriteFont>("Lotus");
            nina = this.Content.Load<SpriteFont>("Nina");
            backmusic = this.Content.Load<SoundEffect>("back");
            SpriteTexture.Load(graphics.GraphicsDevice, this.Content, "shipanimated", 4, 10); // 4, 10
            myBackground = new ScrollingBackground();
            backins = backmusic.CreateInstance();
            myBackground.Load(GraphicsDevice, back);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboard1.IsKeyDown(Keys.Escape))
                menu = true;
            // TODO: Add your update logic here
            keyboard1 = Keyboard.GetState();
            if (menu)
            {
                SpriteTexture.Pause();
                backins.Stop();
                MouseState a2 = Mouse.GetState();

                if (a2.LeftButton == ButtonState.Pressed)
                {
                    if (a2.X >= playbutrect.X && a2.X <= playbutrect.X + playbutrect.Width && a2.Y >= playbutrect.Y && a2.Y <= playbutrect.Y + playbutrect.Height)
                        menu = false;
                    if (a2.X >= exitbutrect.X && a2.X <= exitbutrect.X + exitbutrect.Width && a2.Y >= exitbutrect.Y && a2.Y <= exitbutrect.Y + exitbutrect.Height)
                        Exit();
                }
            }
            else
            {
                SpriteTexture.Play();
                backins.Play();

                randNumber = (int)(randGenerator.NextDouble() * (GraphicsDevice.Viewport.Width - 50) + 50);

                // the game is over
                if (gameOver)
                {
                    // reset the game
                    if (keyboard1.IsKeyDown(Keys.R))
                    {
                        levelFlag = false;
                        reset = true;
                        gameOver = false;
                        hud.score = 0;
                        hud.ammo = 100;
                        hud.time = (double)0;
                        isFire = false;
                        isFire1 = false;
                        isFire2 = false;
                        smallFlag = false;
                        ammoFlag = false;
                        ammoFlag1 = false;
                        speedFlag = false;
                        speedFlag1 = false;
                        bigFlag = false;
                        smallStone.X = randNumber;
                        smallStone.Y = -100;
                        fire.X = (int)(spritePos.X + 60);
                        fire.Y = (int)(spritePos.Y + 20);
                    }

                    if (!reset)
                        return;
                }

                // update time
                if (reset)
                    reset = false;
                else
                    hud.time += gameTime.ElapsedGameTime.TotalMilliseconds;

                // generate stone
                int t = (int)(hud.time / 1000);
                if ((t % 5) == 0)
                    smallFlag = true;
                if (t >= 20 && (t % 7) == 0)
                {
                    bigFlag = true;
                }
                if (t >= 30)
                {
                    if (t % 3 == 0)
                        smallFlag = true;
                    if (t % 4 == 0)
                        bigFlag = true;
                }
                if (t >= 50)
                {
                    if (t % 2 == 0)
                        smallFlag = true;
                    if (t % 3 == 0)
                        bigFlag = true;
                }

                //level status
                if (t >= 50)
                {
                    levelFlag = true;
                }
                if (t == 81)
                {
                    ammoFlag1 = true;

                }
                if (t == 61)
                    speedFlag1 = true;

                // left collision
                if (spritePos.X <= -30)
                    spritePos.X = -30;

                // up collision
                if (spritePos.Y <= -20)
                    spritePos.Y = -20;

                // right collision
                if (spritePos.X >= (GraphicsDevice.Viewport.Width - 100))
                    spritePos.X = (GraphicsDevice.Viewport.Width - 100);

                // down collision
                if (spritePos.Y >= (GraphicsDevice.Viewport.Height - 120))
                    spritePos.Y = (GraphicsDevice.Viewport.Height - 120);
                // fire the Ammo!
                if (ammoFlag1 && isFire && fire.Y <= ammo.Y + 23 && fire.Y >= ammo.Y + 5 && fire.X >= ammo.X - 10 && fire.X <= ammo.X + 71)
                {

                    //hud.ammo--;
                    ammoFlag1 = false;
                    isFire = false;
                    ammoFlag = true;
                    hud.ammo--;
                }
                if (levelFlag && ammoFlag1 && isFire && fire1.Y <= ammo.Y + 23 && fire1.Y >= ammo.Y + 5 && fire1.X >= ammo.X - 10 && fire1.X <= ammo.X + 71)
                {

                    //hud.ammo--;
                    ammoFlag1 = false;
                    isFire1 = false;
                    ammoFlag = true;
                    hud.ammo--;
                }
                if (levelFlag && ammoFlag1 && isFire && fire2.Y <= ammo.Y + 23 && fire2.Y >= ammo.Y + 5 && fire2.X >= ammo.X - 10 && fire2.X <= ammo.X + 71)
                {
                    //hud.ammo--;
                    ammoFlag1 = false;
                    isFire2 = false;
                    ammoFlag = true;
                    hud.ammo--;
                }
                // fire the Speed!
                if (speedFlag1 && isFire && fire.Y <= speed.Y + 23 && fire.Y >= speed.Y + 5 && fire.X >= speed.X - 10 && fire.X <= speed.X + 71)
                {

                    //hud.ammo--;
                    speedFlag1 = false;
                    isFire = false;
                    speedFlag = true;
                    hud.ammo--;
                }
                if (levelFlag && speedFlag1 && isFire && fire1.Y <= speed.Y + 23 && fire1.Y >= speed.Y + 5 && fire1.X >= speed.X - 10 && fire1.X <= speed.X + 71)
                {

                    //hud.ammo--;
                    speedFlag1 = false;
                    isFire1 = false;
                    speedFlag = true;
                    hud.ammo--;
                }
                if (levelFlag && speedFlag1 && isFire && fire2.Y <= speed.Y + 23 && fire2.Y >= speed.Y + 5 && fire2.X >= speed.X - 10 && fire2.X <= speed.X + 71)
                {
                    //    hud.ammo--;
                    speedFlag1 = false;
                    isFire2 = false;
                    speedFlag = true;
                    hud.ammo--;
                }
                // fire the smallstone!
                if (smallFlag && isFire && fire.Y <= smallStone.Y + 20 && fire.Y >= smallStone.Y + 5 && fire.X >= smallStone.X - 10 && fire.X <= smallStone.X + 30)
                {
                    hud.score++;
                    //hud.ammo--;
                    smallFlag = false;
                    isFire = false;
                    hud.ammo--;
                }
                if (levelFlag && smallFlag && isFire && fire1.Y <= smallStone.Y + 20 && fire1.Y >= smallStone.Y + 5 && fire1.X >= smallStone.X - 10 && fire1.X <= smallStone.X + 30)
                {
                    hud.score++;
                    //hud.ammo--;
                    smallFlag = false;
                    isFire1 = false;
                    hud.ammo--;
                }
                if (levelFlag && smallFlag && isFire && fire2.Y <= smallStone.Y + 20 && fire2.Y >= smallStone.Y + 5 && fire2.X >= smallStone.X - 10 && fire2.X <= smallStone.X + 30)
                {
                    hud.score++;
                    //hud.ammo--;
                    smallFlag = false;
                    isFire2 = false;
                    hud.ammo--;
                }
                // fire the bigstone!
                if (bigFlag && isFire && fire.Y <= bigStone.Y + 30 && fire.Y >= bigStone.Y + 10 && fire.X >= bigStone.X - 15 && fire.X <= bigStone.X + 40)
                {
                    hud.score++;
                    //hud.ammo--;
                    counter++;
                    isFire = false;
                    hud.ammo--;
                }
                if (bigFlag && isFire1 && fire1.Y <= bigStone.Y + 30 && fire1.Y >= bigStone.Y + 10 && fire1.X >= bigStone.X - 15 && fire1.X <= bigStone.X + 40)
                {
                    hud.score++;
                    //hud.ammo--;
                    counter++;
                    isFire1 = false;
                    hud.ammo--;
                }
                if (bigFlag && isFire2 && fire2.Y <= bigStone.Y + 30 && fire2.Y >= bigStone.Y + 10 && fire2.X >= bigStone.X - 15 && fire2.X <= bigStone.X + 40)
                {
                    hud.score++;
                    //  hud.ammo--;
                    counter++;
                    isFire2 = false;
                    hud.ammo--;
                }
                if (counter >= 2)
                {
                    bigFlag = false;
                    counter = 0;
                }
                // game over!
                if (!levelFlag)
                {
                    if (((smallStone.X >= spritePos.X + 10) && (smallStone.X <= spritePos.X + 100) && (smallStone.Y >= spritePos.Y) && (smallStone.Y <= spritePos.Y + 120)) || (hud.ammo <= 0))
                        gameOver = true;
                    if (((bigStone.X >= spritePos.X + 10) && (bigStone.X <= spritePos.X + 100) && (bigStone.Y >= spritePos.Y) && (bigStone.Y <= spritePos.Y + 120)) || (hud.ammo <= 0))
                        gameOver = true;
                }
                if (levelFlag)
                {
                    if (((smallStone.X >= spritePos.X + 5) && (smallStone.X <= spritePos.X + 110) && (smallStone.Y >= spritePos.Y) && (smallStone.Y <= spritePos.Y + 120)) || (hud.ammo <= 0))
                        gameOver = true;
                    if (((bigStone.X >= spritePos.X + 3) && (bigStone.X <= spritePos.X + 100) && (bigStone.Y >= spritePos.Y) && (bigStone.Y <= spritePos.Y + 120)) || (hud.ammo <= 0))
                        gameOver = true;
                }
                // stone move
                if (smallFlag)
                    smallStone.Y += 3;
                else
                {
                    smallStone.X = randNumber;
                    smallStone.Y = -100;
                }
                // Big stone move
                if (bigFlag)
                    bigStone.Y += 3;
                else
                {
                    bigStone.X = randNumber;
                    bigStone.Y = -100;
                }
                // Speed  move
                if (speedFlag1)
                    speed.Y += 3;
                else
                {
                    speed.X = randNumber;
                    speed.Y = -100;
                }
                // ammo  move
                if (ammoFlag1)
                    ammo.Y += 3;
                else
                {
                    ammo.X = randNumber;
                    ammo.Y = -100;
                }
                // stone passed
                if (smallStone.Y >= GraphicsDevice.Viewport.Height)
                {
                    smallFlag = false;
                }
                if (bigStone.Y >= GraphicsDevice.Viewport.Height)
                {
                    bigFlag = false;
                    counter = 0;
                }
                // fire move
                if (levelFlag)
                {
                    if (isFire1 || isFire || isFire2)
                    {
                        fire.Y -= 5;
                        fire1.Y -= 5;
                        fire2.Y -= 5;
                    }
                    else
                    {
                        fire.X = (int)(spritePos.X + 60);
                        fire.Y = (int)(spritePos.Y + 20);
                        fire1.X = (int)(spritePos.X + 25);
                        fire1.Y = (int)(spritePos.Y + 20);
                        fire2.X = (int)(spritePos.X + 95);
                        fire2.Y = (int)(spritePos.Y + 20);
                    }
                }
                if (!levelFlag)
                {
                    if (isFire)
                        fire.Y -= 5;
                    else
                    {
                        fire.X = (int)(spritePos.X + 60);
                        fire.Y = (int)(spritePos.Y + 20);
                    }
                }
                // fire passed
                if (levelFlag)
                {
                    if (fire.Y <= 0)
                    {

                        isFire = false;
                        hud.ammo--;
                    }
                    if (fire1.Y <= 0)
                    {
                        isFire1 = false;
                        hud.ammo--;
                    }
                    if (fire2.Y <= 0)
                    {
                        isFire2 = false;
                        hud.ammo--;
                    }
                }

                // fire passed
                if (!levelFlag)
                {
                    if (fire.Y <= 0)
                    {
                        isFire = false;
                        hud.ammo--;
                    }
                }

                // fire!
                if (!levelFlag && keyboard1.IsKeyDown(Keys.Space) && hud.ammo > 0)
                {
                    isFire = true;

                }
                if (levelFlag && keyboard1.IsKeyDown(Keys.Space) && hud.ammo > 0)
                {
                    isFire = true;
                    isFire1 = true;
                    isFire2 = true;
                }

                // move up
                if (keyboard1.IsKeyDown(Keys.Up))
                    spritePos.Y -= 5;

                // move down
                if (keyboard1.IsKeyDown(Keys.Down))
                    spritePos.Y += 5;

                // move left
                if (keyboard1.IsKeyDown(Keys.Left))
                    spritePos.X -= 5;

                // move right
                if (keyboard1.IsKeyDown(Keys.Right))
                    spritePos.X += 5;
                // move for speed
                if (speedFlag)
                {
                    // move up
                    if (keyboard1.IsKeyDown(Keys.Up))
                        spritePos.Y -= 7;

                    // move down
                    if (keyboard1.IsKeyDown(Keys.Down))
                        spritePos.Y += 7;

                    // move left
                    if (keyboard1.IsKeyDown(Keys.Left))
                        spritePos.X -= 7;

                    // move right
                    if (keyboard1.IsKeyDown(Keys.Right))
                        spritePos.X += 7;
                }
                //ammo ++
                if (ammoFlag)
                {
                    hud.ammo = hud.ammo + 20;
                    ammoFlag = false;
                }
                bang = true;
                SpriteTexture.UpdateFrame(elapsed);
                myBackground.Update(elapsed * 500);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (menu)
            {
                spriteBatch.DrawString(arial, "SpaceCraft", new Vector2((float)(GraphicsDevice.Viewport.Width / 4 * 2 - arial.MeasureString("SpaceCraft").X / 2), (float)50), Color.Tomato);
                spriteBatch.Draw(playbut, playbutrect, Color.White);
                spriteBatch.Draw(exitbut, exitbutrect, Color.White);
                this.IsMouseVisible = true;
            }
            else
            {
                myBackground.Draw(spriteBatch);
                if (levelFlag)
                {
                    SpriteTexture1.DrawFrame(spriteBatch, spritePos);
                }
                else
                {
                    SpriteTexture.DrawFrame(spriteBatch, spritePos);
                }
                this.IsMouseVisible = false;
                spriteBatch.DrawString(lotus, "Time: " + ((int)hud.time / 1000).ToString(), new Vector2((float)(10), (float)10), Color.White);
                spriteBatch.DrawString(lotus, "Score: " + hud.score.ToString(), new Vector2((float)(10), (float)25), Color.White);
                spriteBatch.DrawString(lotus, "Ammo: " + hud.ammo.ToString(), new Vector2((float)(10), (float)40), Color.White);
                if (isFire && !levelFlag)
                    spriteBatch.Draw(fireTexture, fire, Color.White);
                if (isFire && levelFlag)
                {
                    spriteBatch.Draw(fireTexture, fire, Color.White);
                }
                if (isFire1 && levelFlag)
                {
                    spriteBatch.Draw(fire1Texture, fire1, Color.White);
                }
                if (isFire2 && levelFlag)
                {
                    spriteBatch.Draw(fire1Texture, fire2, Color.White);
                }
                if (smallFlag)
                    spriteBatch.Draw(stoneTexture, smallStone, Color.White);
                if (middleFlag)
                    spriteBatch.Draw(stoneTexture, middleStone, Color.White);
                if (bigFlag)
                    spriteBatch.Draw(stoneTexture, bigStone, Color.White);
                if (speedFlag1)
                    spriteBatch.Draw(speedTexture, speed, Color.White);
                if (ammoFlag1)
                    spriteBatch.Draw(ammoTexture, ammo, Color.White);
                if (gameOver)
                {
                    spriteBatch.DrawString(nina, "Game Over", new Vector2((float)(GraphicsDevice.Viewport.Width / 4 * 2 - nina.MeasureString("Game Over").X / 2), (float)100), Color.Red);
                    spriteBatch.DrawString(nina, "< Press R to reset the game >", new Vector2((float)(GraphicsDevice.Viewport.Width / 4 * 2 - nina.MeasureString("< Press R to reset the game >").X / 2), (float)130), Color.White);
                }
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
public class AnimatedTexture
{
    private int framecount;
    private Texture2D myTexture;
    private float TimePerFrame;
    private int Frame;
    private float TotalElapsed;
    private bool Paused;
    public float Rotation, Scale, Depth;
    public Vector2 Origin;
    public AnimatedTexture(Vector2 Origin, float Rotation, float Scale, float Depth)
    {
        this.Origin = Origin;
        this.Rotation = Rotation;
        this.Scale = Scale;
        this.Depth = Depth;
    }
    public void Load(GraphicsDevice device, ContentManager content, string asset, int FrameCount, int FramesPerSec)
    {
        framecount = FrameCount;
        myTexture = content.Load<Texture2D>(asset);
        TimePerFrame = (float)1 / FramesPerSec;
        Frame = 0;
        TotalElapsed = 0;
        Paused = false;
    }
    public void UpdateFrame(float elapsed)
    {
        if (Paused)
            return;
        TotalElapsed += elapsed;
        if (TotalElapsed > TimePerFrame)
        {
            Frame++;
            Frame = Frame % framecount;
            TotalElapsed -= TimePerFrame;
        }
    }
    public void DrawFrame(SpriteBatch Batch, Vector2 screenpos)
    {
        DrawFrame(Batch, Frame, screenpos);
    }
    public void DrawFrame(SpriteBatch Batch, int Frame, Vector2 screenpos)
    {
        int FrameWidth = myTexture.Width / framecount;
        Rectangle sourcerect = new Rectangle(FrameWidth * Frame, 0,
            FrameWidth, myTexture.Height);
        Batch.Draw(myTexture, screenpos, sourcerect, Color.White,
            Rotation, Origin, Scale, SpriteEffects.None, Depth);
    }

    public bool IsPaused
    {
        get { return Paused; }
    }
    public void Reset()
    {
        Frame = 0;
        TotalElapsed = 0f;
    }
    public void Stop()
    {
        Pause();
        Reset();
    }
    public void Play()
    {
        Paused = false;
    }
    public void Pause()
    {
        Paused = true;
    }

}

public class ScrollingBackground
{
    private Vector2 screenpos, origin, texturesize;
    private Texture2D mytexture;
    private int screenheight;
    public void Load(GraphicsDevice device, Texture2D backgroundTexture)
    {
        mytexture = backgroundTexture;
        screenheight = device.Viewport.Height;
        int screenwidth = device.Viewport.Width;
        origin = new Vector2(mytexture.Width / 2, 0);
        screenpos = new Vector2(screenwidth / 2, screenheight / 2);
        texturesize = new Vector2(0, mytexture.Height);
    }
    public void Update(float deltaY)
    {
        screenpos.Y += deltaY;
        screenpos.Y = screenpos.Y % mytexture.Height;
    }
    public void Draw(SpriteBatch batch)
    {
        if (screenpos.Y < screenheight)
        {
            batch.Draw(mytexture, screenpos, null,
                    Color.White, 0, origin, 1, SpriteEffects.None, 0f);
        }
        batch.Draw(mytexture, screenpos - texturesize, null,
                Color.White, 0, origin, 1, SpriteEffects.None, 0f);
    }
}

