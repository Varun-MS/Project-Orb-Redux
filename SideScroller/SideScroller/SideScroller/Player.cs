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

namespace SideScroller
{
    class Player
    {
        #region Fields

        // Player One Animation Strip Data
        const string STRIP_NAME = "p1_spritesheet";
        Texture2D playerOneAnimationStrip;
        int playerOneInitialFrameWidth = 66;
        int playerOneInitialFrameHeight = 92;

        // Fields Used to Track and Draw Animations 
        Rectangle playerDrawRectangle;
        Rectangle playerSourceRectangle;
        int frameNumber = 0;
        int elapsedFrameTime = 10;
        const int NUM_FRAMES = 11;
        const int FRAME_TIME = 20;
        
        // Physics Properties
        int jumpAmount = 20;
        bool isWalkingLeft = false;
        bool isWalkingRight = false;
        bool isJumping = false;
        bool isStill = true;
        bool isFacingForward = false;
       
        #endregion

        #region Constructors

        ///<summary>
        ///Contructor for the player
        ///</summary>
        ///<param name="contentManager"> the Game's Content Manager</param>
        ///<param name="position">Position of the Player Chractor</param>
        public Player(ContentManager contentManager, Vector2 position)
        {
            //Initializes Player Charactor
            playerOneAnimationStrip = contentManager.Load<Texture2D>(STRIP_NAME);
            playerSourceRectangle = new Rectangle(0, 196, playerOneInitialFrameWidth, playerOneInitialFrameHeight);
            playerDrawRectangle = new Rectangle((int)position.X, (int)position.Y, playerOneInitialFrameWidth, playerOneInitialFrameHeight);
        }

        #endregion

        #region Public Methods

        ///<summary>
        ///Draws the Player Charactor
        ///</summary>
        ///<param name="spriteBatch">the XNA spritebatch to use for drawing</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerOneAnimationStrip, playerDrawRectangle, playerSourceRectangle, Color.White);
        }

        ///<summary>
        ///Determines The State of the Player
        ///</summary>
        /// <param name="pressedKeys">Indicates the pressed keys at time of update</param>
        public void getPlayerState(KeyboardState pressedKeys)
        {
            //To revert states to defaults so that the next frame starts afresh
            resetPlayerState();
            
            if (pressedKeys.IsKeyDown(Keys.A) && !pressedKeys.IsKeyDown(Keys.D))
            {
                isWalkingLeft = true;
                isWalkingRight = false;
                isStill = false;
                isFacingForward = false;
            }

            else if (pressedKeys.IsKeyDown(Keys.D) && !pressedKeys.IsKeyDown(Keys.A))
            {
                isWalkingRight = true;
                isWalkingLeft = false;
                isStill = false;
                isFacingForward = false;
            }

            else// if (!pressedKeys.IsKeyDown(Keys.D) && !pressedKeys.IsKeyDown(Keys.A))
            {
                isStill = true;
                isWalkingLeft = false;
                isWalkingRight = false;
            }

            if (pressedKeys.IsKeyDown(Keys.W))
            {
                isJumping = true;
                isFacingForward = false;
            }

            if (isStill && pressedKeys.IsKeyDown(Keys.D) && pressedKeys.IsKeyDown(Keys.A) && !pressedKeys.IsKeyDown(Keys.W))
            {
                isFacingForward = true;
            }
        }

        void resetPlayerState()
        {
            bool isWalkingLeft = false;
            bool isWalkingRight = false;
            bool isJumping = false;
            bool isStill = true;
            bool isFacingForward = false;
        }

        /// <summary>
        /// Updates Player Position
        /// </summary>
        /// <param name="pressedKeys">Indicates the pressed keys at time of update</param>
        /// <param name="gameTime">Game Time</param>
        public void Update(KeyboardState pressedKeys, GameTime gameTime)
        {
            getPlayerState(pressedKeys);

            ///<summary>
            ///Animations Generated Based on Player Movement
            ///</summary>  
            if (isJumping)
            {
                //Makes Player Jump and Generates Jump Animation
                Jump(gameTime);
            }
            
            if (isWalkingLeft)
            {
                //Moves Player to the Left
                DrawRectangleX = DrawRectangleX - 4;

                //Starts Walk Animation, if Not Jumping                
                if (!isJumping)
                {
                    AnimateWalk(gameTime);
                    jumpAmount = 20;
                }
                else
                {
                    animateJump();
                }
            }
            
            if (isWalkingRight)
            {
                //Moves Player to the Right
                DrawRectangleX = DrawRectangleX + 4;

                //Starts Walk Animation, if Not Jumping                
                if (!isJumping)
                {
                    AnimateWalk(gameTime);
                    jumpAmount = 20;
                }
                else
                {
                    animateJump();
                }
            }

            if (isStill)
            {
                SourceRectangleWidth = 66;
                SourceRectangleHeight = 92;
                DrawRectangleWidth = 66;
                DrawRectangleHeight = 92;
                 
                playerSourceRectangle.X = 67;
                playerSourceRectangle.Y = 196;
            }


            if (isFacingForward)
            {
                SourceRectangleWidth = 66;
                SourceRectangleHeight = 92;
                DrawRectangleWidth = 66;
                DrawRectangleHeight = 92;

                playerSourceRectangle.X = 0;
                playerSourceRectangle.Y = 196;
            }
         }

