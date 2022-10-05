using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Newtonsoft.Json;
namespace InteractivePiano
{
    public class InteractivePianoGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Piano _pianoObj;
        private Audio _audioObj;

        public InteractivePianoGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _audioObj = Audio.Instance;
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
            string pressedKey;
            KeyboardState ns = Keyboard.GetState();
            foreach (Microsoft.Xna.Framework.Input.Keys a in ns.GetPressedKeys())
            {   
                pressedKey = a.ToString();
                string pressedKeyChar = LoadJson(pressedKey);
                if(pressedKeyChar.Length > 0){
                    _pianoObj.StrikeKey(pressedKeyChar[0]);
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
        private string LoadJson(string key){
            string keyStr = "";
            using (StreamReader r = new StreamReader("keys.json"))
            {
                string json = r.ReadToEnd();
                if (json.Contains(key)){
                    dynamic keysArray = JsonConvert.DeserializeObject(json);
                    foreach (var item in keysArray){
                        keyStr = item[key].key;
                    }
                }
                
            }
            return keyStr;

            
        }
    }
}
