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
        /// <param name="category">类别名称</param>
        /// <returns></returns>
        public async Task DeleteAsync(string category)
        {
            var datas = await _repository.GetListAsync(category);
            await _repository.DeleteManyAsync(datas);
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

        /// <summary>
        /// 按类别名称、数据字典名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <returns>数据字典信息列表</returns>
        public async Task<List<Data>> GetListAsync(string category, string name)
        {
            return await _repository.GetListAsync(category, name);
        }

        // <summary>
        /// 按类别名称、数据字典备注查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="remark">数据字典备注</param>
        /// <returns>数据字典信息列表</returns>

        public async Task<List<Data>> GetListRemarkAsync(string category, string remark)
        {
            return await _repository.GetListRemarkAsync(category, remark);
        }

        /// <summary>
        /// 按类别名称、数据字典名称、数据字典备注查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <param name="remark">数据字典备注</param>
        /// <returns>数据字典信息列表</returns>
        public async Task<List<Data>> GetListAsync(string category, string name, string remark)
        {
            return await _repository.GetListAsync(category, name, remark);
        }

        /// <summary>
        /// 按类别名称、数据字典名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <returns>数据字典信息列表</returns>
        public async Task<ICollection<string>> GetNameAsync(string category, string name)
        {
            var datas = await _repository.GetListAsync(category, name);
            return datas.GroupBy(d => d.Remark1).Select(d => d.Key).ToArray();
        }

        /// <summary>
        /// 按类别名称批量查询
        /// </summary>
        /// <param name="categories">类别名称</param>
        /// <returns></returns>
        public async Task<Dictionary<string, ICollection<string>>> GetNameListAsync(string[] categories)
        {
            Dictionary<string, ICollection<string>> names = new Dictionary<string, ICollection<string>>();

            var datas = await _repository.GetListAsync(categories);
            foreach (var category in categories)
            {
                names.Add(
                    category,
                    datas.Where(d => d.Category == category).GroupBy(d => d.Name).Select(d=>d.Key).ToArray()
                );
            }

            return names;
        }

        /// <summary>
        /// 按类别名称批量更新
        /// </summary>
        /// <param name="oldCategory">旧名称</param>
        /// <param name="newCategory">新名称</param>
        /// <returns></returns>
        public async Task UpdateAsync(string oldCategory, string newCategory)
        {
            var datas = await _repository.GetListAsync(oldCategory);

            if (datas.Count > 0)
            {
                Parallel.ForEach(datas, data =>
                {
                    data.Category = newCategory;
                });

                await _repository.UpdateManyAsync(datas);
            }

        }
    }
}
