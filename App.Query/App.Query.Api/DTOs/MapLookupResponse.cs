using App.Common.DTOs;
using App.Query.Domain.Entities;

namespace App.Query.Api.DTOs
{
    public class MapLookupResponse: BaseResponse
    {
        public List<MapEntity> Maps { get; set; }
    }
}
