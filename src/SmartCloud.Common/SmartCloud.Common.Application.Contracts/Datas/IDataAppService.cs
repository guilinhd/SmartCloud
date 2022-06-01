﻿using SmartCloud.Common.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartCloud.Common.Datas
{
    public interface IDataAppService : ICrudAppService<DataDto, Guid, GetDataListDto, DataDto>
    {
        /// <summary>
        /// 按类别名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <returns>数据字典信息列表</returns>
        Task<List<DataDto>> FindAllAsync(string category);

        /// <summary>
        /// 按类别名称、数据字典名称查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <returns>数据字典信息列表</returns>
        Task<List<DataDto>> FindAllAsync(string category, string name);

        /// <summary>
        /// 按类别名称、数据字典名称、数据字典备注查询
        /// </summary>
        /// <param name="category">类别名称</param>
        /// <param name="name">数据字典名称</param>
        /// <param name="remark">数据字典备注</param>
        /// <returns>数据字典信息列表</returns>
        Task<List<DataDto>> FindAllAsync(string category, string name, string remark);
    }
}
