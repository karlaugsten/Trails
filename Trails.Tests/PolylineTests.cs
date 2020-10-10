using System;
using System.Collections.Generic;
using Xunit;

namespace Trails.Tests
{
    public class PolylineTests
    {
        [Fact]
        public void TestPolylineAlgorithmConvertsSingleValue()
        {
            double value = -179.9832104;
            var result = PolylineConverter.Convert(value);
            Assert.Equal("`~oia@", result);
        }

        [Fact]
        public void TestPolylineAlgorithmConvertsListOfValues()
        {
            var list = new List<Location>() {
                new Location() {
                    Latitude = 38.5,
                    Longitude = -120.2
                },
                new Location() {
                    Latitude = 40.7,
                    Longitude = -120.95
                },
                new Location() {
                    Latitude = 43.252,
                    Longitude = -126.453
                },
            };
            var result = PolylineConverter.Convert(list);
            Assert.Equal("_p~iF~ps|U_ulLnnqC_mqNvxq`@", result);
        }

        [Fact]
        public void TestPolylineConvertsLocationsBack() {
            var list = new List<Location>() {
                new Location() {
                    Latitude = 38.5,
                    Longitude = -120.2
                },
                new Location() {
                    Latitude = 40.7,
                    Longitude = -120.95
                },
                new Location() {
                    Latitude = 43.252,
                    Longitude = -126.453
                }
            };
            var polyline = PolylineConverter.Convert(list);
            var result = PolylineConverter.ParseGps(polyline);
            int i = 0;
            Assert.Equal(list.Count, result.Count);
            foreach(var loc in result) {
                Assert.Equal(list[i].Latitude, loc.Latitude);
                Assert.Equal(list[i++].Longitude, loc.Longitude);
            }
        }

        [Fact]
        public void TestPolylineAlgorithmConvertsIntsAndBack()
        {
            var list = new List<int>() {
                1501,
                1502,
                1600,
                1550
            };
            var polyline = PolylineConverter.Convert(list);
            var result = PolylineConverter.ParseInts(polyline);
            int i = 0;
            Assert.Equal(list.Count, result.Count);
            foreach(var value in result) {
                Assert.Equal(list[i++], value);
            }
        }

        [Fact]
        public void TestPolylineAlgorithmConvertsHighIntsAndBack()
        {
            var list = new List<int>() {
                8000,
                8001,
                7600,
                1
            };
            var polyline = PolylineConverter.Convert(list);
            var result = PolylineConverter.ParseInts(polyline);
            int i = 0;
            Assert.Equal(list.Count, result.Count);
            foreach(var value in result) {
                Assert.Equal(list[i++], value);
            }
        }
    }
}
