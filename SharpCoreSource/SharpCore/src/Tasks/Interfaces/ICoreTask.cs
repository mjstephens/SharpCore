using System;

namespace SharpCore.Tasks.Interfaces
{
    /// <summary>
    /// Describes client-facing API for core task system
    /// </summary>
    public interface ICoreTask : ICoreSystemInterface
    {
        /// <summary>
        /// Returns a task for immediate use.
        /// </summary>
        /// <param name="action">The ITaskable that will receive tick callbacks from the task.</param>
        /// <returns></returns>
        //TaskHandle Task(Action action);

        /// <summary>
        /// Returns a timed task for immediate use.
        /// </summary>
        /// <param name="action">The ITaskable that will receive tick callbacks from the task.</param>
        /// <param name="duration">The duration of the timed task.</param>
        /// <returns></returns>
        //TaskHandle Task(Action action, float duration);
    }
}