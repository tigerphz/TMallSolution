using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMall.DomainModels.Management;
using TMall.Infrastructure.Data;
using TMall.Services.IRepository.Management;
using TMall.Infrastructure.Utility;
using TMall.Framework.Logging;
using TMall.Framework.ExceptionHanding;
using TMall.Infrastructure.Web;
using TMall.Infrastructure.Core;

namespace TMall.Services.IBizServices.Management
{
    public class ExceptionLogBizService : IExceptionLogBizService
    {
        private IExceptionLogRepository _logRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ExceptionLogBizService(IExceptionLogRepository logRepository)
        {
            _logRepository = logRepository;
            _unitOfWork = _logRepository.UnitOfWork;
        }

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return false;
                default:
                    return true;
            }
        }

        public void DeleteLog(Guid sysno)
        {
            ExceptionLogInfo logInfo = new ExceptionLogInfo() { SysNo = sysno };
            _logRepository.Delete(logInfo);
        }

        public ExceptionLogInfo GetLogById(Guid sysno)
        {
            return _logRepository.GetByKey(sysno);
        }

        public IList<ExceptionLogInfo> GetLogByIds(Guid[] sysnos)
        {
            return _logRepository.Entities.Where(x => sysnos.Contains(x.SysNo)).ToList();
        }

        public SearchPageInfo<ExceptionLogInfo> GetLogs(PageInfo pageInfo)
        {
            if (string.IsNullOrEmpty(pageInfo.OrderField))
            {
                throw ExceptionHelper.ThrowBusinessException("分页获取数据必须指定排序列");
            }

            SearchPageInfo<ExceptionLogInfo> seachPage = new SearchPageInfo<ExceptionLogInfo>();
            seachPage.PageInfo = pageInfo;
            seachPage = _logRepository.GetPageEntities(seachPage);
            return seachPage;
        }

        public SearchPageInfo<ExceptionLogInfo> GetLogs(SearchPageInfo<ExceptionLogInfo> search)
        {
            Check.NotNull(search, "search");
            Check.NotNull(search.PageInfo, "search.PageInfo");
            if (string.IsNullOrEmpty(search.PageInfo.OrderField))
            {
                throw ExceptionHelper.ThrowBusinessException("分页获取数据必须指定排序列");
            }

            search = _logRepository.GetPageEntities(search);
            return search;
        }

        public void Log(LogLevel logLevel, Exception exception, string format, params object[] args)
        {
            IWebHelper _webHelper = EngineContext.Current.Resolve<IWebHelper>();

            var log = new ExceptionLogInfo()
            {
                Source = exception.Source,
                InnerException = exception.InnerException == null ? string.Empty : exception.InnerException.Message,
                EventStackTrace = exception == null ? string.Empty : exception.StackTrace,
                EventType = exception == null ? string.Empty : exception.GetType().FullName,
                EventDetail = exception == null ? string.Empty : exception.Message,
                EventMessage = args == null || args.Length == 0 ? format : string.Format(format, args),
                HostName = _webHelper.GetCurrentHostName(),
                LogLevelName = logLevel.ToString(),
                IpAddress = _webHelper.GetCurrentIpAddress(),
                PageUrl = _webHelper.GetThisPageUrl(),
                ReferrerUrl = _webHelper.GetUrlReferrer(),
            };

            _logRepository.Insert(log);
        }
    }
}
