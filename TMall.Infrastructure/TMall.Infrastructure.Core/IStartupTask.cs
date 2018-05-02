namespace TMall.Infrastructure.Core
{
    /// <summary>
    /// 需要在项目启动是执行的任务可以继承此接口
    /// </summary>
    public interface IStartupTask 
    {
        /// <summary>
        /// 执行操作
        /// </summary>
        void Execute();

        /// <summary>
        /// 任务序列级别
        /// </summary>
        int Order { get; }
    }
}
