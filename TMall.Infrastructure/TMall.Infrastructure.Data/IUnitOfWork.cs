// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="CIK">
//   Copyright by Thang Chung
// </copyright>
// <summary>
//   Defines the IUnitOfWork type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.Objects;

namespace TMall.Infrastructure.Data
{
    /// <summary>
    ///     业务单元操作接口
    /// </summary>
    public interface IUnitOfWork
    {
        #region 属性

        /// <summary>
        ///     获取 当前单元操作是否已被提交
        /// </summary>
        bool IsCommitted { get; }

        #endregion

        #region 方法

        /// <summary>
        ///     提交当前单元操作的结果
        /// </summary>
        /// <returns></returns>
        int Commit();

        /// <summary>
        ///     把当前单元操作回滚成未提交状态
        /// </summary>
        void Rollback();

        #endregion
    }
}
