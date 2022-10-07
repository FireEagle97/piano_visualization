namespace InteractivePiano{
    public class Key{
        private float _screenWidth;
        private float _screenHeight;
        private Coordinate _position;
        private bool _isPressed;

        public Key(float width, float height, float screenWidth, float screenHeight, bool isPressed){
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            width = Width;
            height = Height;
            _position = new Coordinate(Width,Height);
            _isPressed = isPressed;
           
        }
        public Coordinate Position{
            get{
                return _position;
            }
            
        }
        public bool IsPressed{
            get{
                return _isPressed;
            }
            set{
                _isPressed = value;
            }
        }
        public float Width{get;}
        public float Height{get;}
    }
}