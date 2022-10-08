using System;
namespace InteractivePiano{

    public class CircularArray : IRingBuffer{
       private double[] _buffer;
        public CircularArray(int bufferLength){
            if(bufferLength >0){
                _buffer = new double[bufferLength];
            }else{
                throw new ArgumentException("Array length should be a positive number");
            }
            
            indexPositon = 0;
        }
        public int Length {
            get{
               return _buffer.Length;
            }
        }

        public int indexPositon{
            get;
            set;
        }
        // Returns and removes the first element in the buffer. Adds value to the end of the buffer
        public double Shift(double value){
            
            double firstValue = _buffer[indexPositon];
            _buffer[indexPositon]=value;
            indexPositon = (indexPositon + 1 )% Length;
            return firstValue;
        }
        /// Indexer to go through elements in the buffer starting at the front to the value at the end
        public double this[int index] {
            get {return _buffer[(indexPositon + index )% Length];}
        }
        /// Performs a deep copy of the array into the buffer
        public void Fill(double[] array){
            array.CopyTo(_buffer,0);
        }
    }
}