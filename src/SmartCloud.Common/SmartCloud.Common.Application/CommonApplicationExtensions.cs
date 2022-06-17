using System.Text.Json;

namespace SmartCloud.Common
{
    public static class CommonApplicationExtensions
    {
        public static List<DataIndexs.Description> ToDataIndexDescriptions(this string description)
        {
            return JsonSerializer.Deserialize<List<DataIndexs.Description>>(description);
        }

        public static List<Description> ToDescriptions(this string description)
        {
            return JsonSerializer.Deserialize<List<Description>>(description);
        }

        public static void ToTree(this INodeDto root, List<INodeDto> dtos)
        {
            List<INodeDto> curNodes = new();

            if (root.Category == 0)
            {
                curNodes = dtos.Where(m => m.ParentId == "").OrderBy(m => m.No).ToList();
            }
            else
            {
                curNodes = dtos.Where(m => m.ParentId == root.Id.ToString()).OrderBy(m => m.No).ToList();
            }

            foreach (var item in curNodes)
            {
                item.Nodes = new();
                root.Nodes.Add(item);
                
                ToTree(item, dtos);
            }
        }
    }
}
