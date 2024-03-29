﻿using Domain.Enum;

namespace API.DTOs.Client
{
    public class SelectingClientDto
    {
        public int Id { get; set; }

        public string SelectedName { get; set; }

        public CodeOfTheCountry SelectedСodeOfTheCountry { get; set; }

        public string SelectedRegionCode { get; set; }

        public string SelectedSubscriberNumber { get; set; }
    }
}
