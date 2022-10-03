
namespace InteractivePiano{

    public class CircularArray : IRingBuffer{
        public CircularArray(int arrLength){
            arr = new double[arrLength];
            indexPositon = 0;
        }
       public double[] arr{
        get;
        set;
       }
        public int Length {
            get{
               return arr.Length;
            }
        }

        public int indexPositon{
            get;
            set;
        }
        // Returns and removes the first element in the buffer. Adds value to the end of the buffer
        public double Shift(double value){
            
            double firstValue = arr[indexPositon];
            arr[indexPositon]=value;
            indexPositon = (indexPositon + 1 )% Length;
            return firstValue;
        }
        /// Indexer to go through elements in the buffer starting at the front to the value at the end
        public double this[int index] {
            get {return arr[(indexPositon + index )% Length];}
        }
        /// Performs a deep copy of the array into the buffer
        public void Fill(double[] array){
            array.CopyTo(arr,0);
        }
    }
}