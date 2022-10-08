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
        private KeyboardState _previousKBState;
        private KeyboardState _currentKBState;

        public PianoSprite(Game game): base(game){
            _game = game;
        }

        public override void Draw(GameTime gameTime)
        {   
            int pxBlack = 40;
            int pxWhite =0;
            // for(int i =0 ; i< _keys.Count; i++){
            //     if(_keys[i].Note.Contains("#")== false){
             
            //         _spriteBatch.Begin();
            //         _spriteBatch.Draw(_whiteKeyImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
            //         _spriteBatch.End();
            //         if(_keys[i].IsPressed){
            //             _spriteBatch.Begin();
            //             _spriteBatch.Draw(_pressedWhiteImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
            //             _spriteBatch.End();
            //         }
            //         pxWhite+=50;
            //     }
            // }
            for(int j =0; j< _keys.Count;j++){
                if(_keys[j].Note.Contains("#")){
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_blackKeyImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
                    _spriteBatch.End();
                    if(_keys[j].IsPressed){
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_pressedBlackImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
                        _spriteBatch.End();
                    }
                    pxBlack+=100;
                }
            }
            
            
            // for(int i = 0; i<25; i++){
            //     if(_keys[i].IsPressed == true){
            //         _spriteBatch.Begin();
            //         _spriteBatch.Draw(_pressedWhiteImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
            //         _spriteBatch.End();
            //     }else {
            //         _spriteBatch.Begin();
            //         _spriteBatch.Draw(_whiteKeyImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
            //         _spriteBatch.End();
            //     }
            //     pxWhite+=50;
                
            // }
            // for(int j =25; j < 37; j++){
                
            //     if(_keys[j].IsPressed == true){
            //         _spriteBatch.Begin();
            //         _spriteBatch.Draw(_pressedBlackImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
            //         _spriteBatch.End();
            //     }else{
            //         _spriteBatch.Begin();
            //         _spriteBatch.Draw(_blackKeyImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
            //         _spriteBatch.End();
            //     }
            //     pxBlack+=100;
            // }
            //////////
            //  for(int i = 0; i<25; i++){
            
            //     _spriteBatch.Begin();
            //     _spriteBatch.Draw(_whiteKeyImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
            //     _spriteBatch.End();
            //     pxWhite+=50;
            //     }
                
         
            // for(int j =0; j < 12; j++){
            //     _spriteBatch.Begin();
            //     _spriteBatch.Draw(_blackKeyImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
            //     _spriteBatch.End();
            //     pxBlack+=100;
            // }
            base.Draw(gameTime);
        }

        public override void Initialize()
        {   
            _previousKBState = Keyboard.GetState();
            _keys = new List<Key>();
            Piano pianoObj = new Piano();
            _keysStr = pianoObj.Keys;
            string[] keysNotes = {"C","C#","D","D#","E","F","F#","G","G#","A","A#","B","C","C#","D",
            "D#","E","F","F#","G","G#","A","A#","B","C","C#","D","D#","E","F","F#","G","G#","A","A#","B","C"};
            for(int i =0; i< _keysStr.Length; i++){
                _keys.Add(new Key(0,0,GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, false,keysNotes[i]));
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
            string pressedKey;
            int pressedKeyIndx;
            _currentKBState = Keyboard.GetState();
            Keys[] pressedKeys = _currentKBState.GetPressedKeys();
            if(_currentKBState != _previousKBState){
                for (int i =0; i< _keys.Count; i++){
                    _keys[i].IsPressed = false;
                }
                _currentKBState = _previousKBState;
                // pressedKey = LoadJson(pressedKeys[0].ToString());
                // pressedKeyIndx = _keysStr.IndexOf(pressedKey);
                // _keys[pressedKeyIndx].IsPressed = false;
            }
            if (pressedKeys.Length > 0){
                pressedKey = GetKeyStr(pressedKeys[0].ToString());
                pressedKeyIndx = _keysStr.IndexOf(pressedKey);
                _keys[pressedKeyIndx].IsPressed = true;
                
            }
            base.Update(gameTime);
        }
        public string GetKeyStr(string key){
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
            get{
                return _keys;
            }
            set{
                _keys = value;
            }
        }

    }
}