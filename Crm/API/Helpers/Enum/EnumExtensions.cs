using API.DTOs.Enum;

namespace API.Helpers.Enum
{
    public static class EnumExtensions
    {
        public static List<EnumValueDto> GetValues<T>()
        {
            var valuesEnum = new List<EnumValueDto>();

            foreach (var enumValue in System.Enum.GetValues(typeof(T)))
            {
                valuesEnum.Add(new EnumValueDto()
                {
                    Name = System.Enum.GetName(typeof(T), enumValue),
                    Value = (int)enumValue
                });
            }

            return valuesEnum;
        }
    }
}
