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
        /// 按类别名称批量删除
        /// </summary>
        /// <param name="name">类别名称</param>
        /// <returns></returns>
        public async Task DeleteAllByCategoryName(string name)
        {
            var datas = await _repository.GetListAsync(name);

            await _repository.DeleteManyAsync(datas);
        }

        /// <summary>
        /// 按旧类别名称批量修改新类别名称
        /// </summary>
        /// <param name="oldName">旧类别名称</param>
        /// <param name="newName">新类别名称</param>
        /// <returns></returns>
        public async Task ChangeAllByCategoryName(string oldName, string newName)
        {
            var datas = await _repository.GetListAsync(oldName);

            Parallel.ForEach(datas, data => {
                data.Category = newName;
            });

            await _repository.UpdateManyAsync(datas);
        }

        
    }
}
