using Microsoft.EntityFrameworkCore;
using SmartCloud.Common.DataIndexs;
using SmartCloud.Common.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SmartCloud.Common.Attachments
{
    public class EfAttachmentRepository : EfCoreRepository<CommonDbContext, Attachment, Guid>, IAttachmentRepository
    {
        public EfAttachmentRepository(IDbContextProvider<CommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Attachment>> GetListAsync(QueryEnum query, string tableId, string name)
        {
            var dbSet = await GetDbSetAsync();

            switch (query)
            {
                case QueryEnum.File:
                    {
                        if (name.IndexOf(".zip") > -1)
                        {
                            return await dbSet.Where(d => d.TableId == tableId && d.ServerFileName.Left(20) == name.Left(20)).OrderBy(d => d.Name).ToListAsync();
                        }
                        else
                        {
                            return await dbSet.Where(d => d.TableId == tableId && d.ServerFileName == name ).OrderBy(d => d.Name).ToListAsync();
                        }
                    }
                case QueryEnum.Folder:
                    return await dbSet.Where(d => d.TableId == tableId && d.ServerPathName.StartsWith(name) && d.Length == "").OrderBy(d => d.Name).ToListAsync();
                case QueryEnum.Row:
                    return await dbSet.Where(d => d.TableId == tableId).OrderBy(d => d.Name).ToListAsync();
                case QueryEnum.Rows:
                    return await dbSet.Where(d => name.Contains(d.TableId)).OrderBy(d => d.Name).ToListAsync();
                default:
                    return await dbSet.Where(d => d.TableId == tableId).OrderBy(d => d.Name).ToListAsync();
            }
        }
    }
}
