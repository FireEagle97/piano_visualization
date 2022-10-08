using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
namespace InteractivePiano{
    public class PianoSprite : DrawableGameComponent{
        private List<Key> _keys;
        private SpriteBatch _spriteBatch;
        private Texture2D _whiteKeyImage;
        private Texture2D _blackKeyImage;
        private Texture2D _pressedBlackImage;
        private Texture2D _pressedWhiteImage;
        private SpriteFont note;
        private string _keysStr;
        private List<string>_keysNotes;
        private Game _game;
        private KeyboardState _previousKBState;
        private KeyboardState _currentKBState;

        public PianoSprite(Game game): base(game){
            _game = game;
            
        }

        public override void Draw(GameTime gameTime)
        {   
            int pxBlack = 80;
            int pxWhite =0;
            for(int i =0 ; i< _keys.Count; i++){
                if(_keys[i].Note.Contains("#") == false){
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_whiteKeyImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
                    _spriteBatch.End();
                    if(_keys[i].IsPressed){
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_pressedWhiteImage, new Vector2(_keys[i].Position.X+pxWhite, _keys[i].Position.Y), Color.White);
                        _spriteBatch.DrawString(note,_keysNotes[i], new Vector2(_keys[i].Position.X+pxWhite,250), Color.Red);
                        _spriteBatch.End();
                    }
                    pxWhite+=50;
                }
            }
            for(int j =0; j< _keys.Count;j++){
                if(_keys[j].Note.Contains("#")){
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(_blackKeyImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
                    _spriteBatch.End();
                    if(_keys[j].IsPressed){
                        _spriteBatch.Begin();
                        _spriteBatch.Draw(_pressedBlackImage, new Vector2(_keys[j].Position.X+pxBlack, _keys[j].Position.Y), Color.White);
                        _spriteBatch.DrawString(note,_keysNotes[j], new Vector2(_keys[j].Position.X+pxBlack,250), Color.Red);
                        _spriteBatch.End();
                    }
                    pxBlack+=68;
                }
            }
            base.Draw(gameTime);
        }

        public override void Initialize()
        {   
            _previousKBState = Keyboard.GetState();
            _keys = new List<Key>();
            Piano pianoObj = new Piano();
            _keysStr = pianoObj.Keys;
            _keysNotes = new List<string>{"C","C#","D","D#","E","F","F#","G","G#","A","A#","B","C","C#","D",
            "D#","E","F","F#","G","G#","A","A#","B","C","C#","D","D#","E","F","F#","G","G#","A","A#","B","C", "keyNotFound"};
            for(int i =0; i< _keysStr.Length; i++){
                _keys.Add(new Key(0,0,GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, false,_keysNotes[i]));
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
            note = _game.Content.Load<SpriteFont>("note");   
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
            }
            if (pressedKeys.Length > 0){
                pressedKey = GetKeyStr(pressedKeys[0].ToString());
                if(pressedKey.Length >0){
                    pressedKeyIndx = _keysStr.IndexOf(pressedKey);
                    _keys[pressedKeyIndx].IsPressed = true;
                }
            }

            base.Update(gameTime);
        }
        public string GetKeyStr(string key){
            Dictionary<string,string> keys = new Dictionary<string,string>(){
            {"Q","q"},{"D2","2"},{"W","w"},{"E","e"},{"D4","4"},{"R","r"},{"D5","5"},
            {"T","t"},{"Y","y"},{"D7","7"},{"U","u"},{"D8","8"},{"I","i"},{"D9","9"},
            {"O","o"},{"P","p"},{"OemMinus","-"},{"OemOpenBrackets","["},{"OemPlus","="},
            {"Z","z"},{"X","x"},{"D","d"},{"C","c"},{"F","f"},{"V","v"},{"G","g"},{"B","b"},
            {"N","n"},{"J","j"},{"M","m"},{"K","k"},{"OemComma",","},{"OemPeriod","."},{"OemSemicolon",";"},
            {"OemQuestion","/"},{"OemQuotes","'"},{"Space"," "}
            };
            if(keys.ContainsKey(key)){
                return keys[key];
            }
            return "";
      
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