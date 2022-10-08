using System.Collections.Generic;
using System;
namespace InteractivePiano{
    
    public class Piano : IPiano{
        private List<IMusicalString> _wiresArray;
        public Piano(string keys = "q2we4r5ty7u8i9op-[=zxdcfvgbnjmk,.;/' ",  int samplingRate = 44100){
            Keys = keys;
            SamplingRate = samplingRate;
            _wiresArray = new List<IMusicalString>();
            CreateWires(keys);//read and create the wires based on the string keys
            
        }
     
        public int SamplingRate{
            get;
        }
        public List<IMusicalString> WiresArr{
            get;
        }
        /// Strikes the piano key (wire) corresponding to the specified character
         public void StrikeKey(char key){
            if(Keys.IndexOf(key) == -1){
                throw new ArgumentException("key doesn't exists in keys list");
            }
            _wiresArray[Keys.IndexOf(key)].Strike();
        }
        private List<IMusicalString> CreateWires(String Keys){
            for(int i =0; i<Keys.Length;i++){
                double frequency = Math.Pow(2.0,(double)((i-24.0)/12.0))*440.0;
                PianoWire aWire = new PianoWire(frequency, 44100);
                _wiresArray.Add(aWire); 
            }

            return _wiresArray;
        }

        /// Plays all of the vibrating keys (wires) at the current time step.
        public double Play(){
            double sumOfSamples =0;
            foreach (PianoWire wire in _wiresArray){
              sumOfSamples += wire.Sample();
            }

            return sumOfSamples;
        }
        /// List containing a string descibring all the wires in the piano with their key and note frequency
        public List<string> GetPianoKeys(){
            List<string> PianoKeys = new List<string>();
            for(int i =0; i<Keys.Length;i++){
                string aKey = $" {Keys[i]} {_wiresArray[i].NoteFrequency}";
                PianoKeys.Add(aKey);

            }
            return PianoKeys;

        }
        public string Keys {get;}
    }
}