        #endregion

        #region Private Methods

        ///<summary>
        ///Sets the source rectangle Location to correspond to desired frame
        ///</summary>
        ///<param name="frameNumber">Frame Number of the current animation frame</param> 
        private void SetSourceRectangleLocation(int frameNumber)
        {
            // Calculate X and Y based on frame number
            switch (frameNumber)
            {
                case 1: playerSourceRectangle.X = 0;
                        playerSourceRectangle.Y = 0;
                        break;

                case 2: playerSourceRectangle.X = 73;
                        playerSourceRectangle.Y = 0;
                        break;

                case 3: playerSourceRectangle.X = 146;
                        playerSourceRectangle.Y = 0;
                        break;

                case 4: playerSourceRectangle.X = 0;
                        playerSourceRectangle.Y = 98;
                        break;

                case 5: playerSourceRectangle.X = 73;
                        playerSourceRectangle.Y = 98;
                        break;

                case 6: playerSourceRectangle.X = 146;
                        playerSourceRectangle.Y = 98;
                        break;

                case 7: playerSourceRectangle.X = 219;
                        playerSourceRectangle.Y = 0;
                        break;

                case 8: playerSourceRectangle.X = 292;
                        playerSourceRectangle.Y = 0;
                        break;

                case 9: playerSourceRectangle.X = 219;
                        playerSourceRectangle.Y = 98;
                        break;

                case 10: playerSourceRectangle.X = 365;
                         playerSourceRectangle.Y = 0;
                         break;

                case 11: playerSourceRectangle.X = 292;
                         playerSourceRectangle.Y = 98;
                         break;
            }
        }

        ///<summary>
        ///Makes The Player Jump
        ///</summary>
        ///<param name="gameTime">XNA Gametime</param>
        private void Jump(GameTime gameTime)
        {
            animateJump();

            DrawRectangleY = DrawRectangleY - jumpAmount;
            jumpAmount = jumpAmount - 1;

            if (jumpAmount < -20)
            {
                isJumping = false;
            }
        }

        private void animateJump()
        {
            SourceRectangleWidth = 67;
            SourceRectangleHeight = 94;

            DrawRectangleWidth = 67;
            DrawRectangleHeight = 94;

            playerSourceRectangle.X = 438;
            playerSourceRectangle.Y = 93;
        }
        
        ///<summary>
        ///Support for the Walk Animation
        ///</summary>
        ///<param name="gameTime">XNA Gametime</param>
        private void AnimateWalk(GameTime gameTime)
        {
            //Appropriately Modifies Rectangle Widths Based On Sprite Dimensions
            SourceRectangleWidth = 72;
            SourceRectangleHeight = 97;
            DrawRectangleWidth = 72;
            DrawRectangleHeight = 97;

            elapsedFrameTime += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedFrameTime > FRAME_TIME)
            {
                frameNumber++;

                SetSourceRectangleLocation(frameNumber);

                if (frameNumber > NUM_FRAMES - 1)
                {
                    frameNumber = 0;
                }

                elapsedFrameTime = 0;
            }
        }

        #endregion

        #region Properties

        ///<summary>
        ///sets the position of the player
        ///</summary>
        private int DrawRectangleX
        {
            get
            {
                return playerDrawRectangle.X;
            }

            set
            {
                playerDrawRectangle.X = value;

                if (playerDrawRectangle.Left < 0)
                {
                    playerDrawRectangle.X = 0;
                }

                if (playerDrawRectangle.Right > 800)
                {
                    playerDrawRectangle.X = 800 - playerDrawRectangle.Width;
                }
            }
        }

        private int DrawRectangleY
        {
            get
            {
                return playerDrawRectangle.Y;
            }

            set
            {
                playerDrawRectangle.Y = value;

                if (playerDrawRectangle.Top < 0)
                {
                    playerDrawRectangle.Y = 0;
                }

                if (playerDrawRectangle.Bottom > 600)
                {
                    playerDrawRectangle.Y = 600 - playerDrawRectangle.Height;
                }
            }
        }

        private int DrawRectangleWidth
        {
            get
            {
                return playerDrawRectangle.Width;
            }

            set
            {
                playerDrawRectangle.Width = value;
            }
        }

        private int DrawRectangleHeight
        {
            get
            {
                return playerDrawRectangle.Height;
            }

            set
            {
                playerDrawRectangle.Height = value;
            }
        }

        private int SourceRectangleWidth
        {
            get
            {
                return playerSourceRectangle.Width;
            }

            set
            {
                playerSourceRectangle.Width = value;
            }
        }

        private int SourceRectangleHeight
        {
            get
            {
                return playerSourceRectangle.Height;
            }

            set
            {
                playerSourceRectangle.Height = value;
            }
        }        
        
        #endregion
    }
}