﻿using System;
using System.Collections.Generic;
using System.Linq;
using Mapsui.Samples.Common.Maps;
using Mapsui.Samples.Tests.Maps;

namespace Mapsui.Samples.Common
{
    public static class AllSamples
    {
        public static IEnumerable<IDemoSample> GetSamples()
        {
            var type = typeof(IDemoSample);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName.StartsWith("Mapsui"));

            return assemblies
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface)
                .Select(Activator.CreateInstance).Select(t => t as IDemoSample)
                .OrderBy(s => s.Name)
                .ToList();
        }

        private static Dictionary<string, Func<Map>> CreateList()
        {
            return new Dictionary<string, Func<Map>>
            {
                ["OpenStreetMap"] = OsmSample.CreateMap,
                ["Virtual Earth"] = BingSample.CreateMap,
                ["Simple Points"] = PointsSample.CreateMap,
                ["Simple LineStrings"] = LineStringSample.CreateMap,
                ["Simple Polygons"] = PolygonSample.CreateMap,
                ["Complex Polygons"] = ComplexPolygonSample.CreateMap,
                ["Various"] = VariousSample.CreateMap,
                ["Labels"] = LabelsSample.CreateMap,
                ["Center on location"] = CenterOnLocationSample.CreateMap,
                ["Projected point"] = ProjectionSample.CreateMap,
                ["Stacked labels"] = StackedLabelsSample.CreateMap,
                ["Symbols"] = SymbolsSample.CreateMap,
                ["Map Info"] = InfoLayersSample.CreateMap,
                ["MbTiles"] = MbTilesSample.CreateMap,
                ["MbTiles Overlay"] = MbTilesOverlaySample.CreateMap,
                ["Keep Within Extents"] = KeepWithinExtentsSample.CreateMap,
                ["Pen Stroke Cap"] = PenStrokeCapSample.CreateMap,
                ["Animated point movement"] = AnimatedPointsSample.CreateMap,
                ["WMTS"] = WmtsSample.CreateMap,
                ["WMS called tiled"] = TiledWmsSample.CreateMap,
                ["Rasterizing Layer"] = RasterizingLayerSample.CreateMap,
                ["Empty Map"] = EmptyMapSample.CreateMap,
                ["Mutating triangle"] = MutatingTriangleSample.CreateMap,
                ["Symbols in World Units"] = SymbolsInWorldUnitsSample.CreateMap,
                ["Widgets"] = WidgetSample.CreateMap,
                ["ScaleBar"] = ScaleBarSample.CreateMap,
                ["OpacityStyle"] = OpacityStyleSample.CreateMap,
                ["Svg"] = SvgSample.CreateMap,
                ["Atlas"] = AtlasSample.CreateMap,
                ["Itinero routing"] = ItineroRoutingSample.CreateMap,
                ["Custom Widget"] = CustomWidgetSample.CreateMap
            };
        }
    }
}
