using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace InteractivePiano
{
    public class InteractivePianoGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        // private List<PianoSprite> _pianoSprites;
        private PianoSprite _pianoSprite;
        private Piano _pianoObj;
        private Audio _audioObj;

        public InteractivePianoGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _pianoSprite = new PianoSprite(this);
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            _audioObj = Audio.Instance;
            this.Components.Add(_pianoSprite);

            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _pianoObj = new Piano();
            KeyboardState ks = Keyboard.GetState();
            Keys[] pressedKey = ks.GetPressedKeys();
            if(pressedKey.Length > 0){
                string pressedKeyStr = _pianoSprite.GetKeyStr(pressedKey[0].ToString());
                if(pressedKeyStr.Length > 0){
                    _pianoObj.StrikeKey(pressedKeyStr[0]);
                    _audioObj.Reset();
                    for(int i =0 ; i< _pianoObj.SamplingRate*3; i++){
                        _audioObj.Play(_pianoObj.Play());
                    }
                }
                
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        
    }
}
