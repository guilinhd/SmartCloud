
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace SmartCloud.Common.Datas
{
    public class GetDataListDto : PagedAndSortedResultRequestDto
    {
        [Required]
        public string Category { set; get; } = "";
    }
}
