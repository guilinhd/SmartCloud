using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.Domain.DataIndexs;
using Volo.Abp.Data;

namespace SmartCloud.Common.EntityFrameworkCore
{
    [ConnectionStringName("SmartCloud")]
    public interface ICommonDbContext
    {
        DbSet<DataIndex> DataIndexs { get; }
    }
}
