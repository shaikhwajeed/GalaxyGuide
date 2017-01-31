namespace GalaxyGuide.Core.Parser
{
    /// <summary>
    /// Represent the type of result/processing is done.
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Represent the Unit related input.
        /// </summary>
        UnitInfo,
        /// <summary>
        /// Represents Metal Info.
        /// </summary>
        MetalInfo,
        /// <summary>
        /// Represents question.
        /// </summary>
        Question,

        /// <summary>
        /// Represents unknown entity.
        /// </summary>
        Unknown
    }
}
