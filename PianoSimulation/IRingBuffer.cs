namespace PianoSimulation
{
    /// <summary>
    /// Defines a fixed size ring, or circular, buffer
    /// </summary>
    public interface IRingBuffer
    {
        /// <summary>
        /// Length of the buffer
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Returns and removes the first element in the buffer. Adds value to the end of the buffer
        /// </summary>
        /// <param name="value">Value to be added at the end of the buffer</param>
        /// <returns>First element in the buffer</returns>
        double Shift(double value);

        /// <summary>
        /// Indexer to go through elements in the buffer starting at the front to the value at the end
        /// </summary>
        /// <param name="index">index, where 0 indicates front of the ring buffer</param>
        /// <returns>element at the index</returns>
        double this[int index] { get; }

        /// <summary>
        /// Performs a deep copy of the array into the buffer
        /// </summary>
        /// <param name="array">array of doubles to be copied</param>
        void Fill(double[] array);
    }
}