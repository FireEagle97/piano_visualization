using System.Collections.Generic;
using System;
namespace InteractivePiano{
    
    public class Piano : IPiano{
        public Piano(string keys = "q2we4r5ty7u8i9op-[=zxdcfvgbnjmk,.;/' ",  int samplingRate = 44100){
            Keys = keys;
            SamplingRate = samplingRate;
            WiresArr = new List<IMusicalString>();
            CreateWires(keys);//read and create the wires based on the string keys
            
        }
     
        public int SamplingRate{
            get;
        }
        public List<IMusicalString> WiresArr{
            get;
        }
        private List<IMusicalString> CreateWires(String Keys){
            foreach (char key in Keys){
            double frequency = Math.Pow(2.0,(double)((Keys.IndexOf(key)-24.0)/12.0))*440.0;
            PianoWire aWire = new PianoWire(frequency, 44100);
            WiresArr.Add(aWire); 
            }

            return WiresArr;
        }
        /// Strikes the piano key (wire) corresponding to the specified character
        public void StrikeKey(char key){
            double frequency = Math.Pow(2.0,(double)((Keys.IndexOf(key)-24.0)/12.0))*440.0;
            foreach (PianoWire wire in WiresArr){
                if(wire.NoteFrequency== frequency){
                    wire.Strike();
                }
            }
        }
        /// Plays all of the vibrating keys (wires) at the current time step.
        public double Play(){
            double sumOfSamples =0;
            foreach (PianoWire wire in WiresArr){
              sumOfSamples += wire.Sample();
            }

            return sumOfSamples;
        }
        /// List containing a string descibring all the wires in the piano with their key and note frequency
        public List<string> GetPianoKeys(){
            List<string> PianoKeys = new List<string>();
            for(int i =0; i<Keys.Length;i++){
                double frequency = Math.Pow(2.0,(double)((i-24.0)/12.0))*440.0;
                string aKey = $" {Keys[i]} {frequency}";
                PianoKeys.Add(aKey);

            }
            return PianoKeys;

        }
        public string Keys {get;}
    }
}