using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace SmartCloud.Common.Datas
{
    public class DataManager : DomainService
    {
        private readonly IDataRepository _repository;

        public DataManager(IDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 获取实体个数
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <returns>个数</returns>
        public async Task<int> GetDatasCount(string category)
        {
            var datas = await _repository.GetListAsync(category);
            return datas.Count();
        }
    }
}
