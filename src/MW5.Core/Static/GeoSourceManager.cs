﻿using MapWinGIS;
using MW5.Core.Concrete;
using MW5.Core.Helpers;
using MW5.Core.Interfaces;

namespace MW5.Core.Static
{
    public static class GeoSourceManager
    {
        private static readonly FileManager _manager;

        static GeoSourceManager()
        {
            _manager = new FileManager();
        }

        public static IDatasource Open(string filename, OpenStrategy openStrategy = OpenStrategy.AutoDetect)
        {
            var source = _manager.Open(filename, (tkFileOpenStrategy)openStrategy, null);
            return LayerSourceHelper.Convert(source);
        }

        public static IFeatureSet OpenShapefile(string filename)
        {
            var sf = _manager.OpenShapefile(filename);
            if (sf != null)
            {
                return new FeatureSet(sf);
            }
            return null;
        }

        public static IImageSource OpenRaster(string filename, OpenStrategy strategy)
        {
            var img = _manager.OpenRaster(filename, (tkFileOpenStrategy)strategy);
            if (img != null)
            {
                return BitmapSource.Wrap(img);
            }
            return null;
        }

        public static bool ClearGdalOverviews(string filename)
        {
            return _manager.ClearGdalOverviews(filename);
        }

        public static bool BuildGdalOverviews(string filename)
        {
            return _manager.BuildGdalOverviews(filename);
        }

        public static bool RemoveProxyForGrid(string filename)
        {
            return _manager.RemoveProxyForGrid(filename);
        }

        public static VectorLayer OpenFromDatabase(string connectionString, string layerNameOrQuery)
        {
            var layer = _manager.OpenFromDatabase(connectionString, layerNameOrQuery);
            return layer != null ? new VectorLayer(layer) : null;
        }

        public static VectorLayer OpenVectorLayer(string filename, GeometryType preferedGeometryType = GeometryType.None, bool forUpdate = false)
        {
            var shpType = GeometryHelper.GeometryType2ShpType(preferedGeometryType);
            var layer = _manager.OpenVectorLayer(filename, shpType, forUpdate);
            return layer != null ? new VectorLayer(layer) : null;
        }

        public static IVectorDatasource OpenVectorDatasource(string filename)
        {
            var ds = _manager.OpenVectorDatasource(filename);
            return ds != null ? new VectorDatasource(ds) : null;
        }

        public static bool get_IsSupported(string filename)
        {
            return _manager.IsSupported[filename];
        }

        public static bool get_IsRgbImage(string filename)
        {
            return _manager.IsRgbImage[filename];
        }

        public static bool get_IsGrid(string filename)
        {
            return _manager.IsGrid[filename];
        }

        public static bool get_IsVectorLayer(string filename)
        {
            return _manager.IsVectorLayer[filename];
        }

        public static OpenStrategy GetOpenStrategy(string filename)
        {
            return (OpenStrategy)_manager.OpenStrategy[filename];
        }

        public static bool get_CanOpenAs(string filename, OpenStrategy strategy)
        {
            return _manager.CanOpenAs[filename, (tkFileOpenStrategy)strategy];
        }

        public static bool get_IsSupportedBy(string filename, SupportType supportType)
        {
            return _manager.IsSupportedBy[filename, (tkSupportType)supportType];
        }

        public static string LastError
        {
            get { return _manager.ErrorMsg[_manager.LastErrorCode]; }
        }

        public static OpenStrategy LastOpenStrategy
        {
            get { return (OpenStrategy)_manager.LastOpenStrategy; }
        }

        public static string LastOpenFilename
        {
            get { return _manager.LastOpenFilename; }
        }

        public static bool LastOpenIsSuccess
        {
            get { return _manager.LastOpenIsSuccess; }
        }

        public static bool get_HasGdalOverviews(string filename)
        {
            return _manager.HasGdalOverviews[filename];
        }

        public static bool get_HasValidProxyForGrid(string filename)
        {
            return _manager.HasValidProxyForGrid[filename];
        }

        public static string OpenFileFilter
        {
            get { return _manager.CdlgFilter; }
        }

        public static string RasterFilter
        {
            get { return _manager.CdlgRasterFilter; }
        }

        public static string VectorFilter
        {
            get { return _manager.CdlgVectorFilter; }
        }

        public static string SupportedGdalFormats
        {
            get { return _manager.SupportedGdalFormats; }
        }
    }
}