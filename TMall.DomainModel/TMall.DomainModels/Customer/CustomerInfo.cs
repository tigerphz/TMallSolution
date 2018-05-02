using System;
using TMall.DomainModule.Enums;
using TMall.DomainModels.Base;
using TMall.Infrastructure.Utility;

namespace TMall.DomainModels.Customer
{
    public class CustomerInfo : BaseEntity
    {
        public CustomerInfo()
        {
            SysNo = EntityGuid.NewComb();
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 真名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 通过PasswordSalt混淆加密后的密码散列码
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        //public string NewPassword { get; set; }

        /// <summary>
        /// 混淆码
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// 客户状态
        /// </summary>
        public CustomerStatus? Status
        {
            get
            {
                return (CustomerStatus) BitConverter.ToInt32(
                    new byte[] {StatusByte, 0x0, 0x0, 0x0}, 0);
            }
            set { StatusByte = BitConverter.GetBytes((int) value)[0]; }
        }

        public byte StatusByte { get; set; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// QQ号码
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 居住区域SysNo
        /// </summary>
        public int? DwellAreaSysno { get; set; }

        /// <summary>
        /// 居住地址
        /// </summary>
        public string DwellAddress { get; set; }

        /// <summary>
        /// 居住地邮政编码
        /// </summary>
        public string DwellZip { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiveName { get; set; }

        /// <summary>
        /// 收货联系人姓名
        /// </summary>
        public string ReceiveContact { get; set; }

        /// <summary>
        /// 收货固定电话
        /// </summary>
        public string ReceivePhone { get; set; }

        /// <summary>
        /// 收货移动电话
        /// </summary>
        public string ReceiveCellPhone { get; set; }

        /// <summary>
        /// 收货传真号码
        /// </summary>
        public string ReceiveFax { get; set; }

        /// <summary>
        /// 收货区域sysno
        /// </summary>
        public int? ReceiveAreaSysNo { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string ReceiveAddress { get; set; }

        /// <summary>
        /// 收货地址邮政编码
        /// </summary>
        public string ReceiveZip { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// 有效积分
        /// </summary>
        public int ValidScore { get; set; }

        /// <summary>
        /// 有效的预支付金额
        /// </summary>
        public decimal ValidPrepayAmt { get; set; }

        /// <summary>
        /// 邮箱是否通过验证
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int CustomerType { get; set; }

        /// <summary>
        /// 总订单金额
        /// </summary>
        public decimal TotalSOMoney { get; set; }

        /// <summary>
        /// 已确认的总金额
        /// </summary>
        public decimal ConfirmedTotalAmt { get; set; }

        /// <summary>
        /// 用户来源
        /// </summary>
        public string FromLinkSource { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 是否允许评论
        /// </summary>
        public bool IsAllowComment { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 推荐人ID
        /// </summary>
        public Guid? RecommendedByCustomerID { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public Guid? UpdateUserID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
