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
        private Texture2D _greyImage;
        private Game _game;

        public PianoSprite(Game game): base(game){
            _game = game;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //add images here
            base.LoadContent();
        }

    }
}