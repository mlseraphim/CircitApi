namespace CircitApi.Models
{
    public class RapidAstronomy
    {
        public RapidAstronomyAstronomy astronomy { get; set; }
    }

    public class RapidAstronomyAstronomy
    {
        public RapidAstronomyAstronomyAstro astro { get; set; }
    }

    public class RapidAstronomyAstronomyAstro
    {
        public string sunrise { get; set; }

        public string sunset { get; set; }

        public string moonrise { get; set; }

        public string moonset { get; set; }

        public string moon_phase { get; set; }

        public int moon_illumination { get; set; }

        public int is_moon_up { get; set; }

        public int is_sun_up { get; set; }
    }
}