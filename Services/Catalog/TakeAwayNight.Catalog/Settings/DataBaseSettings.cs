﻿namespace TakeAwayNight.Catalog.Settings
{
    public class DataBaseSettings:IDataBaseSettings
    {
        public string CategoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string SliderCollectionName { get; set; }
        public string FeatureCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}