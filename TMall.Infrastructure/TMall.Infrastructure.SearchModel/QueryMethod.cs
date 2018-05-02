namespace TMall.Infrastructure.SearchModel
{
    /// <summary>
    /// Html��Ԫ�صļ�����ʽ
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags")]
    public enum QueryMethod
    {
        /// <summary>
        /// ����
        /// </summary>
        //[GlobalCode("=", OnlyAttribute = true)]
        Equal = 0,

        /// <summary>
        /// С��
        /// </summary>
        //// [GlobalCode("<", OnlyAttribute = true)]
        LessThan = 1,

        /// <summary>
        /// ����
        /// </summary>
        // [GlobalCode(">", OnlyAttribute = true)]
        GreaterThan = 2,

        /// <summary>
        /// С�ڵ���
        /// </summary>
        // [GlobalCode("<=", OnlyAttribute = true)]
        LessThanOrEqual = 3,

        /// <summary>
        /// ���ڵ���
        /// </summary>
        // [GlobalCode(">=", OnlyAttribute = true)]
        GreaterThanOrEqual = 4,

        /// <summary>
        /// Like
        /// </summary>
        // [GlobalCode("like", OnlyAttribute = true)]
        Like = 6,

        /// <summary>
        /// In
        /// </summary>
        // [GlobalCode("in", OnlyAttribute = true)]
        In = 7,

        /// <summary>
        /// ����һ��ʱ���ȡ��ǰ���ʱ������, ToSqlδʵ�֣���ʵ����IQueryable
        /// </summary>
        // [GlobalCode("between", OnlyAttribute = true)]
        DateBlock = 8,

        // [GlobalCode("<>", OnlyAttribute = true)]
        NotEqual = 9,


        // [GlobalCode("like", OnlyAttribute = true)]
        StartsWith = 10,

        // [GlobalCode("like", OnlyAttribute = true)]
        EndsWith = 11,

        /// <summary>
        /// ����Like������
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        Contains = 12,

        /// <summary>
        /// ����In������
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        StdIn = 13,
        
        /// <summary>
        /// ����DatetimeС��+23h59m59s999f������
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        DateTimeLessThanOrEqual = 14
    }
}