using System;
namespace InteractivePiano{

    public class PianoWire : IMusicalString{

       private CircularArray _samplesArray;
        public PianoWire(double noteFrequency, int samplingRate){

            NoteFrequency = noteFrequency;
            NumberOfSamples = Convert.ToInt32(samplingRate/noteFrequency);
            _samplesArray = new CircularArray(NumberOfSamples);

        }
        /// The frequency of the note generated by the string
        public double NoteFrequency{get;}
        /// Number of samples generated by the string simluation
        public int NumberOfSamples{
            get;
        }

        /// This method simulates striking the wire by replacing all of the values
        /// in the ring buffer with random values from the range -0.5 to 0.5. 
        public void Strike(){
            double[] randArr = new double[NumberOfSamples];
            Random rnd = new Random();
            for(int i = 0;i<randArr.Length;i++){
                randArr[i] = rnd.NextDouble()- 0.5;  
            }
            _samplesArray.Fill(randArr);

       }
        /// This method adds a new value to the rear, which is the average of the two
        /// first values multiplied by the decay factor. It removes the value  
        /// currently at the front, and  returns it.
        public double Sample(double decay=0.996){
            //calls shift
           double valueToAdd = ((_samplesArray[0]+_samplesArray[1])/2.0) * decay;
            return _samplesArray.Shift(valueToAdd);
        }

}
}