using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System.IO;
using Newtonsoft.Json;
namespace InteractivePiano{
    public class PianoSprite : DrawableGameComponent{
        private List<Key> _keys;
        private SpriteBatch _spriteBatch;
        private Texture2D _whiteKeyImage;
        private Texture2D _blackKeyImage;
        private Texture2D _pressedBlackImage;
        private Texture2D _pressedWhiteImage;
        private string _keysStr;
        // private Texture2D _greyImage;
        private Game _game;
        private KeyboardState _keyBoardState;

        public PianoSprite(Game game): base(game){
            _game = game;
            _keys = Keys;
        }

        public override void Draw(GameTime gameTime)
        {   
            int pxBlack = 40;
            int pxWhite =0;
            
            for(int i = 0; i<25; i++){
                _spriteBatch.Begin();
                if(_keys[i].IsPressed == true){
                    _spriteBatch.Draw(_pressedWhiteImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
                }else {
                    _spriteBatch.Draw(_whiteKeyImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
                }
                _spriteBatch.End();
                pxWhite+=50;
                
            }
            for(int j =0; j < 12; j++){
                _spriteBatch.Begin();
                if(_keys[j].IsPressed == true){
                    _spriteBatch.Draw(_pressedBlackImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
                }else{
                    _spriteBatch.Draw(_blackKeyImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
                }
                _spriteBatch.End();
                pxBlack+=100;
            }
            base.Draw(gameTime);
        }

        public override void Initialize()
        {   
            _keyBoardState = Keyboard.GetState();
            _keys = new List<Key>();
            Piano piano = new Piano();
            _keysStr = piano.Keys;
            for(int i =0; i< _keysStr.Length; i++){
                _keys.Add(new Key(0,0,GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, false));
            } 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _whiteKeyImage = _game.Content.Load<Texture2D>("white_key");
            _blackKeyImage = _game.Content.Load<Texture2D>("black_key");
            _pressedBlackImage = _game.Content.Load<Texture2D>("pressed_black_key");
            _pressedWhiteImage = _game.Content.Load<Texture2D>("pressed_white_key");   
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            // int keyIndex;
            // string pressedKey;
            // _keyBoardState = Keyboard.GetState();
            // foreach (Keys a in _keyBoardState.GetPressedKeys()){
            //     pressedKey = a.ToString();
            //     string pressedKeyStr = LoadJson(pressedKey);
            //     if(char.IsNumber(pressedKeyStr[0])){
            //         keyIndex = _keysStr.IndexOf(pressedKeyStr);
            //     int pxBlack = 40;
            //     int pxWhite =0;
            
            //     for(int i = 0; i<25; i++){
            //         _spriteBatch.Begin();
            //         _spriteBatch.Draw(_whiteKeyImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
            //         _spriteBatch.End();
            //         pxWhite+=50;
                    
            //     }
            //     for(int j =0; j < 12; j++){
            //         _spriteBatch.Begin();
            //         _spriteBatch.Draw(_pressedBlackImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
            //         _spriteBatch.End();
            //         pxBlack+=100;
            //     }
            //     }
            // }
            base.Update(gameTime);
        }
        public string LoadJson(string key){
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
        public List<Key> Keys{
            get;
            set;
        }

    }
}