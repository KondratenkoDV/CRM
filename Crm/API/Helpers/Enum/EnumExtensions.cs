using API.DTOs.Enum;

namespace API.Helpers.Enum
{
    public static class EnumExtensions
    {
        public static List<ValueCodeOfTheCountryDto> GetValues<T>()
        {
            var valuesEnum = new List<ValueCodeOfTheCountryDto>();

            foreach (var enumValue in System.Enum.GetValues(typeof(T)))
            {
                valuesEnum.Add(new ValueCodeOfTheCountryDto()
                {
                    Name = System.Enum.GetName(typeof(T), enumValue),
                    Value = (int)enumValue
                });
            }

            return valuesEnum;
        }
    }
}
