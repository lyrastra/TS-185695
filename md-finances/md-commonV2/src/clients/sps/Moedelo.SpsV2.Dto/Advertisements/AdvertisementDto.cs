using System;

namespace Moedelo.SpsV2.Dto.Advertisements
{
    public class AdvertisementDto
    {
        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Notice { get; set; }

        public string Text { get; set; }

        public string ImagePath { get; set; }

        public string Url { get; set; }

        public string UrlText { get; set; }
    }
}